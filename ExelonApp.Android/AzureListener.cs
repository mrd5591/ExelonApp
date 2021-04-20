using Android.Content;
using WindowsAzure.Messaging.NotificationHubs;

namespace ExelonApp.Droid
{
    public class AzureListener : Java.Lang.Object, INotificationListener
    {
        public void OnPushNotificationReceived(Context context, INotificationMessage message)
        {
            throw new System.NotImplementedException();
        }
    }
}