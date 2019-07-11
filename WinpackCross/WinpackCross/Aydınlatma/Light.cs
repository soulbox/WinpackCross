using EasyModbus;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace WinpackCross.Aydınlatma
{
    public class Light
    {
        public delegate void ErrorHandler(string msg);
        public delegate void CycleHandler(Light Cycle);
        public event ErrorHandler ErrorEvent;
        public event CycleHandler Cycle;
        static readonly ModbusClient _modbus;
        static ModbusClient modbus = _modbus = _modbus ?? new ModbusClient("10.0.0.9", 502) { ConnectionTimeout = 1000 };
        static bool started = false;
        enum Adresler
        {
            BaseAdr = 2048,//delta plc
            Kat3_1 = BaseAdr + 512,
            Kat3_2 = BaseAdr + 513,
            Kat3_3 = BaseAdr + 514,
            Kat3_4 = BaseAdr + 515,
            Kat3_5 = BaseAdr + 516,
            Kat3_6 = BaseAdr + 517,
            Kat2_1 = BaseAdr + 521,
            Kat2_2 = BaseAdr + 522,
            Kat2_3 = BaseAdr + 523,
            Kat2_4 = BaseAdr + 524,
            Kat2_5 = BaseAdr + 525,
            Kat2_6 = BaseAdr + 526,
            Kat2_7 = BaseAdr + 527,
            Kat1_1 = BaseAdr + 531,
            Kat1_2 = BaseAdr + 532,
            Kat1_3 = BaseAdr + 533,
            Kat1_4 = BaseAdr + 534,
            Kat1_5 = BaseAdr + 535,
            Kat1_6 = BaseAdr + 536,
            FirstAdr = Kat3_1,
            LastAdr = Kat1_6,
            Quantity = (LastAdr - FirstAdr + 1)

        }
        //public static Boolean[] Coils, CoilsBuff;

        public static Boolean[] Coils = Coils ?? new Boolean[Adresler.Quantity.GetHashCode()];
        public static Boolean[] CoilsBuff = CoilsBuff ?? new Boolean[Adresler.Quantity.GetHashCode()];
        public Light()
        {
            if (started && modbus.Connected) return;
            try
            {
                modbus.Connect();
                started = true;
                Task.Factory.StartNew(() =>
               {
                   do
                   {

                       Coils = modbus.ReadCoils((int)Adresler.FirstAdr, (int)Adresler.Quantity);
                       if (!CoilsBuff.SequenceEqual(Coils))
                       {
                           CoilsBuff = Coils;
                           Cycle?.Invoke(this);                           
                       }
                   } while (modbus.Connected);
                   started = false;
                   ErrorEvent?.Invoke($"IP:{modbus.IPAddress}\nPLC'ye Bağlantı Koptu!");

               });
                Task.Delay(100).Wait();
            }
            catch (Exception)
            {
                started = false;
                ErrorEvent?.Invoke($"IP:{modbus.IPAddress}\nPLC'ye Bağlanılamadı.");
            }

        }

        public object this[string propertyName]
        {
            get
            {
                // probably faster without reflection:
                // like:  return Properties.Settings.Default.PropertyValues[propertyName] 
                // instead of the following
                Type myType = typeof(Light);
                PropertyInfo myPropInfo = myType.GetProperty(propertyName);
                return myPropInfo.GetValue(this, null);
            }
            set
            {
                Type myType = typeof(Light);
                PropertyInfo myPropInfo = myType.GetProperty(propertyName);
                myPropInfo.SetValue(this, value, null);

            }

        }
        public Boolean Kat1_1
        {
            get
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                return Coils[Index(GetAdres(PropName))];
            }
            set
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                modbus.WriteSingleCoil(GetAdres(PropName).GetHashCode(), value);
            }
        }
        public Boolean Kat1_2
        {
            get
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                return Coils[Index(GetAdres(PropName))];
            }
            set
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                modbus.WriteSingleCoil(GetAdres(PropName).GetHashCode(), value);
            }
        }
        public Boolean Kat1_3
        {
            get
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                return Coils[Index(GetAdres(PropName))];
            }
            set
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                modbus.WriteSingleCoil(GetAdres(PropName).GetHashCode(), value);
            }
        }
        public Boolean Kat1_4
        {
            get
            {

                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                return Coils[Index(GetAdres(PropName))];

            }
            set
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                modbus.WriteSingleCoil(GetAdres(PropName).GetHashCode(), value);
            }
        }
        public Boolean Kat1_5
        {
            get
            {

                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                return Coils[Index(GetAdres(PropName))];

            }
            set
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                modbus.WriteSingleCoil(GetAdres(PropName).GetHashCode(), value);
            }
        }
        public Boolean Kat1_6
        {
            get
            {

                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                return Coils[Index(GetAdres(PropName))];

            }
            set
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                modbus.WriteSingleCoil(GetAdres(PropName).GetHashCode(), value);
            }
        }
        public Boolean Kat2_1
        {
            get
            {

                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                return Coils[Index(GetAdres(PropName))];

            }
            set
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                modbus.WriteSingleCoil(GetAdres(PropName).GetHashCode(), value);
            }
        }
        public Boolean Kat2_2
        {
            get
            {

                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                return Coils[Index(GetAdres(PropName))];

            }
            set
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                modbus.WriteSingleCoil(GetAdres(PropName).GetHashCode(), value);
            }
        }
        public Boolean Kat2_3
        {
            get
            {

                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                return Coils[Index(GetAdres(PropName))];

            }
            set
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                modbus.WriteSingleCoil(GetAdres(PropName).GetHashCode(), value);
            }
        }
        public Boolean Kat2_4
        {
            get
            {

                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                return Coils[Index(GetAdres(PropName))];

            }
            set
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                modbus.WriteSingleCoil(GetAdres(PropName).GetHashCode(), value);
            }
        }
        public Boolean Kat2_5
        {
            get
            {

                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                return Coils[Index(GetAdres(PropName))];

            }
            set
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                modbus.WriteSingleCoil(GetAdres(PropName).GetHashCode(), value);
            }
        }
        public Boolean Kat2_6
        {
            get
            {

                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                return Coils[Index(GetAdres(PropName))];

            }
            set
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                modbus.WriteSingleCoil(GetAdres(PropName).GetHashCode(), value);
            }
        }
        public Boolean Kat2_7
        {
            get
            {

                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                return Coils[Index(GetAdres(PropName))];

            }
            set
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                modbus.WriteSingleCoil(GetAdres(PropName).GetHashCode(), value);
            }
        }
        public Boolean Kat3_1
        {
            get
            {

                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                return Coils[Index(GetAdres(PropName))];

            }
            set
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                modbus.WriteSingleCoil(GetAdres(PropName).GetHashCode(), value);
            }
        }
        public Boolean Kat3_2
        {
            get
            {

                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                return Coils[Index(GetAdres(PropName))];

            }
            set
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                modbus.WriteSingleCoil(GetAdres(PropName).GetHashCode(), value);
            }
        }
        public Boolean Kat3_3
        {
            get
            {

                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                return Coils[Index(GetAdres(PropName))];

            }
            set
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                modbus.WriteSingleCoil(GetAdres(PropName).GetHashCode(), value);
            }
        }
        public Boolean Kat3_4
        {
            get
            {

                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                return Coils[Index(GetAdres(PropName))];

            }
            set
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                modbus.WriteSingleCoil(GetAdres(PropName).GetHashCode(), value);
            }
        }
        public Boolean Kat3_5
        {
            get
            {

                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                return Coils[Index(GetAdres(PropName))];

            }
            set
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                modbus.WriteSingleCoil(GetAdres(PropName).GetHashCode(), value);
            }
        }
        public Boolean Kat3_6
        {
            get
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                return Coils[Index(GetAdres(PropName))];
            }
            set
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                modbus.WriteSingleCoil(GetAdres(PropName).GetHashCode(), value);
            }
        }
        public Boolean Kat1
        {
            get
            {
                bool[] Dizi = { Kat1_1, Kat1_2, Kat1_3, Kat1_4, Kat1_5, Kat1_6 };
                return Dizi.All(x => x);
            }
            set
            {
                Kat1_1 = value;
                Kat1_2 = value;
                Kat1_3 = value;
                Kat1_4 = value;
                Kat1_5 = value;
                Kat1_6 = value;
            }
        }
        public Boolean Kat2
        {
            get
            {
                bool[] Dizi = { Kat2_1, Kat2_2, Kat2_3, Kat2_4, Kat2_5, Kat2_6,Kat2_7  };
                return Dizi.All(x => x);
            }
            set
            {
                Kat2_1 = value;
                Kat2_2 = value;
                Kat2_3 = value;
                Kat2_4 = value;
                Kat2_5 = value;
                Kat2_6 = value;
                Kat2_7 = value;

            }
        }
        public Boolean Kat3
        {
            get
            {
                bool[] Dizi = { Kat3_1, Kat3_2, Kat3_3, Kat3_4, Kat3_5, Kat3_6 };
                return Dizi.All(x => x);
            }
            set
            {
                Kat3_1 = value;
                Kat3_2 = value;
                Kat3_3 = value;
                Kat3_4 = value;
                Kat3_5 = value;
                Kat3_6 = value;

            }
        }
        public Boolean Hepsi
        {
            get
            {
                bool[] Dizi = { Kat1, Kat2, Kat3 };
                return Dizi.All(x => x);
            }
            set
            {
                Kat1 = value;
                Kat2 = value;
                Kat3 = value;

            }
        }
        int Index(Adresler Data)
        {
            return Data.GetHashCode() - Adresler.FirstAdr.GetHashCode();
        }
        Adresler GetAdres(string str)
        {
            return (Adresler)Enum.Parse(typeof(Adresler), str);
        }

    }
    public static class Extension
    {
        //public static int GetHashCode(this Enum Data)
        //{
        //    return Convert.GetHashCode32(Data);
        //}
        //public static Enum ToSingleEnum(this Enum   data, string str)
        //{            
        //    return (Aydınlatma.Light.Adresler)Enum.Parse(typeof(Aydınlatma.Light.Adresler), str);
        //}
    }
}
