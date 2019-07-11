using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace WinpackCross.DbModel
{
   public static class DB_Collection
    {
        static SqlConnectionStringBuilder _ConStr;
        static SqlConnectionStringBuilder ConStr = _ConStr= _ConStr ?? new SqlConnectionStringBuilder()
        {
            DataSource = "192.168.1.190",
            InitialCatalog = "DataCollector",
            UserID = "sa",
            Password = "63792958",
            ConnectTimeout = 10
        };
        static SqlConnection _con ;
        public static SqlConnection Con
        {
            get => _con=_con ?? new SqlConnection(ConStr.ConnectionString);
        }
    }
}
