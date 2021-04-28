using ExelonApp.Util;
using Xamarin.Forms;

[assembly: Dependency(typeof(ExelonApp.iOS.iOSAccountManager))]
namespace ExelonApp.iOS
{
    class iOSAccountManager : IAccountManager
    {
        public void Initialize()
        {
            TinyAccountManager.iOS.AccountManager.Initialize();
        }
    }
}