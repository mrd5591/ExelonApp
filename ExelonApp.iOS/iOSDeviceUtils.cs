using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(ExelonApp.iOS.iOSDeviceUtils))]
namespace ExelonApp.iOS
{
    class iOSDeviceUtils : IDeviceUtils
    {
        public static string pnsToken = "";
        public string GetDeviceId() => UIDevice.CurrentDevice.IdentifierForVendor.ToString();

        public string GetPNSToken()
        {
            return pnsToken;
        }
    }
}