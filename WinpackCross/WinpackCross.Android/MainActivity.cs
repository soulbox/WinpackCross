using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms;
using Plugin.Toasts;
//using Microsoft.AppCenter;
//using Microsoft.AppCenter.Distribute;
using Android.Content;
//using Android.Support.V4.Content;
using Android;
using Android.Support.V4.App;
using Android.Support.V4.Content;
//using Android.Support.V4.App;


//using Plugin.Toasts;
//using Xamarin.Forms;

namespace WinpackCross.Droid
{
    [Activity(Label = "WinpackCross", Icon = "@drawable/LogoWp",
        Theme = "@style/MainTheme", MainLauncher = true, 
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public static  MainActivity AndroidMainActivity { get; set; }
        public MainActivity()
        {
            AndroidMainActivity = this;
           
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            StrictMode.VmPolicy.Builder builder = new StrictMode.VmPolicy.Builder();
            StrictMode.SetVmPolicy(builder.Build());
            builder.DetectFileUriExposure();

#pragma warning disable CS0618 // Type or member is obsolete
            bool isNonPlayAppAllowed = Android.Provider.Settings.Secure.GetInt(ContentResolver, Android.Provider.Settings.Secure.InstallNonMarketApps) == 1;
#pragma warning restore CS0618 // Type or member is obsolete

            //ContextCompat.CheckSelfPermission(this, Manifest.Permission.mar)
            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.WriteExternalStorage) != (int)Permission.Granted)
            {
               ActivityCompat.RequestPermissions(this, new string[] { Manifest.Permission.WriteExternalStorage }, 0);
            }

            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.ReadExternalStorage) != (int)Permission.Granted)
            {
                ActivityCompat.RequestPermissions(this, new string[] { Manifest.Permission.ReadExternalStorage }, 0);
            }
            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.RequestInstallPackages) != (int)Permission.Granted)
            {
                ActivityCompat.RequestPermissions(this, new string[] { Manifest.Permission.RequestInstallPackages }, 0);
            }



            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            //DependencyService.Register<ToastNotification>();
            DependencyService.Register<ToastNotification>();
            ToastNotification.Init(this,new PlatformOptions() {SmallIconDrawable=Android.Resource.Drawable.IcDialogInfo });
            LoadApplication(new App());
            }
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == 1)
            {
   
            }
        }
    }
}