using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace WinpackCross.DbModel
{
  public   class DB_Machine
    {
        SqlConnection _Con = new SqlConnection();
        SqlConnectionStringBuilder _strbuild = new SqlConnectionStringBuilder();
        SqlConnectionStringBuilder stringBuilder
        {
            get 
            {
                _strbuild. DataSource = IP;
                _strbuild.InitialCatalog = "ITS_Client_" + işemriNo;
                _strbuild.UserID = "sa";
                _strbuild.Password = "63792958";
                _strbuild.ConnectTimeout = 10;
                return _strbuild;
            }
        }
        public string IP { get; set; }
        public int işemriNo { get; set; }
        public string  Makina { get; set; }
        public string ÜrünAdı { get; set; }
        public string SN { get; set; }
        public string LOT { get; set; }
        public DateTime SKT { get; set; }
        public string Etiket { get; set; }
        public string EtiketIP { get; set; }
        public string Kamera { get; set; }
        public string KameraIP { get; set; }
        public string İnkjet { get; set; }
        public string inkjetPort { get; set; }
        public bool  Durum { get; set; }
        public int Count
        {
            get
            {
                int dön = 0;
                if (string.IsNullOrEmpty(IP) & işemriNo == 0) return 0;
                try
                {
                    using (SqlCommand cmd = new SqlCommand(" select count(*) from FirmaÜrünClient", DBHelper.ConMachine(Con)))
                    {
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                dön = dr.GetInt32(0);
                            }
                        }

                    }
                }
                catch (Exception)
                {
                }
                return dön;
            }
        }
        public async Task<int> Counts()
        {
            return await  Task<int>.Run( () => 
            {

                int dön = 0;
                if (string.IsNullOrEmpty(IP) & işemriNo==0) return 0;
                try
                {
                    using (SqlCommand cmd = new SqlCommand(" select count(*) from FirmaÜrünClient", DBHelper.ConMachine(Con)))
                    {
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                dön = dr.GetInt32(0);
                            }
                        }

                    }
                }
                catch (Exception)
                {
                }
                return dön;

            });
        }

        public SqlConnection Con
        {
            get
            {
                _Con.ConnectionString = stringBuilder.ConnectionString;
                return _Con;
            }
        }
    }
}
