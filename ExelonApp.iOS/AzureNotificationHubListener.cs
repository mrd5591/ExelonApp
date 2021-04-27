using UIKit;
using WindowsAzure.Messaging.NotificationHubs;

namespace ExelonApp.iOS
{
    public class AzureNotificationHubListener : MSNotificationHubDelegate
    {
        public override void DidReceivePushNotification(MSNotificationHub notificationHub, MSNotificationHubMessage message)
        {
            // This sample assumes { aps: { alert: { title: "Hello", body: "World" } } }
            var alertTitle = message.Title ?? "Notification";
            var alertBody = message.Body;

            var myAlert = UIAlertController.Create(alertTitle, alertBody, UIAlertControllerStyle.Alert);
            myAlert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
            UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(myAlert, true, null);
        }
    }
}