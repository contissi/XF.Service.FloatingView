using Android.App;
using Android.Content;
using Plugin.CurrentActivity;

namespace XF.Service.FloatingView.Droid.BroadcastReceivers
{
    [BroadcastReceiver]
    [IntentFilter(new[] { _broadcastId }, DataMimeType = "text/plain")]
    public class ServiceBroadcaster : BroadcastReceiver
    {
        public static string ReturnToApp = "ComingBack";
        const string _broadcastId = "com.XF.Service.FloatingViewService";

        public ServiceBroadcaster()
        {
        }

        private Context CurrentContext => CrossCurrentActivity.Current.Activity;

        public void Send()
        {
            Intent BroadcastIntent = new Intent(CurrentContext, typeof(ServiceBroadcaster));
            CurrentContext.SendBroadcast(BroadcastIntent);
        }

        public override void OnReceive(Context context, Intent intent)
        {
            Intent selfIntent = new Intent(context, typeof(MainActivity));
            selfIntent.SetFlags(ActivityFlags.ReorderToFront | ActivityFlags.SingleTop | ActivityFlags.ClearTop);
            context.StartActivity(selfIntent);
        }
    }
}
