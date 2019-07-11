//using Plugin.AutoLogin;
using Plugin.Toasts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinpackCross.DbModel;
using Xamarin.Forms;
//using System.IO;
//using System.IO.Ports;



namespace WinpackCross
{
    public partial class MainPage : ContentPage
    {

        public static SoviaUser  Kullanıcı { get; set; }
        
        DBHelper  db = new DBHelper();

        NotificationOptions NotfyOpt = new NotificationOptions()
        {
            Title = "The Title Line",
            Description = "The Description Content",
            IsClickable = true,

            WindowsOptions = new WindowsOptions() { LogoUri = "info.png" },
            ClearFromHistory = false,
            AllowTapInNotificationCenter = false,
            AndroidOptions = new AndroidOptions()
            {
                HexColor = "#F99D1C",
                ForceOpenAppOnNotificationTap = true
            }
            
        };
        NotificationOptions NotfyOptions(string title, string description)
        {
            NotfyOpt.Title = title;
            NotfyOpt.Description = description;
            
            return NotfyOpt;
        }

        public MainPage()
        {

            InitializeComponent();
            //this.InitializeComponent();
            //this.BindingContext = this;
            ////activator.BindingContext = loading;
            //txtKullanıcıAdı.Focused += TxtKullanıcıAdı_Focused;
            //activator.BindingContext = loading;
            this.Appearing += MainPage_Appearing;
            txtKullanıcıAdı.Text = Settings.UserName;
            txtŞifresi.Text = Settings.Password;
            swHatırla.IsToggled = Settings.Hatırla;
           DBHelper.Errors += Db_ConClosed;
            Ping.PingError += Ping_PingError;
            
        }

        private void MainPage_Appearing(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
           {

               if (Ping.PingHost("10.0.0.9", 502))
                   imgPLC.Source = "Connected.png";
               else
                   imgPLC.Source = "Disconnected.png";

               //await WaitAndExecute(1000,  () => imgPLC .Source= "Connected.png");

               if (Ping.PingHost("192.168.1.190", 1433))
                   imgSQL.Source = "Connected.png";
               else
                   imgSQL.Source = "Disconnected.png";

               //await WaitAndExecute(1000,  () =>  imgSQL .Source = "Connected.png");

           });
        }
        protected async Task WaitAndExecute(int milisec, Action actionToExecute)

        {

            await Task.Delay(milisec);

            actionToExecute();

        }

        private void Ping_PingError(string msg)
        {
            
            //Device.BeginInvokeOnMainThread( async () =>
            //{
            //  await   DisplayAlert("Hata", msg, "Tamam");
            //});
        }

        bool isError = false;
        private void Db_ConClosed(DBHelper.ErrorType type ,string msg)
        {
            isError = true;
            //DisplayAlert("Hata", "Bağlantı Hatası!", "Tamam");
            //ShowToast(NotfyOptions("Hata", "Bağlantı Hatası!"));
            Device.BeginInvokeOnMainThread(async () =>
            {
                await DisplayAlert("Hata", $"Bağlantı Hatası!", "Tamam");
            });

        }
        void ShowToast(INotificationOptions options)
        {
            var notificator = DependencyService.Get<IToastNotificator>();

            // await notificator.Notify(options);

            notificator.Notify((INotificationResult result) =>
            {
                System.Diagnostics.Debug.WriteLine("Notification [" + result.Id + "] Result Action: " + result.Action);
            }, options);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            isError = false;
            activator.IsRunning = true;
            Task.Run(async () =>
            {
                Kullanıcı = db.KullanıcıListele
                .FirstOrDefault(x => x.username == txtKullanıcıAdı.Text && x.password == txtŞifresi.Text);
                if (Kullanıcı != null)
                {
                    if (Settings.Hatırla)
                    {
                        Settings.UserName = txtKullanıcıAdı.Text;
                        Settings.Password = txtŞifresi.Text;
                    }
                    var mainpage = new Views.MasterPage();
                    App.Current.MainPage = mainpage;
                    await Navigation.PushModalAsync(mainpage, true);
                }
                else if (!isError)
                {
                    //ShowToast(NotfyOptions("Hata", "Kullanıcı Adı veya şifre Hatalı!"));
                    Device.BeginInvokeOnMainThread(async ()=> 
                    {
                        await DisplayAlert("Hata", "Kullanıcı Adı veya şifre Hatalı!","Tamam");
                    });

                }

            }).ContinueWith(x => { activator.IsRunning = false; });

        }



        private void SwHatırla_Toggled(object sender, ToggledEventArgs e)
        {
            Settings.Hatırla = e.Value;
        }
    }
}
