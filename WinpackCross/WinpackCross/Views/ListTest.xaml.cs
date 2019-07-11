using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WinpackCross.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListTest : ContentPage
    {
        public ListTest()
        {
            InitializeComponent();
            var liste = new List<TesList>()
            {
                new TesList (){Icon="Temp.png",Label="Isı1",Value ="15,5°C" },
                new TesList (){Icon="humidity.png",Label="Nem1",Value ="%65 Rh" },
                new TesList (){Icon="Temp.png",Label="Isı2",Value ="15,5°C" },
                new TesList (){Icon="humidity.png",Label="Nem2",Value ="%65 Rh" },
                new TesList (){Icon="Temp.png",Label="Isı3",Value ="15,5°C" },
                new TesList (){Icon="humidity.png",Label="Nem3",Value ="%65 Rh" },
            };

            listes.ItemsSource=  liste;
        }
    }
    public class TesList
    {
        public string Icon { get; set; }
        public string Label { get; set; }
        public string Value { get; set; }


    }
}