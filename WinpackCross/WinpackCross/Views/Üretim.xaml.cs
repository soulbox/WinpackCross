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

    using Utility;
    using MachineServiceReference;
    using static WinpackCross.Utility.ServiceBase;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Üretim : ContentPage
    {
        public static MachineServiceReference.DB_Machine Makine = null;
        private ServiceManager<DB_Machine> service;
        public Üretim()
        {            
            InitializeComponent();
            this.Appearing += Üretim_Appearing;

            service = new ServiceManager<MachineServiceReference.DB_Machine>(ClientUri.MachineApi);
            System.Timers.Timer timer = new System.Timers.Timer(30000);
            timer.Elapsed += Timer_Elapsed;
            timer.Start();

        }
        int count = 1;
        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
           {
               var liste = await service.ListeleAsync(HttpType.GET);
               navigationDrawerList.ItemsSource = liste;
               txtGenelToplam.Text = $"Genel Toplam: { liste.Sum(x => x.Count)}\nRefresh:{count}";
               //navigationDrawerList.ItemsSource = await DBHelper.MakineListele;
               //txtGenelToplam.Text = $"Genel Toplam: { DBHelper.MakineListele.Result.Sum(x => x.Count)}\nRefresh:{count}";
               count++;
           });
        }

        private  async void Üretim_Appearing(object sender, EventArgs e)
        {
            activator.IsRunning = true;
            var liste = await service.ListeleAsync(HttpType.GET);
            navigationDrawerList.ItemsSource = liste;
            txtGenelToplam.Text = $"Genel Toplam: {liste.Sum(x => x.Count)}";
            activator.IsRunning = false;
        }


        private async void NavigationDrawerList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;
            ListView lst = (ListView)sender;
            Makine = (DB_Machine)e.SelectedItem;
            await Navigation.PushAsync(new JobDetail());          
            lst.SelectedItem = null;
        }
    }
}