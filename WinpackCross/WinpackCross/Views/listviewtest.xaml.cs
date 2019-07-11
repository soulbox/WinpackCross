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
	public partial class listviewtest : ContentPage
	{
		public listviewtest ()
		{
			InitializeComponent ();
            //List<Tester> asdg = new List<Tester>() { new Tester() { Texts = "Deneme"} };
            //listt.ItemsSource  = "ddddd";
		}
         class Tester
        {
             public string  Texts { get; set; }
        }
	}
}