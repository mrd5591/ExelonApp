using static Android.Provider.Settings;
using Xamarin.Forms;

[assembly: Dependency(typeof(ExelonApp.Droid.AndroidDeviceUtils))]
namespace ExelonApp.Droid
{
    class AndroidDeviceUtils : IDeviceUtils
    {
        public string GetDeviceId() => Secure.GetString(Android.App.Application.Context.ContentResolver, Secure.AndroidId);
    }
}