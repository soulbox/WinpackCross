using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WinpackCross.Aydınlatma;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WinpackCross.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Aydınlatma : ContentPage
    {
        private static Light _PlcLight;
        Light PlcLight { get { return _PlcLight = _PlcLight ?? new Light(); } }

        public Aydınlatma()
        {
            InitializeComponent();

#pragma warning disable CS0618 // Type or member is obsolete
            Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);
#pragma warning restore CS0618 // Type or member is obsolete
            PlcLight.Cycle += PlcLight_Cycle;
            //AydınlatmaLayout.BindingContext = PlcLight;
            Depos.AddRange( new[]{
            new Işık(){Name="Kat1_1",imageName=PlcLight.Kat1_1?LOn:LOff ,Text="Işık-1"},
            new Işık(){Name="Kat1_2",imageName=PlcLight.Kat1_2?LOn:LOff ,Text="Işık-2"},
            new Işık(){Name="Kat1_3",imageName=PlcLight.Kat1_3?LOn:LOff ,Text="Işık-3"},
            new Işık(){Name="Kat1_4",imageName=PlcLight.Kat1_4?LOn:LOff ,Text="Işık-4"},
            new Işık(){Name="Kat1_5",imageName=PlcLight.Kat1_5?LOn:LOff ,Text="Işık-5"},
            new Işık(){Name="Kat1_6",imageName=PlcLight.Kat1_6?LOn:LOff ,Text="Işık-6"},
            new Işık(){Name="Kat1",imageName=PlcLight.Kat1?LOn:LOff ,Text="Deponun Hepsi"},
            new Işık(){Name="Kat2_1",imageName=PlcLight.Kat2_1?LOn:LOff ,Text="Işık-1"},
            new Işık(){Name="Kat2_2",imageName=PlcLight.Kat2_2?LOn:LOff ,Text="Işık-2"},
            new Işık(){Name="Kat2_3",imageName=PlcLight.Kat2_3?LOn:LOff ,Text="Işık-3"},
            new Işık(){Name="Kat2_4",imageName=PlcLight.Kat2_4?LOn:LOff ,Text="Işık-4"},
            new Işık(){Name="Kat2_5",imageName=PlcLight.Kat2_5?LOn:LOff ,Text="Işık-5"},
            new Işık(){Name="Kat2_6",imageName=PlcLight.Kat2_6?LOn:LOff ,Text="Işık-6"},
            new Işık(){Name="Kat2_7",imageName=PlcLight.Kat2_7?LOn:LOff ,Text="Işık-7"},
            new Işık(){Name="Kat2",imageName=PlcLight.Kat2 ?LOn:LOff ,Text="İmalatın Hepsi"},
            new Işık(){Name="Kat3_1",imageName=PlcLight.Kat3_1?LOn:LOff ,Text="Işık-1"},
            new Işık(){Name="Kat3_2",imageName=PlcLight.Kat3_2?LOn:LOff ,Text="Işık-2"},
            new Işık(){Name="Kat3_3",imageName=PlcLight.Kat3_3?LOn:LOff ,Text="Işık-3"},
            new Işık(){Name="Kat3_4",imageName=PlcLight.Kat3_4?LOn:LOff ,Text="Işık-4"},
            new Işık(){Name="Kat3_5",imageName=PlcLight.Kat3_5?LOn:LOff ,Text="Işık-5"},
            new Işık(){Name="Kat3_6",imageName=PlcLight.Kat3_6?LOn:LOff ,Text="Işık-6"},
            new Işık(){Name="Kat3",imageName=PlcLight.Kat3?LOn:LOff ,Text="Çatının Hepsi"},
            new Işık(){Name="Hepsi",imageName=PlcLight.Hepsi?LOn:LOff ,Text="Bütün Bina"}
        });
            DepoView .BindingContext  = Depos;

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
        private void PlcLight_Cycle(Light Cycle)
        {
            ImageButton[] Switchs = { Kat1,Kat1_1,Kat1_2,Kat1_3,Kat1_4,Kat1_5,Kat1_6,Kat2,Kat2_1,Kat2_2,Kat2_3,Kat2_4,Kat2_5,Kat2_6,Kat2_7
            ,Kat3,Kat3_1,Kat3_2,Kat3_3,Kat3_4,Kat3_5,Kat3_6,Hepsi};
            Switchs.ToList().ForEach(buton =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    string Name = buton.CommandParameter.ToString();
                    if ((bool)PlcLight[Name])
                    {                        
                        //Depos.FirstOrDefault(x => x.Name == Name).imageName = LOn;
                        buton.Source = LOn;                    }
                    else
                    {                       
                        //Depos.FirstOrDefault(x => x.Name == Name).imageName = LOff;
                        buton.Source = LOff;
                    }
                });

            });

        }
        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            
            ImageButton btn = (ImageButton)sender;
            string Name = btn.CommandParameter.ToString();
            if ((bool)PlcLight[Name])
            {
                PlcLight[Name] = false;             
                btn.Source = LOff;
            }
            else
            {
                PlcLight[Name] = true;
                btn.Source = LOn;
            }
        }
        private void SwitchCell_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {

        }
    }
}