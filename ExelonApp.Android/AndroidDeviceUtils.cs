using static Android.Provider.Settings;
using Xamarin.Forms;
using Firebase.Messaging;
using Android.Gms.Tasks;
using Java.Lang;

[assembly: Dependency(typeof(ExelonApp.Droid.AndroidDeviceUtils))]
namespace ExelonApp.Droid
{
    class AndroidDeviceUtils : IDeviceUtils
    {
        public static string pnsToken = "";
        public string GetDeviceId() => Secure.GetString(Android.App.Application.Context.ContentResolver, Secure.AndroidId);

        public string GetPNSToken()
        {
            return pnsToken;
        }
    }
}