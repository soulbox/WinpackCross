using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms;
using Plugin.Toasts;
//using Plugin.Toasts;
//using Xamarin.Forms;

namespace WinpackCross.Droid
{
    [Activity(Label = "WinpackCross", Icon = "@drawable/LogoWp",
        Theme = "@style/MainTheme", MainLauncher = true, 
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            
            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            //DependencyService.Register<ToastNotification>();
            DependencyService.Register<ToastNotification>();
            ToastNotification.Init(this,new PlatformOptions() {SmallIconDrawable=Android.Resource.Drawable.IcDialogInfo });
            LoadApplication(new App());
        }
    }
}