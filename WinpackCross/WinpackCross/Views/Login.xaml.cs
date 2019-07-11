using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WinpackCross
{
    using Plugin.Connectivity;
    using Plugin.Toasts;
    using System.Net;
    using UserServiceReference;
    using WinpackCross.DbModel;
    using WinpackCross.Utility;
    using static WinpackCross.Utility.ServiceBase;   
   
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {     

  
        public Login()
        {
            this.Appearing += MainPage_Appearing;
            InitializeComponent();
            status.Text = $"Powered By@Kadir Aygün v{DependencyService.Get<IUtility>().GetBuildNumber()}";
            txtKullanıcıAdı.Text = Settings.UserName;
            txtŞifresi.Text = Settings.Password;
            swHatırla.IsToggled = Settings.Hatırla;
            
            Utility.Updater.DownloadProgress += AutoUpdate_DownloadProgress;
            Utility.Updater.DownloadComplated += AutoUpdate_DownloadComplated;
        }
        bool comp = false;
        private async void AutoUpdate_DownloadComplated(string FilePath)
        {
            await WaitAndExecute(900, () =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    progressBar.IsVisible = false;
                    activator.IsRunning = false;

                    //await DisplayAlert("Winpack", "İndirildi", "Tamam")
                    //.ContinueWith(x =>
                    //{
                    //Device.OpenUri(new Uri(FilePath));
                    if (!comp)
                    {
                        comp = true;
                        isdownloading = false;
                        await DisplayAlert("Bilgi", $"Eğer Uygulama Güncellenemediyse;\n*Önce bu uygulamayı kaldırın.*İndirilenler klasöründe '{System.IO.Path.GetFileName(FilePath)} tekrar çalıştırın.'", "Tamam");
                        DependencyService.Get<Utility.IUtility>().ProccesStart(FilePath);
                    }
                    //});

                });
            });
        }
        bool isdownloading = false;
        private void AutoUpdate_DownloadProgress(long MaxLength, DownloadProgressChangedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                isdownloading = true;
                double getprogress = ((double)e.BytesReceived  * 100 / (double)MaxLength) / 100;
                await progressBar.ProgressTo(getprogress, 900, Easing.Linear);


            });
        }

        private async void MainPage_Appearing(object sender, EventArgs e)
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                var a = await DisplayAlert("Hata", "Lütfen internet Bağlantınızı Kontrol Edinizi!", "Tamam", "Vazgeç");
                if (a) return; else return;
            }

            if ( !(await  Utility.Updater.IsUpdateAsync()) & !comp & !isdownloading)
            {
                await WaitAndExecute(1000, async () =>
                {
                    var a = await DisplayAlert("Güncelleme", "Yeni Versiyon Memcut", "Güncelle", "Vazgeç");
                    if (a)
                    {
                        activator.IsRunning = true;
                        progressBar.IsVisible = true;
                        Utility.Updater.Download();
                    }

                });
            }

            if (Ping.PingHost("192.168.1.190", 1433))
            {
                txtbağlantı.Text = $"[Local]";
                imgBaglantı.Source = "wificonnected.png";
            }
            else if (!CrossConnectivity.Current.IsConnected)
                txtbağlantı.Text = $"Bağlantı Durumu";
            else
            {
                txtbağlantı.Text = $"[Network]";
                imgBaglantı.Source = "Network.png";
            }
        }



        protected async Task WaitAndExecute(int milisec, Action actionToExecute)

        {

            await Task.Delay(milisec);

            actionToExecute();

        }


        private bool isError = false;

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                await DisplayAlert("internet", "İnternet veya local wifi Bağlantısı Yok.", "Tamam");
                return;
            }
            if (Ping.PingHost("192.168.1.190", 1433))
            {
                var mypc = Ping.PingHost("192.168.1.115", 9005);
                var server = Ping.PingHost("192.168.1.190", 9005);
                if (!server && !mypc)
                {
                    await DisplayAlert("Durum", "Local Api Servisi Çalışmıyor.", "Tamam");
                    return;
                }
            }
            else
            {
                if (!Ping.PingHost(DBGlobal.GlobalIP, 1433))
                {
                    await DisplayAlert("Durum", "Global Api Bilgisi Alınamıyor.", "Tamam");
                    return;
                }


                if (!Ping.PingHost(DBGlobal.GetGlobalApi.İp, 9005))
                {
                    await DisplayAlert("Durum", "Global Api Servisi Çalışmıyor.", "Tamam");
                    return;

                }

            }
            isError = false;
            activator.IsRunning = true;
            Utility.ServiceManager<UserDTO> service1 = new Utility.ServiceManager<UserDTO>(ClientUri.UserApi);
            List<UserDTO> GelenKullanıcılar;
            try
            {
                GelenKullanıcılar = await service1.ListeleAsync(Utility.HttpType.POST);
            }
            catch (WebException ex)
            {
                if (ex.Status.ToString().Contains("ConnectFailure"))
                    await DisplayAlert("Durum", "Servis Bağlantısı Kurulamadı", "Tamam");
                activator.IsRunning = false;
                return;
            }

            Views.Share.Kullanıcı = GelenKullanıcılar.FirstOrDefault(x => x.username == txtKullanıcıAdı.Text && x.password == txtŞifresi.Text);
            if (Views.Share.Kullanıcı != null)
            {
                stackSuccess.IsVisible = true;
                if (Settings.Hatırla)
                {
                    Settings.UserName = txtKullanıcıAdı.Text;
                    Settings.Password = txtŞifresi.Text;
                }

                Utility.ServiceManager<UserPermissionDTO> service2 = new Utility.ServiceManager<UserPermissionDTO>(ClientUri.UserPerApi);
                var permissions = await service2.ListeleAsync(Utility.HttpType.POST);
                Views.Share.Kullanıcı.userPermissionDTO = permissions.Where(x => x.UserId == Views.Share.Kullanıcı.Id).ToArray();
                var mainpage = new Views.MasterPage();
            
                activator.IsRunning = false;
                await Navigation.PushModalAsync(mainpage, true);
                //Application.Current.MainPage = mainpage;

            }
            else
            {
                activator.IsRunning = false;
                await DisplayAlert("Hata", "Kullanıcı Adı veya şifre Hatalı!", "Tamam");
            }
        }



        private void SwHatırla_Toggled(object sender, ToggledEventArgs e)
        {
            Settings.Hatırla = e.Value;
        }

        private async void Updater(object sender, EventArgs e)
        {
            if (!await Utility.Updater.IsUpdateAsync() & !comp)
            {
                await WaitAndExecute(1000, async () =>
                {
                    var a = await DisplayAlert("Güncelleme", "Yeni Versiyon Memcut", "Güncelle", "Vazgeç");

                    if (a)
                    {
                        activator.IsRunning = true;
                        progressBar.IsVisible = true;
                        //Utility.AutoUpdate.Download();
                         Utility.Updater.Download();
                    }
                });

            }

            //"/storage/emulated/0/Download/com.winpack.WinpackCross.apk"
            //DependencyService.Get<Utility.IUtility>().ProccesStart("/storage/emulated/0/Download/com.winpack.WinpackCross.apk");

            //if (!Utility.AutoUpdate.isUpdate & !comp)
            //{

            //    await WaitAndExecute(1000, async () =>
            //    {
            //        var a = await DisplayAlert("Güncelleme", "Yeni Versiyon Memcut", "Güncelle", "Vazgeç");

            //        if (a)
            //        {
            //            activator.IsRunning = true;
            //            progressBar.IsVisible = true;
            //            Utility.AutoUpdate.Download();

            //        }
            //    });

            //}
        }
    }
}