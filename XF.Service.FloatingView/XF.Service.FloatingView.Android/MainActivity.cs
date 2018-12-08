using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Plugin.CurrentActivity;
using Prism;
using Prism.Ioc;
using XF.Service.FloatingView.Droid.BroadcastReceivers;
using XF.Service.FloatingView.Droid.Services;

namespace XF.Service.FloatingView.Droid
{
    [Activity(Label = "Floating View", Icon = "@mipmap/fv", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private Intent _serviceIntent;
        private ServiceBroadcaster _serviceReceiver;
        private const int _reqCodeDrawOverPermission = 1234;

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            CrossCurrentActivity.Current.Init(this, bundle);

            _serviceIntent = new Intent(this, typeof(FloatingViewService));

            RegisterBroadcastReceiver();

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App(new AndroidInitializer()));
        }

        protected override void OnPause()
        {
            base.OnPause();
            RequestDrawOverPermission();
        }

        protected override void OnResume()
        {
            base.OnResume();
            StopService(_serviceIntent);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            UnRegisterBroadcastReceiver();
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (requestCode == _reqCodeDrawOverPermission)
            {
                if (Android.Provider.Settings.CanDrawOverlays(this))
                {
                    StartService(_serviceIntent);
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

        private void RequestDrawOverPermission()
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.M)
            {
                StartService(_serviceIntent);
            }
            else
            {
                if (!Android.Provider.Settings.CanDrawOverlays(this))
                {
                    Intent intent = new Intent(Android.Provider.Settings.ActionManageOverlayPermission,
                            Android.Net.Uri.Parse("package:" + ApplicationContext.PackageName));

                    StartActivityForResult(intent, _reqCodeDrawOverPermission);
                }
                else
                {
                    StartService(_serviceIntent);
                }
            }
        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register any platform specific implementations
        }
    }
}

