using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(ExelonApp.iOS.iOSDeviceUtils))]
namespace ExelonApp.iOS
{
    class iOSDeviceUtils : IDeviceUtils
    {
        public string GetDeviceId() => UIDevice.CurrentDevice.IdentifierForVendor.ToString();
    }
}