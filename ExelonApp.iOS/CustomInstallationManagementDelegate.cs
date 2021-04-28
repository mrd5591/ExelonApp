using ObjCRuntime;
using WindowsAzure.Messaging.NotificationHubs;

namespace ExelonApp.iOS
{
    public class CustomInstallationManagementDelegate : MSInstallationManagementDelegate
    {
        public override void WillUpsertInstallation(MSNotificationHub notificationHub, MSInstallation installation, [BlockProxy(typeof(NullableCompletionHandler))] NullableCompletionHandler completionHandler)
        {
            
        }
    }
}