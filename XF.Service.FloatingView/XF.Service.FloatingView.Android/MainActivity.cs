using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Plugin.CurrentActivity;
using Prism;
using Prism.Ioc;
using XF.Service.FloatingView.Droid.BroadcastReceivers;
using XF.Service.FloatingView.Interfaces;

namespace XF.Service.FloatingView.Droid
{
    [Activity(Label = "Floating View", Icon = "@mipmap/fvicon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private ServiceBroadcaster _serviceReceiver;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            CrossCurrentActivity.Current.Init(this, savedInstanceState);

            RegisterBroadcastReceiver();

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App(new AndroidInitializer()));
        }


        protected override void OnDestroy()
        {
            base.OnDestroy();
            UnRegisterBroadcastReceiver();
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (requestCode == Services.IntentService.ReqCodeDrawOverPermission)
            {
                if (Android.Provider.Settings.CanDrawOverlays(this))
                {
                    StartService(Services.IntentService.FloatingViewServiceIntent);
                }
                else
                {
                    Android.Widget.Toast.MakeText(Android.App.Application.Context, "Draw over permission is required!", Android.Widget.ToastLength.Long);
                }
            }
            else
            {
                base.OnActivityResult(requestCode, resultCode, data);
            }
        }

        private void RegisterBroadcastReceiver()
        {
            IntentFilter filter = new IntentFilter(ServiceBroadcaster.ReturnToApp);
            filter.AddCategory(Intent.CategoryDefault);
            _serviceReceiver = new ServiceBroadcaster();
            RegisterReceiver(_serviceReceiver, filter);
        }

        private void UnRegisterBroadcastReceiver()
        {
            UnregisterReceiver(_serviceReceiver);
        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register any platform specific implementations
            containerRegistry.Register<IIntentService, Services.IntentService>();
        }
    }
}

