namespace XF.Service.FloatingView.Interfaces
{
    public interface IIntentService
    {
        void StartFloatingView();
        void StopFloatingView();
        void AskForDrawOverPermission();
    }
}
