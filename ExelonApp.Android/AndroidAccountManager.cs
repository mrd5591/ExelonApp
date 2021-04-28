using ExelonApp.Util;
using Xamarin.Forms;

[assembly: Dependency(typeof(ExelonApp.Droid.AndroidAccountManager))]
namespace ExelonApp.Droid
{
    class AndroidAccountManager : IAccountManager
    {
        public void Initialize()
        {
            TinyAccountManager.Droid.AccountManager.Initialize();
        }
    }
}