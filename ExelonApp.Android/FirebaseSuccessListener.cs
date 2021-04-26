using Android.Gms.Tasks;
using System;

namespace ExelonApp.Droid
{
    class FirebaseSuccessListener : Java.Lang.Object, IOnSuccessListener
    {
        public void OnSuccess(Java.Lang.Object result)
        {
            AndroidDeviceUtils.pnsToken = result.ToString();
        }
    }
}