using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using WinpackCross.iOS;

[assembly: Xamarin.Forms.Dependency(typeof(Utility))]
namespace WinpackCross.iOS
{
    public class Utility : WinpackCross.Utility.IUtility
    {
        public string Path => throw new NotImplementedException();

        public string GetBuildNumber()
        {

            return NSBundle.MainBundle.InfoDictionary["CFBundleShortVersionString"].ToString();
        }

        public string GetVersionNumber()
        {
            throw new NotImplementedException();
        }

        public void ProccesStart(string path)
        {
            throw new NotImplementedException();
        }
    }
}