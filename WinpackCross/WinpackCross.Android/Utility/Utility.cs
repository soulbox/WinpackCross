using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;

using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using WinpackCross;
using Android.Content.PM;
using WinpackCross.Utility;
using System.Runtime.CompilerServices;
using WinpackCross.Droid;
using Mono;
//using Android.Support.V4.Content;
using Android.Webkit;
using Java.IO;
//using Java.IO;
//using Android.Webkit;
//using Java.IO;
//using Android.Support.V4.Content;

[assembly: Xamarin.Forms.Dependency(typeof(Utility))]
namespace WinpackCross.Droid
{
    public class Utility : IUtility
    {
        PackageInfo _appInfo;
        public Utility()
        {
            try
            {
                var context = Application.Context;
                _appInfo = context.PackageManager.GetPackageInfo(context.PackageName, 0);
            }
            catch (Exception)
            {

          
            }

        }
        public string GetVersionNumber()
        {
            return _appInfo.VersionName;
        }
        public string GetBuildNumber()
        {
            return _appInfo.VersionCode.ToString();
        }
        public string Path { get => Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads).AbsolutePath; }

        public void ProccesStart(string path)
        {







            //Intent intentr = new Intent(Intent.ActionUninstallPackage);
            //intentr.SetData(Android.Net.Uri.Parse("package:" + MainActivity.AndroidMainActivity.ApplicationContext.PackageName));
            //intentr.PutExtra(Intent.ExtraReturnResult, true);

            //MainActivity.AndroidMainActivity.StartActivityForResult(intentr, 1);


            //Direk Kaldırır.
            //Intent intentr = new Intent(Intent.ActionDelete);
            //intentr.SetDataAndType(Android.Net.Uri.Parse("package:" + MainActivity.AndroidMainActivity.ApplicationContext.PackageName),mimeType );
            //MainActivity.AndroidMainActivity.StartActivity(intentr);


            //var mimeType = MimeTypeMap.Singleton.GetMimeTypeFromExtension(path.Substring(path.LastIndexOf(".")).Substring(1));
            //Java.IO.File apkFile = new Java.IO.File(path);

            //    Android.Net.Uri uri = Android.Support.V4.Content.FileProvider.GetUriForFile(MainActivity.AndroidMainActivity, MainActivity.AndroidMainActivity.ApplicationContext.PackageName + ".provider", apkFile);
            //    Intent intent = new Intent(Intent.ActionInstallPackage);
            //    intent.AddFlags(ActivityFlags.ClearTask | ActivityFlags.NewTask);
            //    intent.AddFlags(ActivityFlags.GrantReadUriPermission);
            //    intent.SetDataAndType(uri, mimeType);
            //    MainActivity.AndroidMainActivity.StartActivity(intent);

          


            ÇalışanStandartProcces(path );


        }
        void ÇalışanStandartProcces(string path)
        {
            var mimeType = MimeTypeMap.Singleton.GetMimeTypeFromExtension(path.Substring(path.LastIndexOf(".")).Substring(1));
            Java.IO.File apkFile = new Java.IO.File(path);
            if (Build.VERSION.SdkInt >= BuildVersionCodes.N)
            {
                Android.Net.Uri uri = Android.Support.V4.Content.FileProvider.GetUriForFile(MainActivity.AndroidMainActivity, MainActivity.AndroidMainActivity.ApplicationContext.PackageName + ".provider", apkFile);
                Intent intent = new Intent(Intent.ActionInstallPackage);
                intent.AddFlags(ActivityFlags.ClearTask | ActivityFlags.NewTask);
                intent.AddFlags(ActivityFlags.GrantReadUriPermission);
                intent.SetDataAndType(uri, mimeType);
                MainActivity.AndroidMainActivity.StartActivity(intent);

            }
            else
            {
                Android.Net.Uri uri = Android.Net.Uri.FromFile(apkFile);
                Intent intent = new Intent(Intent.ActionView);
                intent.SetDataAndType(uri, mimeType);
                intent.SetFlags(ActivityFlags.NewTask);
                MainActivity.AndroidMainActivity.StartActivity(intent);

            }

        }






    }
}