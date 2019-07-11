using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace WinpackCross.DbModel
{
   public static class Extensions
    {
        public static string SafeGetString(this SqlDataReader dr ,int index)
        {            
            return dr.IsDBNull (index) ? string.Empty :dr.GetString(index);
        }
    }
}
