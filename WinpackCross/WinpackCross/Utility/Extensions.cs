using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;

namespace WinpackCross.DbModel
{
    public static class Extensions
    {
        public static string SafeGetString(this SqlDataReader dr, int index)
        {
            return dr.IsDBNull(index) ? string.Empty : dr.GetString(index);
        }
    }
}
namespace WinpackCross.Utility
{
    using PLCServiceReference;
    public static class Extensions
    {
        public static long ToLong(this string Source)
        {
            return Convert.ToInt64(Source);
        }

        public static object GetValue(this PLC obj, string propertyName)
        {
            Type myType = typeof(PLC);
            PropertyInfo myPropInfo = myType.GetProperty(propertyName);
            return myPropInfo.GetValue(obj, null);
        }
        public static void SetValue(this PLC obj, object value, string propertyName)
        {
            Type myType = typeof(PLC);
            PropertyInfo myPropInfo = myType.GetProperty(propertyName);
            myPropInfo.SetValue(obj, value, null);
        }



    }
}