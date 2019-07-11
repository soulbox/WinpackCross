using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace WinpackCross.DbModel
{
    public static class DBGlobal
    {
      public    const string GlobalIP = "213.14.174.241";       
        public  static SqlConnectionStringBuilder strbuild = new SqlConnectionStringBuilder()
        {
            DataSource = GlobalIP,
            InitialCatalog = "Winpower",
            UserID = "sa",
            Password = "63792958",
            MultipleActiveResultSets = true,
            PersistSecurityInfo = true
        };
        static SqlConnection Con = new SqlConnection(strbuild.ConnectionString);
        static SqlConnection ConData
        {
            get
            {

                try
                {
                    if (Con.State != System.Data.ConnectionState.Open) Con.Open();
                    SqlConnection.ClearAllPools();
                    SqlConnection.ClearPool(Con);
                    return Con;
                }
                catch (Exception ex)
                {
#if (DEBUG)
                    Debug.WriteLine(ex.ToString());
#endif

                    return null;
                }
            }
        }
        static GeneralDTO Global ;
        public static GeneralDTO GetGlobalApi
        {
            get
            {
                if (Global == null)
                {
                    List<GeneralDTO> dön = new List<GeneralDTO>();
                    string query = "Select * from General";

                    var asd = Ping.PingHost(Con.DataSource, 1433);
                    using (SqlCommand cmd = new SqlCommand(query, ConData))
                    {
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                dön.Add(new GeneralDTO()
                                {
                                    Id = dr.GetInt32(0),
                                    Açıklama = dr.GetString(1),
                                    İp = dr.GetString(2),
                                });
                            }
                        }
                    }
                    return dön.FirstOrDefault();
                }
                else
                {
                    return Global;
                }
                
            }
        }

    }
    public class GeneralDTO
    {
        public int Id { get; set; }
        public string Açıklama { get; set; }
        public string İp { get; set; }
    }
}
