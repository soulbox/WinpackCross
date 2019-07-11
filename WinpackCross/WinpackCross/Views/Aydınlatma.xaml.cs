using System;
using System.Collections.Generic;
using System.Linq;
using WinpackCross.Utility;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static WinpackCross.Utility.PLCServiceManager;

namespace WinpackCross.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Aydınlatma : ContentPage
    {


        static PLCServiceReference.PLC ServicePLC;
        PLCServiceManager service = new PLCServiceManager(ServiceBase.ClientUri.PLCApi);
        public Aydınlatma()
        {
     
            InitializeComponent();
            this.Appearing += Aydınlatma_Appearing;
            System.Timers.Timer timer = new System.Timers.Timer(1000);
            timer.Elapsed += Timer_Elapsed;
            timer.Start();

#pragma warning disable CS0618 // Type or member is obsolete
            Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);
#pragma warning restore CS0618 // Type or member is obsolete
            //PlcLight.CycleLight += PlcLight_Cycle;
            //AydınlatmaLayout.BindingContext = PlcLight;



        }


        private async void Aydınlatma_Appearing(object sender, EventArgs e)
        {

            activator.IsRunning = true;
            activator.IsVisible = activator.IsRunning;
            ServicePLC = await service.GetStatusAsync();
            Depos.AddRange(new[]
            {
                new Işık(){Name="Kat1_1",imageName=ServicePLC.Kat1_1?LOn:LOff ,Text="Işık-1"},
                new Işık(){Name="Kat1_2",imageName=ServicePLC.Kat1_2?LOn:LOff ,Text="Işık-2"},
                new Işık(){Name="Kat1_3",imageName=ServicePLC.Kat1_3?LOn:LOff ,Text="Işık-3"},
                new Işık(){Name="Kat1_4",imageName=ServicePLC.Kat1_4?LOn:LOff ,Text="Işık-4"},
                new Işık(){Name="Kat1_5",imageName=ServicePLC.Kat1_5?LOn:LOff ,Text="Işık-5"},
                new Işık(){Name="Kat1_6",imageName=ServicePLC.Kat1_6?LOn:LOff ,Text="Işık-6"},
                new Işık(){Name="Kat1",imageName=ServicePLC.Kat1?LOn:LOff ,Text="Deponun Hepsi"},
                new Işık(){Name="Kat2_1",imageName=ServicePLC.Kat2_1?LOn:LOff ,Text="Işık-1"},
                new Işık(){Name="Kat2_2",imageName=ServicePLC.Kat2_2?LOn:LOff ,Text="Işık-2"},
                new Işık(){Name="Kat2_3",imageName=ServicePLC.Kat2_3?LOn:LOff ,Text="Işık-3"},
                new Işık(){Name="Kat2_4",imageName=ServicePLC.Kat2_4?LOn:LOff ,Text="Işık-4"},
                new Işık(){Name="Kat2_5",imageName=ServicePLC.Kat2_5?LOn:LOff ,Text="Işık-5"},
                new Işık(){Name="Kat2_6",imageName=ServicePLC.Kat2_6?LOn:LOff ,Text="Işık-6"},
                new Işık(){Name="Kat2_7",imageName=ServicePLC.Kat2_7?LOn:LOff ,Text="Işık-7"},
                new Işık(){Name="Kat2",imageName=ServicePLC.Kat2 ?LOn:LOff ,Text="İmalatın Hepsi"},
                new Işık(){Name="Kat3_1",imageName=ServicePLC.Kat3_1?LOn:LOff ,Text="Işık-1"},
                new Işık(){Name="Kat3_2",imageName=ServicePLC.Kat3_2?LOn:LOff ,Text="Işık-2"},
                new Işık(){Name="Kat3_3",imageName=ServicePLC.Kat3_3?LOn:LOff ,Text="Işık-3"},
                new Işık(){Name="Kat3_4",imageName=ServicePLC.Kat3_4?LOn:LOff ,Text="Işık-4"},
                new Işık(){Name="Kat3_5",imageName=ServicePLC.Kat3_5?LOn:LOff ,Text="Işık-5"},
                new Işık(){Name="Kat3_6",imageName=ServicePLC.Kat3_6?LOn:LOff ,Text="Işık-6"},
                new Işık(){Name="Kat3",imageName=ServicePLC.Kat3?LOn:LOff ,Text="Çatının Hepsi"},
                new Işık(){Name="Hepsi",imageName=ServicePLC.Hepsi?LOn:LOff ,Text="Bütün Bina"}
            });
            DepoView.BindingContext = Depos;
            activator.IsRunning = false;
            activator.IsVisible = activator.IsRunning;

        }

        const string LOn = "outline_flash_on_24";
        const string LOff = "outline_flash_off_24";

        class Işık
        {
            public string Text { get; set; }
            public string Name { get; set; }
            public string imageName { get; set; }
        }
        List<Işık> Depos = new List<Işık>();
        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            ImageButton[] Switchs = { Kat1,Kat1_1,Kat1_2,Kat1_3,Kat1_4,Kat1_5,Kat1_6,Kat2,Kat2_1,Kat2_2,Kat2_3,Kat2_4,Kat2_5,Kat2_6,Kat2_7
            ,Kat3,Kat3_1,Kat3_2,Kat3_3,Kat3_4,Kat3_5,Kat3_6,Hepsi};
            Switchs.ToList().ForEach(buton =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    ServicePLC = await service.GetStatusAsync();
                    string Name = buton.CommandParameter.ToString();
                    if ((bool)ServicePLC.GetValue(Name))
                    {
                        //Depos.FirstOrDefault(x => x.Name == Name).imageName = LOn;
                        buton.Source = LOn;
                    }
                    else
                    {
                        //Depos.FirstOrDefault(x => x.Name == Name).imageName = LOff;
                        buton.Source = LOff;
                    }
                });

            });
        }

        private async void ImageButton_Clicked(object sender, EventArgs e)
        {

            ImageButton btn = (ImageButton)sender;
            string Name = btn.CommandParameter.ToString();
            if ((bool)ServicePLC.GetValue(Name))
            {
                ServicePLC.SetValue(false, Name);
                var response = await service.SetPropValueAsync(Name, false);
                Timer_Elapsed(null, null);
                btn.Source = (bool)ServicePLC.GetValue(Name) ? LOn : LOff;
            }
            else
            {

                ServicePLC.SetValue(true, Name);
                var response = await service.SetPropValueAsync(Name, true);
                Timer_Elapsed(null, null);
                btn.Source = (bool)ServicePLC.GetValue(Name) ? LOn : LOff;
            }
        }
        private void SwitchCell_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {

        }
    }
}