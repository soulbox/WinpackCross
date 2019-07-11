using System;
using System.Collections.Generic;
using System.Text;

namespace WinpackCross.Utility
{
   public  interface IUtility
    {
        string GetVersionNumber();
        string GetBuildNumber();
        string Path { get; }
        void ProccesStart(string path);
    }
}
