using Foundation;
using ObjCRuntime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
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