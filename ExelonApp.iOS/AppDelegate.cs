using WindowsAzure.Messaging.NotificationHubs;
using UserNotifications;
using Foundation;
using UIKit;
using System.Runtime.InteropServices;
using System;

namespace ExelonApp.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            // Set the Message listener
            MSNotificationHub.SetDelegate(new AzureNotificationHubListener());

            // Start the SDK
            MSNotificationHub.Start(new CustomInstallationManagementDelegate());

            app.RegisterForRemoteNotifications();

            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }

        public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        {
            byte[] result = new byte[deviceToken.Length];
            Marshal.Copy(deviceToken.Bytes, result, 0, (int)deviceToken.Length);
            iOSDeviceUtils.pnsToken = BitConverter.ToString(result).Replace("-", "");
        }
    }
}
