using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using XF.Service.FloatingView.Droid.BroadcastReceivers;

namespace XF.Service.FloatingView.Droid.Services
{
    [Service(Label = "FloatingViewService")]
    [IntentFilter(new String[] { "com.XF.Service.FloatingViewService" })]
    public class FloatingViewService : Android.App.Service
    {
        IBinder _binder;
        IWindowManager _windowManager;
        FrameLayout _frameLayout;

        public override StartCommandResult OnStartCommand(Android.Content.Intent intent, StartCommandFlags flags, int startId)
        {
            return StartCommandResult.Sticky;
        }

        public override IBinder OnBind(Intent intent)
        {
            _binder = new FloatingViewServiceBinder(this);
            return _binder;
        }

        public override void OnCreate()
        {
            base.OnCreate();
            CreateFloatingIcon();
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            _windowManager.RemoveView(_frameLayout);
            _frameLayout = null;
        }

        private void CreateFloatingIcon()
        {
            WindowManagerLayoutParams layoutParams = new WindowManagerLayoutParams(WindowManagerLayoutParams.WrapContent,
                                                                                    WindowManagerLayoutParams.WrapContent,
                                                                                    WindowManagerTypes.ApplicationOverlay,
                                                                                    WindowManagerFlags.NotFocusable | WindowManagerFlags.WatchOutsideTouch | WindowManagerFlags.NotTouchModal,
                                                                                    Android.Graphics.Format.Translucent);

            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            {
                layoutParams.Type = WindowManagerTypes.Phone;
            }

            layoutParams.Gravity = GravityFlags.Left | GravityFlags.CenterVertical;

            _windowManager = GetSystemService(WindowService).JavaCast<IWindowManager>();
            _frameLayout = new FrameLayout(Android.App.Application.Context);

            var layoutInflator = LayoutInflater.From(Android.App.Application.Context);

            layoutInflator.Inflate(Resource.Layout.FloatingView, _frameLayout);

            var view = _frameLayout.FindViewById<ImageView>(Resource.Id.FloatingIcon);

            //view.SetOnTouchListener(new MyTouchListener(Android.App.Application.Context));
            view.SetOnClickListener(new MyClickListener(Android.App.Application.Context));

            _windowManager.AddView(_frameLayout, layoutParams);
        }
    }

    public class MyTouchListener : Java.Lang.Object, Android.Views.View.IOnTouchListener
    {
        Context _currentContext;
        float dX;
        float dY;

        float lastdX;
        float lastdY;

        MotionEventActions lastAction;

        public MyTouchListener(Context context)
        {
            _currentContext = context;
        }

        public bool OnTouch(Android.Views.View v, MotionEvent e)
        {

            switch (e.ActionMasked)
            {
                case MotionEventActions.Down:
                    lastdX = dX = v.GetX() - e.RawX;
                    lastdY = dY = v.GetY() - e.RawY;
                    lastAction = MotionEventActions.Down;
                    break;

                case MotionEventActions.Move:
                    v.SetY(e.RawY + dY);
                    v.SetX(e.RawX + dX);

                    lastdX = e.RawX + dX;
                    lastdY = e.RawY + dY;

                    lastAction = MotionEventActions.Move;
                    break;

                case MotionEventActions.Up:
                    if (lastAction == MotionEventActions.Down)
                    {
                        var currentX = v.GetX() - e.RawX;
                        var currentY = v.GetY() - e.RawY;

                        if (currentX == lastdX && currentY == lastdY)
                        {
                            v.CallOnClick();
                        }
                    }
                    //Toast.makeText(DraggableView.this, "Clicked!", Toast.LENGTH_SHORT).show();
                    break;
                default:
                    return false;
            }

            return true;
        }
    }

    public class MyClickListener : Java.Lang.Object, Android.Views.View.IOnClickListener
    {
        Context _currentContext;

        public MyClickListener(Context context)
        {
            _currentContext = context;
        }

        public void OnClick(Android.Views.View v)
        {
            Toast.MakeText(_currentContext, "Navigating back...", ToastLength.Long).Show();

            var broadcaster = new ServiceBroadcaster();

            broadcaster.Send();
        }
    }

    public class FloatingViewServiceBinder : Binder
    {
        readonly FloatingViewService service;

        public FloatingViewServiceBinder(FloatingViewService service)
        {
            this.service = service;
        }

        public FloatingViewService GetFloatingViewService()
        {
            return service;
        }
    }
}
