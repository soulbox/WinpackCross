using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WinpackCross.Views
{
    using PLCServiceReference;
    using System.Timers;
    using Utility;
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IsıNem : ContentPage
    {

        PLCServiceManager service;
        public PLC Plc { get; set; }
        private ObservableCollection<Group> _List;
        public ObservableCollection<Group> Liste
        {
            get
            {
                return _List;
            }
            set
            {
                _List = value;
                base.OnPropertyChanged();
            }
        }

        public IsıNem()
        {
            InitializeComponent();
            this.Appearing += IsıNem_Appearing;
            service = new PLCServiceManager(ServiceBase.ClientUri.PLCApi);
            System.Timers.Timer timer = new System.Timers.Timer(1000);
            timer.Start();
            timer.Elapsed += Timer_Elapsed;

        }


        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
           {
               Plc = await service.GetStatusAsync();
               Liste.ToList().ForEach(x =>
               {
                   x.ToList().ForEach(a =>
                   {
                       switch (a.Id)
                       {
                           case 1:
                               a.Isı = Plc.Kat1_1Isı;
                               a.Nem = Plc.Kat1_1Nem;
                               break;
                           case 2:
                               a.Isı = Plc.Kat1_2Isı;
                               a.Nem = Plc.Kat1_2Nem;
                               break;
                           case 3:
                               a.Isı = Plc.Kat1_3Isı;
                               a.Nem = Plc.Kat1_3Nem;
                               break;
                           case 4:
                               a.Isı = Plc.Kat1_4Isı;
                               a.Nem = Plc.Kat1_4Nem;
                               break;
                           case 5:
                               a.Isı = Plc.Kat1_5Isı;
                               a.Nem = Plc.Kat1_5Nem;
                               break;
                           case 6:
                               a.Isı = Plc.Kat2_1Isı;
                               a.Nem = Plc.Kat2_1Nem;
                               break;
                           case 7:
                               a.Isı = Plc.Kat2_2Isı;
                               a.Nem = Plc.Kat2_2Nem;
                               break;
                           case 8:
                               a.Isı = Plc.Kat2_3Isı;
                               a.Nem = Plc.Kat2_3Nem;
                               break;
                           case 9:
                               a.Isı = Plc.Kat2_4Isı;
                               a.Nem = Plc.Kat2_4Nem;
                               break;
                           case 10:
                               a.Isı = Plc.Kat3_1Isı;
                               a.Nem = Plc.Kat3_1Nem;
                               break;
                           default:
                               break;
                       }
                   });
               });

           });
        }

        private async void IsıNem_Appearing(object sender, EventArgs e)
        {
            activator.IsRunning = true;
            activator.IsVisible = activator.IsRunning;

            Plc = await service.GetStatusAsync();
            Liste = new ObservableCollection<Group> {
                new Group(Katlar.Depo)
                {
                new ViewIsıNem() { Bölge="1.Bölge",Isı=Plc.Kat1_1Isı ,Nem=Plc.Kat1_1Nem,Id=1},
                new ViewIsıNem() { Bölge="B/A Depo",Isı=Plc.Kat1_2Isı,Nem=Plc.Kat1_2Nem,Id=2},
                new ViewIsıNem() { Bölge="3.Bölge",Isı=Plc.Kat1_3Isı,Nem=Plc.Kat1_3Nem,Id=3},
                new ViewIsıNem() { Bölge="4.Bölge",Isı=Plc.Kat1_4Isı,Nem=Plc.Kat1_4Nem,Id=4},
                new ViewIsıNem() { Bölge="5.Bölge",Isı=Plc.Kat1_5Isı,Nem=Plc.Kat1_5Nem,Id=5}
                },
                new Group(Katlar.İmalat)
                {
                new ViewIsıNem() { Bölge="1.Bölge",Isı=Plc.Kat2_1Isı,Nem=Plc.Kat2_1Nem,Id=6},
                new ViewIsıNem() { Bölge="2.Bölge",Isı=Plc.Kat2_2Isı,Nem=Plc.Kat2_2Nem,Id=7},
                new ViewIsıNem() { Bölge="3.Bölge",Isı=Plc.Kat2_3Isı,Nem=Plc.Kat2_3Nem,Id=8},
                new ViewIsıNem() { Bölge="4.Bölge",Isı=Plc.Kat2_4Isı,Nem=Plc.Kat2_4Nem,Id=9}
                },
                new Group(Katlar.Çatı)
                {
                new ViewIsıNem() { Bölge="1.Bölge",Isı=Plc.Kat3_1Isı,Nem=Plc.Kat3_1Nem,Id=10}
                }
            };


            listes.ItemsSource = Liste;
            activator.IsRunning = false;
            activator.IsVisible = activator.IsRunning;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Liste.LastOrDefault().LastOrDefault().Isı = 222f;
            Liste.LastOrDefault().LastOrDefault().Nem = 222f;

        }
    }


    public class ViewIsıNem : INotifyPropertyChanged
    {
        const string ısıico = "Temp.png";
        const string nemico = "humidity.png";
        private string bölge;
        private string ısıicon = ısıico;
        private string nemicon = nemico;
        private float ısı = 10f;
        private float nem = 10f;
        public int Id { get; set; }
        public string Bölge { get => bölge; set { bölge = value; OnPropertyChanged("Bölge"); } }
        public float Isı { get => ısı; set { ısı = value; OnPropertyChanged("Isı"); } }
        public float Nem { get => nem; set { nem = value; OnPropertyChanged("Nem"); } }
        public string Isicon { get => ısıicon; set { ısıicon = value; OnPropertyChanged("Isıicon"); } }
        public string Nemicon { get => nemicon; set { nemicon = value; OnPropertyChanged("Nemicon"); } }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propname)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propname));
        }
    }

    public class Group : ObservableCollection<ViewIsıNem>
    {
        public Group(Katlar kat)
        {
            Kat = kat.ToString();
        }
        public string Kat { get; private set; }
    }
    public enum Katlar
    {
        Depo,
        İmalat,
        Çatı

    }
}