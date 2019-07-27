using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Plugin.CurrentActivity;
using Android.Provider;
using XF.Service.FloatingView.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(XF.Service.FloatingView.Droid.Services.IntentService))]
namespace XF.Service.FloatingView.Droid.Services
{
    public class IntentService : IIntentService
    {
        public static Intent FloatingViewServiceIntent { get; private set; }
        public static int ReqCodeDrawOverPermission { get => 1234; }

        private Context CurrentContext => CrossCurrentActivity.Current.Activity;

        public IntentService()
        {
            FloatingViewServiceIntent = new Intent(CurrentContext, typeof(FloatingViewService));
        }

        public void AskForDrawOverPermission()
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.M)
            {
                if (!Android.Provider.Settings.CanDrawOverlays(CurrentContext))
                {
                    Intent intent = new Intent(Settings.ActionManageOverlayPermission,
                            Android.Net.Uri.Parse("package:" + CurrentContext.PackageName));

                    var mainActivity = CurrentContext as Activity;

                    mainActivity.StartActivityForResult(intent, ReqCodeDrawOverPermission);
                }
            }
        }

        public void StartFloatingView()
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.M)
            {
                CurrentContext.StartService(FloatingViewServiceIntent);
            }
            else
            {
                if (!Settings.CanDrawOverlays(CurrentContext))
                {
                    Toast.MakeText(CurrentContext, "Draw over permission required.", ToastLength.Long);
                }
                else
                {
                    CurrentContext.StartService(FloatingViewServiceIntent);
                }
            }
        }

        public void StopFloatingView()
        {
            CurrentContext.StopService(FloatingViewServiceIntent);
        }
    }
}
