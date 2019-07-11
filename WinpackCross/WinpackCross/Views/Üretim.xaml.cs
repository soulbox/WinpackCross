using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinpackCross.DbModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WinpackCross.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Üretim : ContentPage
	{
        DBHelper db = new DBHelper();
        public  Üretim ()
		{
            this.Appearing += Üretim_Appearing;
			InitializeComponent ();
            DBHelper.Errors += DBHelper_Errors;
            navigationDrawerList.ItemsSource = db.MakineListele.Result;
            
            System.Timers.Timer timer = new System.Timers.Timer(3000);
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
            
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
              
                navigationDrawerList.ItemsSource = await db.MakineListele;
            });
        }

        private void Üretim_Appearing(object sender, EventArgs e)
        {
            
        }

        private void DBHelper_Errors(DBHelper.ErrorType type, string msg)
        {
            Device.BeginInvokeOnMainThread(()=> 
            {
                DisplayAlert("Hata",msg,"Tamam");
            });
        }

        private void NavigationDrawerList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }
    }
}