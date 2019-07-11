using System;
using System.Collections.Generic;
using System.Text;

namespace WinpackCross.DbModel
{
   public class SoviaUser
    {
        public int Id { get; set; }       
        public string description { get; set; }
        public string password { get; set; }
        public string username { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string eposta { get; set; }
        public string addres { get; set; }
        public string tel { get; set; }
        public bool MainAdmin { get; set; }
        public DateTime BasTarihi { get; set; }
        public UserPermission UserPermission { get; set; }
    }
    public class UserPermission
    {
        public bool Raporlama { get; set; }
        public bool Asansör { get; set; }
        public bool Aydınlatma { get; set; }
        public bool Alarm { get; set; }
        public bool Ayarlar { get; set; }
    }
}
