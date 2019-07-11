using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinpackCross.DbModel
{
    public class DBHelper
    {
        public delegate void ConErrorHandler(ErrorType type, string msg);
        public static event ConErrorHandler Errors;

        public enum ErrorType
        {
            DataCollectorConError,
            ITSConError,
            MachineConError
        }
        static SqlConnection ConData
        {
            get
            {

                try
                {
                    if (DB_Collection.Con.State != System.Data.ConnectionState.Open) DB_Collection.Con.Open();
                    SqlConnection.ClearAllPools();
                    SqlConnection.ClearPool(DB_Collection.Con);
                    return DB_Collection.Con;
                }
                catch (Exception ex)
                {
#if (DEBUG)
                    Debug.WriteLine(ex.ToString());
#endif
                    Errors?.Invoke(ErrorType.DataCollectorConError, "Servere Bağlanılamadı.");
                    return null;
                }
            }
        }
        static SqlConnection ConITS
        {
            get
            {
                SqlConnection.ClearAllPools();
                SqlConnection.ClearPool(DB_ITS.Con);
                try
                {
                    if (DB_ITS.Con.State != System.Data.ConnectionState.Open) DB_ITS.Con.Open();
                    return DB_ITS.Con;
                }
                catch (Exception ex)
                {
#if (DEBUG)
                    Debug.WriteLine(ex.ToString());
#endif
                    Errors?.Invoke(ErrorType.DataCollectorConError, "Servere Bağlanılamadı");
                    return null;
                }
            }
        }
        public static SqlConnection ConMachine(SqlConnection Con)
        {
            SqlConnection.ClearAllPools();
            SqlConnection.ClearPool(Con);
            try
            {
                if (Con.State != System.Data.ConnectionState.Open) Con.Open();
                return Con;
            }
            catch (Exception ex)
            {
#if (DEBUG)
                Debug.WriteLine(ex.ToString());
#endif
                Errors?.Invoke(ErrorType.DataCollectorConError, "Makineye Bağlanılamadı");
                return null;
            }

        }
        public List<SoviaUser> KullanıcıListele
        {
            get
            {


                List<SoviaUser> dön = new List<SoviaUser>();
                if (ConData == null) return dön;
                #region Query
                string query = @"select
a.Id,
a.username,
a.password,
a.description,
a.name,
a.surname,
a.eposta,
a.address,
a.tel,
a.BasTarihi,
a.MainAdmin,
b.Raporlama,
b.Asansör,
b.Alarm,
b.Ayarlar,
b.Aydınlatma
from[User] a join UserPermission b on a.Id = b.UserId";
                #endregion
                using (SqlCommand cmd = new SqlCommand(query, ConData))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            dön.Add(new SoviaUser()
                            {
                                Id = dr.GetInt32(0),
                                username = dr.GetString(1),
                                password = dr.GetString(2),
                                description = dr.GetString(3),
                                name = dr.GetString(4),
                                surname = dr.GetString(5),
                                eposta = dr.GetString(6),
                                addres = dr.GetString(7),
                                tel = dr.GetString(8),
                                BasTarihi = dr.GetDateTime(9),
                                MainAdmin = dr.GetBoolean(10),
                                UserPermission = new UserPermission()
                                {

                                    Raporlama = dr.GetBoolean(11),
                                    Asansör = dr.GetBoolean(12),
                                    Alarm = dr.GetBoolean(13),
                                    Ayarlar = dr.GetBoolean(14),
                                    Aydınlatma = dr.GetBoolean(15)

                                }
                            });
                        }
                    }
                }

                return dön;
            }
        }
        public static  Task<List<DB_Machine>> MakineListele
        {
            get
            {
                #region Get
                List<DB_Machine> dön = new List<DB_Machine>();
                #region Query
                string Query = @"select * from işemriListele where işEmriID in (
select max(işEmriID) from işemriListele 
where MakinaIP  like '%192%'
group by MakinaIP )
order by Makina ";
                #endregion
                using (SqlCommand cmd = new SqlCommand(Query, ConITS))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            dön.Add(new DB_Machine()
                            {
                                işemriNo = dr.GetInt32(0),
                                Makina = dr.GetString(1),
                                ÜrünAdı = dr.GetString(2),
                                SN = dr.GetString(3),
                                LOT = dr.GetString(6),
                                SKT = dr.GetDateTime(7),
                                Durum = dr.GetBoolean(9),
                                IP = dr.GetString(10),
                                Etiket = dr.GetString(11),
                                EtiketIP = dr.GetString(12),
                                İnkjet = dr.GetString(13),
                                inkjetPort = dr.GetString(14),
                                Kamera = dr.GetString(15),
                                KameraIP = dr.GetString(16)

                            });
                        }
                    }
                }
                #endregion
                return Task.FromResult<List<DB_Machine>>(dön);
            }
        }

        public static int MakineDeployCount { get => MakineListele.Result.Sum(x => x.Count); }

    }
}
