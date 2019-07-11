using System;
using System.Collections.Generic;
using System.Text;

namespace WinpackCross.DbModel
{
   public  class ITS_Users
    {
        public int Id { get; set; }
        public string username { get; set; }
        public string  password { get; set; }
        public string description { get; set; }
        public DateTime  Tarih { get; set; }
        public bool admin { get; set; }
        public string  adı { get; set; }
        public string soyadı { get; set; }
        public string  eposta { get; set; }
        public string tel { get; set; }
    }
}
