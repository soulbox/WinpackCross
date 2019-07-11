using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ComponentModel;

namespace WinpackCross.Views
{
    using Utility;
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Asaönsör : ContentPage
    {




        private static AsansörCollection _collection;
        PLCServiceManager Service = new PLCServiceManager(ServiceBase.ClientUri.PLCApi);
        public static PLCServiceReference.PLC ServicePlc;

        public Asaönsör()
        {
            
            InitializeComponent();
            this.Appearing += Asaönsör_Appearing;


            System.Timers.Timer timer = new System.Timers.Timer(1000);
            timer.Start();
            timer.Elapsed += Timer_Elapsed;
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                ServicePlc = await Service.GetStatusAsync();
                Device.BeginInvokeOnMainThread(() =>
                {
                    _collection.Items.ToList().ForEach(x =>
                    {

                        switch (x.Title)
                        {
                            case "Çatı":
                                x.imgSource =
                                             ServicePlc.ÇatıKapıDurumu == PLCServiceReference.PLC.KapıDurumları.Açılıyor ? "OpenGarage.png" :
                                             ServicePlc.ÇatıKapıDurumu == PLCServiceReference.PLC.KapıDurumları.Kapanıyor ? "CloseGarage.png" :
                                             ServicePlc.ÇatıKapıDurumu == PLCServiceReference.PLC.KapıDurumları.boşta ? "GarageDoorPart.png" : "";

                                x.Durum = string.Format("{0:[Hazır];0;[Hazır Değil]},{1},{2}"
                                            , ServicePlc.AsansörHareketEdebilir.GetHashCode()
                                            , ServicePlc.ÇatıKapıDurumu == PLCServiceReference.PLC.KapıDurumları.Açılıyor ? "[Kapı Açılıyor]" :
                                              ServicePlc.ÇatıKapıDurumu == PLCServiceReference.PLC.KapıDurumları.Kapanıyor ? "[Kapı Kapanıyor]" :
                                              ServicePlc.ÇatıKapıDurumu == PLCServiceReference.PLC.KapıDurumları.boşta ? "[Kapı Boşta]" : ""
                                            , ServicePlc.Pozisyon == PLCServiceReference.PLC.Pozisyonlar.depo ? "[Depoda]" :
                                            ServicePlc.Pozisyon == PLCServiceReference.PLC.Pozisyonlar.depoimalat ? "[Depo-imalat Arası]" :
                                            ServicePlc.Pozisyon == PLCServiceReference.PLC.Pozisyonlar.imalat ? "[İmalatta]" :
                                            ServicePlc.Pozisyon == PLCServiceReference.PLC.Pozisyonlar.imalatçatı ? "[imalat-Çatı Arası]" :
                                            ServicePlc.Pozisyon == PLCServiceReference.PLC.Pozisyonlar.zemin ? "[Zeminde]" :
                                            ServicePlc.Pozisyon == PLCServiceReference.PLC.Pozisyonlar.zemindepo ? "[Zemin-Depo Arası]" :
                                            ServicePlc.Pozisyon == PLCServiceReference.PLC.Pozisyonlar.çatı ? "[Çatıda]" : "");

                                x.KapıDetail = ServicePlc.ÇatıKapıDurumu == PLCServiceReference.PLC.KapıDurumları.Açılıyor ? "Açılıyor" :
                                               ServicePlc.ÇatıKapıDurumu == PLCServiceReference.PLC.KapıDurumları.Kapanıyor ? "Kapanıyor" :
                                               ServicePlc.ÇatıKapıDurumu == PLCServiceReference.PLC.KapıDurumları.boşta ? "Boşta" : "";
                                break;
                            case "imalat":
                                x.imgSource =
                                              ServicePlc.İmalatKapıDurumu == PLCServiceReference.PLC.KapıDurumları.Açılıyor ? "OpenGarage.png" :
                                              ServicePlc.İmalatKapıDurumu == PLCServiceReference.PLC.KapıDurumları.Kapanıyor ? "CloseGarage.png" :
                                              ServicePlc.İmalatKapıDurumu == PLCServiceReference.PLC.KapıDurumları.boşta ? "GarageDoorPart.png" : "";

                                x.Durum = string.Format("{0:[Hazır];0;[Hazır Değil]},{1},{2}"
                                            , ServicePlc.AsansörHareketEdebilir.GetHashCode()
                                            , ServicePlc.İmalatKapıDurumu == PLCServiceReference.PLC.KapıDurumları.Açılıyor ? "[Kapı Açılıyor]" :
                                              ServicePlc.İmalatKapıDurumu == PLCServiceReference.PLC.KapıDurumları.Kapanıyor ? "[Kapı Kapanıyor]" :
                                              ServicePlc.İmalatKapıDurumu == PLCServiceReference.PLC.KapıDurumları.boşta ? "[Kapı Boşta]" : ""
                                            , ServicePlc.Pozisyon == PLCServiceReference.PLC.Pozisyonlar.depo ? "[Depoda]" :
                                            ServicePlc.Pozisyon == PLCServiceReference.PLC.Pozisyonlar.depoimalat ? "[Depo-imalat Arası]" :
                                            ServicePlc.Pozisyon == PLCServiceReference.PLC.Pozisyonlar.imalat ? "[İmalatta]" :
                                            ServicePlc.Pozisyon == PLCServiceReference.PLC.Pozisyonlar.imalatçatı ? "[imalat-Çatı Arası]" :
                                            ServicePlc.Pozisyon == PLCServiceReference.PLC.Pozisyonlar.zemin ? "[Zeminde]" :
                                            ServicePlc.Pozisyon == PLCServiceReference.PLC.Pozisyonlar.zemindepo ? "[Zemin-Depo Arası]" :
                                            ServicePlc.Pozisyon == PLCServiceReference.PLC.Pozisyonlar.çatı ? "[Çatıda]" : "");

                                x.KapıDetail = ServicePlc.İmalatKapıDurumu == PLCServiceReference.PLC.KapıDurumları.Açılıyor ? "Açılıyor" :
                                               ServicePlc.İmalatKapıDurumu == PLCServiceReference.PLC.KapıDurumları.Kapanıyor ? "Kapanıyor" :
                                               ServicePlc.İmalatKapıDurumu == PLCServiceReference.PLC.KapıDurumları.boşta ? "Boşta" : "";

                                break;
                            case "Depo":
                                x.imgSource =
                                            ServicePlc.DepoKapıDurumu == PLCServiceReference.PLC.KapıDurumları.Açılıyor ? "OpenGarage.png" :
                                            ServicePlc.DepoKapıDurumu == PLCServiceReference.PLC.KapıDurumları.Kapanıyor ? "CloseGarage.png" :
                                            ServicePlc.DepoKapıDurumu == PLCServiceReference.PLC.KapıDurumları.boşta ? "GarageDoorPart.png" : "";

                                x.Durum = string.Format("{0:[Hazır];0;[Hazır Değil]},{1},{2}"
                                            , ServicePlc.AsansörHareketEdebilir.GetHashCode()
                                            , ServicePlc.DepoKapıDurumu == PLCServiceReference.PLC.KapıDurumları.Açılıyor ? "[Kapı Açılıyor]" :
                                              ServicePlc.DepoKapıDurumu == PLCServiceReference.PLC.KapıDurumları.Kapanıyor ? "[Kapı Kapanıyor]" :
                                              ServicePlc.DepoKapıDurumu == PLCServiceReference.PLC.KapıDurumları.boşta ? "[Kapı Boşta]" : ""
                                            , ServicePlc.Pozisyon == PLCServiceReference.PLC.Pozisyonlar.depo ? "[Depoda]" :
                                            ServicePlc.Pozisyon == PLCServiceReference.PLC.Pozisyonlar.depoimalat ? "[Depo-imalat Arası]" :
                                            ServicePlc.Pozisyon == PLCServiceReference.PLC.Pozisyonlar.imalat ? "[İmalatta]" :
                                            ServicePlc.Pozisyon == PLCServiceReference.PLC.Pozisyonlar.imalatçatı ? "[imalat-Çatı Arası]" :
                                            ServicePlc.Pozisyon == PLCServiceReference.PLC.Pozisyonlar.zemin ? "[Zeminde]" :
                                            ServicePlc.Pozisyon == PLCServiceReference.PLC.Pozisyonlar.zemindepo ? "[Zemin-Depo Arası]" :
                                            ServicePlc.Pozisyon == PLCServiceReference.PLC.Pozisyonlar.çatı ? "[Çatıda]" : "");

                                x.KapıDetail = ServicePlc.DepoKapıDurumu == PLCServiceReference.PLC.KapıDurumları.Açılıyor ? "Açılıyor" :
                                               ServicePlc.DepoKapıDurumu == PLCServiceReference.PLC.KapıDurumları.Kapanıyor ? "Kapanıyor" :
                                               ServicePlc.DepoKapıDurumu == PLCServiceReference.PLC.KapıDurumları.boşta ? "Boşta" : "";

                                break;
                            case "Zemin":
                                x.imgSource =
                                            ServicePlc.ZeminKapıDurumu == PLCServiceReference.PLC.KapıDurumları.Açılıyor ? "OpenGarage.png" :
                                            ServicePlc.ZeminKapıDurumu == PLCServiceReference.PLC.KapıDurumları.Kapanıyor ? "CloseGarage.png" :
                                            ServicePlc.ZeminKapıDurumu == PLCServiceReference.PLC.KapıDurumları.boşta ? "GarageDoorPart.png" : "";

                                x.Durum = string.Format("{0:[Hazır];0;[Hazır Değil]},{1},{2}"
                                            , ServicePlc.AsansörHareketEdebilir.GetHashCode()
                                            , ServicePlc.ZeminKapıDurumu == PLCServiceReference.PLC.KapıDurumları.Açılıyor ? "[Kapı Açılıyor]" :
                                              ServicePlc.ZeminKapıDurumu == PLCServiceReference.PLC.KapıDurumları.Kapanıyor ? "[Kapı Kapanıyor]" :
                                              ServicePlc.ZeminKapıDurumu == PLCServiceReference.PLC.KapıDurumları.boşta ? "[Kapı Boşta]" : ""
                                            , ServicePlc.Pozisyon == PLCServiceReference.PLC.Pozisyonlar.depo ? "[Depoda]" :
                                            ServicePlc.Pozisyon == PLCServiceReference.PLC.Pozisyonlar.depoimalat ? "[Depo-imalat Arası]" :
                                            ServicePlc.Pozisyon == PLCServiceReference.PLC.Pozisyonlar.imalat ? "[İmalatta]" :
                                            ServicePlc.Pozisyon == PLCServiceReference.PLC.Pozisyonlar.imalatçatı ? "[imalat-Çatı Arası]" :
                                            ServicePlc.Pozisyon == PLCServiceReference.PLC.Pozisyonlar.zemin ? "[Zeminde]" :
                                            ServicePlc.Pozisyon == PLCServiceReference.PLC.Pozisyonlar.zemindepo ? "[Zemin-Depo Arası]" :
                                            ServicePlc.Pozisyon == PLCServiceReference.PLC.Pozisyonlar.çatı ? "[Çatıda]" : "");

                                x.KapıDetail = ServicePlc.ZeminKapıDurumu == PLCServiceReference.PLC.KapıDurumları.Açılıyor ? "Açılıyor" :
                                               ServicePlc.ZeminKapıDurumu == PLCServiceReference.PLC.KapıDurumları.Kapanıyor ? "Kapanıyor" :
                                               ServicePlc.ZeminKapıDurumu == PLCServiceReference.PLC.KapıDurumları.boşta ? "Boşta" : "";
                                break;
                            default:
                                break;
                        }


                    });
                    txtDurum.Text = string.Format("Asansör:{0} ", ServicePlc.Pozisyon);

                });
            });


            //throw new NotImplementedException();
        }

        private async void Asaönsör_Appearing(object sender, EventArgs e)
        {
            activator.IsRunning = true;
            activator.IsVisible = activator.IsRunning;
            ServicePlc = await Service.GetStatusAsync();
            _collection = new AsansörCollection();
            BindingContext = _collection;
            activator.IsRunning = false;
            activator.IsVisible = activator.IsRunning;
        }


        private async void btnCagirClicked(object sender, EventArgs e)
        {
            ServicePlc = await Service.GetStatusAsync();
            var butn = (Button)sender;
            switch (butn.CommandParameter.ToString())
            {
                case "Çatı":
                    if (!ServicePlc.AsansörHareketEdebilir)
                    {
                        await DisplayAlert("Hata", "Asansör Hareket Halinde!", "Tamam");
                        break;
                    }
                    if (ServicePlc.Pozisyon != PLCServiceReference.PLC.Pozisyonlar.çatı)
                    {
                        var msgResult = await DisplayAlert("Dikkat", $"Asansörü Çağırmaya Eminmisiniz?", "Tamam", "Vazgeç");
                        if (msgResult)
                            await Service.SetPropValueAsync("ÇatıyaGönder", true);
                        //ServicePlc.ÇatıyaGönder = true;
                        Timer_Elapsed(null, null);
                       
                    }
                    else
                    {
                        await DisplayAlert("Durum", "Asansör Çatıda!", "Tamam");
                    }
                    break;
                case "imalat":
                    if (!ServicePlc.AsansörHareketEdebilir)
                    {
                        await DisplayAlert("Hata", "Asansör Hareket Halinde!", "Tamam");
                        break;
                    }
                    if (ServicePlc.Pozisyon != PLCServiceReference.PLC.Pozisyonlar.imalat)
                    {
                        var msgResult = await DisplayAlert("Dikkat", $"Asansörü Çağırmaya Eminmisiniz?", "Tamam", "Vazgeç");
                        if (msgResult)
                            await Service.SetPropValueAsync("imalataGit", true);
                        Timer_Elapsed(null, null);
                        //ServicePlc.imalataGit = true;
                    }
                    else
                    {
                        await DisplayAlert("Durum", "Asansör İmalatta!", "Tamam");
                    }

                    break;
                case "Depo":
                    if (!ServicePlc.AsansörHareketEdebilir)
                    {
                        await DisplayAlert("Hata", "Asansör Hareket Halinde!", "Tamam");
                        break;
                    }
                    if (ServicePlc.Pozisyon != PLCServiceReference.PLC.Pozisyonlar.depo)
                    {
                        var msgResult = await DisplayAlert("Dikkat", $"Asansörü Çağırmaya Eminmisiniz?", "Tamam", "Vazgeç");
                        if (msgResult)
                            await Service.SetPropValueAsync("DepoyaGit", true);
                        Timer_Elapsed(null, null);
                        //ServicePlc.DepoyaGit = true;
                    }
                    else
                    {
                        await DisplayAlert("Durum", "Asansör Depoda!", "Tamam");

                    }

                    break;
                case "Zemin":
                    if (!ServicePlc.AsansörHareketEdebilir)
                    {
                        await DisplayAlert("Hata", "Asansör Hareket Halinde!", "Tamam");
                        break;
                    }
                    if (ServicePlc.Pozisyon != PLCServiceReference.PLC.Pozisyonlar.zemin)
                    {
                        var msgResult = await DisplayAlert("Dikkat", $"Asansörü Çağırmaya Eminmisiniz?", "Tamam", "Vazgeç");
                        if (msgResult)
                            await Service.SetPropValueAsync("ZemineGönder", true);
                        Timer_Elapsed(null, null);
                        //ServicePlc.ZemineGönder = true;
                    }
                    else
                    {
                        await DisplayAlert("Durum", "Asansör Zeminde.!", "Tamam");
                    }
                    break;
                default:
                    break;
            }
        }

        private async void btnKapıAç(object sender, EventArgs e)
        {
            ServicePlc = await Service.GetStatusAsync();

            var butn = (Button)sender;
            switch (butn.CommandParameter.ToString())
            {
                case "Çatı":
                    await Service.SetPropValueAsync("ÇatıKapıDurumu", PLCServiceReference.PLC.KapıDurumları.Açılıyor);
                    Timer_Elapsed(null, null);
                    ServicePlc.ÇatıKapıDurumu = PLCServiceReference.PLC.KapıDurumları.Açılıyor;
                    break;
                case "imalat":
                    await Service.SetPropValueAsync("İmalatKapıDurumu", PLCServiceReference.PLC.KapıDurumları.Açılıyor);
                    Timer_Elapsed(null, null);
                    ServicePlc.İmalatKapıDurumu = PLCServiceReference.PLC.KapıDurumları.Açılıyor;

                    break;
                case "Depo":
                    await Service.SetPropValueAsync("DepoKapıDurumu", PLCServiceReference.PLC.KapıDurumları.Açılıyor);
                    Timer_Elapsed(null, null);
                    ServicePlc.DepoKapıDurumu = PLCServiceReference.PLC.KapıDurumları.Açılıyor;

                    break;
                case "Zemin":
                    //Plc.ZeminKapıDurumu = PLC.PLC.KapıDurumları.Açılıyor;
                    break;
                default:
                    break;
            }
        }

        private async void btnKapıKapat(object sender, EventArgs e)
        {
            ServicePlc = await Service.GetStatusAsync();
            var butn = (Button)sender;
            switch (butn.CommandParameter.ToString())
            {
                case "Çatı":
                    await Service.SetPropValueAsync("ÇatıKapıDurumu", PLCServiceReference.PLC.KapıDurumları.Kapanıyor);
                    Timer_Elapsed(null, null);
                    ServicePlc.ÇatıKapıDurumu = PLCServiceReference.PLC.KapıDurumları.Kapanıyor;
                    break;
                case "imalat":
                    await Service.SetPropValueAsync("İmalatKapıDurumu", PLCServiceReference.PLC.KapıDurumları.Kapanıyor);
                    Timer_Elapsed(null, null);
                    ServicePlc.İmalatKapıDurumu = PLCServiceReference.PLC.KapıDurumları.Kapanıyor;

                    break;
                case "Depo":
                    await Service.SetPropValueAsync("DepoKapıDurumu", PLCServiceReference.PLC.KapıDurumları.Kapanıyor);
                    Timer_Elapsed(null, null);
                    ServicePlc.DepoKapıDurumu = PLCServiceReference.PLC.KapıDurumları.Kapanıyor;

                    break;
                case "Zemin":
                    //Plc.ZeminKapıDurumu = PLC.PLC.KapıDurumları.Açılıyor;
                    break;
                default:
                    break;
            }
        }
        private async void btnKapıDurdur(object sender, EventArgs e)
        {
            ServicePlc = await Service.GetStatusAsync();

            var butn = (Button)sender;
            switch (butn.CommandParameter.ToString())
            {
                case "Çatı":
                    await Service.SetPropValueAsync("ÇatıKapıDurumu", PLCServiceReference.PLC.KapıDurumları.boşta);
                    Timer_Elapsed(null, null);
                    ServicePlc.ÇatıKapıDurumu = PLCServiceReference.PLC.KapıDurumları.boşta;
                    break;
                case "imalat":
                    await Service.SetPropValueAsync("İmalatKapıDurumu", PLCServiceReference.PLC.KapıDurumları.Kapanıyor);
                    Timer_Elapsed(null, null);
                    ServicePlc.İmalatKapıDurumu = PLCServiceReference.PLC.KapıDurumları.boşta;

                    break;
                case "Depo":
                    await Service.SetPropValueAsync("DepoKapıDurumu", PLCServiceReference.PLC.KapıDurumları.Kapanıyor);
                    Timer_Elapsed(null, null);
                    ServicePlc.DepoKapıDurumu = PLCServiceReference.PLC.KapıDurumları.boşta;
                    break;
                case "Zemin":
                    //Plc.ZeminKapıDurumu = PLC.PLC.KapıDurumları.Açılıyor;
                    break;
                default:
                    break;
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {


        }

    }
    public class AsList : System.ComponentModel.INotifyPropertyChanged
    {
        private string _title, _imgsource, _kapıdetail, durum, _imginfo;
        private bool visible;
        public string Title { get => _title; set { _title = value; OnPropertyChanged("Title"); } }
        public string imgSource { get => _imgsource; set { _imgsource = value; OnPropertyChanged("imgSource"); } }
        public string KapıDetail { get => _kapıdetail; set { _kapıdetail = value; OnPropertyChanged("KapıDetail"); } }
        public string Durum { get => durum; set { durum = value; OnPropertyChanged("Durum"); } }
        public string imginfo { get => _imginfo; set { _imginfo = value; OnPropertyChanged("imginfo"); } } //= "Asinfo.png";
        public bool Visible { get => visible; set { visible = value; OnPropertyChanged("Visible"); } } //= true;       
        public event PropertyChangedEventHandler PropertyChanged;

        public AsList()
        {
            imginfo = "Asinfo.png";
            Visible = true;
        }
        private void OnPropertyChanged(string propname)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propname));
        }
    }
    public class AsansörCollection
    {
        PLCServiceReference.PLC ServicePLC { get { return Asaönsör.ServicePlc; } }
        public ObservableCollection<AsList> Items { get; set; }
        public AsansörCollection()
        {

            Items = new ObservableCollection<AsList>() {
                new AsList()
                {
                    Title ="Çatı",
                    imgSource =
                    ServicePLC.ÇatıKapıDurumu == PLCServiceReference.PLC.KapıDurumları.Açılıyor ? "OpenGarage.png":
                    ServicePLC.ÇatıKapıDurumu == PLCServiceReference.PLC.KapıDurumları.Kapanıyor ? "CloseGarage.png":
                    ServicePLC.ÇatıKapıDurumu == PLCServiceReference.PLC.KapıDurumları.boşta ? "GarageDoorPart.png":"",
                    Durum=string.Format("{0:[Hazır];0;[Hazır Değil]},{1},{2}"
                    ,ServicePLC.AsansörHareketEdebilir.GetHashCode()
                    ,ServicePLC.ÇatıKapıDurumu == PLCServiceReference.PLC.KapıDurumları.Açılıyor ? "[Kapı Açılıyor]":
                    ServicePLC.ÇatıKapıDurumu == PLCServiceReference.PLC.KapıDurumları.Kapanıyor ? "[Kapı Kapanıyor]":
                    ServicePLC.ÇatıKapıDurumu == PLCServiceReference.PLC.KapıDurumları.boşta ? "[Kapı Boşta]":""
                    ,ServicePLC.Pozisyon==PLCServiceReference.PLC.Pozisyonlar.depo ? "[Depoda]" :
                    ServicePLC.Pozisyon==PLCServiceReference.PLC.Pozisyonlar.depoimalat ? "[Depo-imalat Arası]" :
                    ServicePLC.Pozisyon==PLCServiceReference.PLC.Pozisyonlar.imalat ? "[İmalatta]":
                    ServicePLC.Pozisyon==PLCServiceReference.PLC.Pozisyonlar.imalatçatı ? "[imalat-Çatı Arası]" :
                    ServicePLC.Pozisyon==PLCServiceReference.PLC.Pozisyonlar.zemin ? "[Zeminde]" :
                    ServicePLC.Pozisyon==PLCServiceReference.PLC.Pozisyonlar.zemindepo ? "[Zemin-Depo Arası]" :
                    ServicePLC.Pozisyon==PLCServiceReference.PLC.Pozisyonlar.çatı ? "[Çatıda]":""),
                    KapıDetail=ServicePLC.ÇatıKapıDurumu == PLCServiceReference.PLC.KapıDurumları.Açılıyor ? "Açılıyor":
                    ServicePLC.ÇatıKapıDurumu == PLCServiceReference.PLC.KapıDurumları.Kapanıyor ? "Kapanıyor":
                    ServicePLC.ÇatıKapıDurumu == PLCServiceReference.PLC.KapıDurumları.boşta ? "Boşta":""  ,
                    imginfo="Asinfo.png"
                    },
                                new AsList()
                {
                    Title ="imalat",
                    imgSource =
                    ServicePLC.İmalatKapıDurumu  == PLCServiceReference.PLC.KapıDurumları.Açılıyor ? "OpenGarage.png":
                    ServicePLC.İmalatKapıDurumu == PLCServiceReference.PLC.KapıDurumları.Kapanıyor ? "CloseGarage.png":
                    ServicePLC.İmalatKapıDurumu == PLCServiceReference.PLC.KapıDurumları.boşta ? "GarageDoorPart.png":"",
                    Durum=string.Format("{0:[Hazır];0;[Hazır Değil]},{1},{2}"
                    ,ServicePLC.AsansörHareketEdebilir.GetHashCode()
                    ,ServicePLC.İmalatKapıDurumu == PLCServiceReference.PLC.KapıDurumları.Açılıyor ? "[Kapı Açılıyor]":
                    ServicePLC.İmalatKapıDurumu == PLCServiceReference.PLC.KapıDurumları.Kapanıyor ? "[Kapı Kapanıyor]":
                    ServicePLC.İmalatKapıDurumu == PLCServiceReference.PLC.KapıDurumları.boşta ? "[Kapı Boşta]":""
                    ,ServicePLC.Pozisyon==PLCServiceReference.PLC.Pozisyonlar.depo ? "[Depoda]" :
                    ServicePLC.Pozisyon==PLCServiceReference.PLC.Pozisyonlar.depoimalat ? "[Depo-imalat Arası]" :
                    ServicePLC.Pozisyon==PLCServiceReference.PLC.Pozisyonlar.imalat ? "[İmalatta]":
                    ServicePLC.Pozisyon==PLCServiceReference.PLC.Pozisyonlar.imalatçatı ? "[imalat-Çatı Arası]" :
                    ServicePLC.Pozisyon==PLCServiceReference.PLC.Pozisyonlar.zemin ? "[Zeminde]" :
                    ServicePLC.Pozisyon==PLCServiceReference.PLC.Pozisyonlar.zemindepo ? "[Zemin-Depo Arası]" :
                    ServicePLC.Pozisyon==PLCServiceReference.PLC.Pozisyonlar.çatı ? "[Çatıda]":""),
                    KapıDetail=ServicePLC.İmalatKapıDurumu == PLCServiceReference.PLC.KapıDurumları.Açılıyor ? "Açılıyor":
                    ServicePLC.İmalatKapıDurumu == PLCServiceReference.PLC.KapıDurumları.Kapanıyor ? "Kapanıyor":
                    ServicePLC.İmalatKapıDurumu == PLCServiceReference.PLC.KapıDurumları.boşta ? "Boşta":""  ,
                    imginfo="Asinfo.png"
                    },
                                new AsList()
                {
                    Title ="Depo",
                    imgSource =
                    ServicePLC.DepoKapıDurumu  == PLCServiceReference.PLC.KapıDurumları.Açılıyor ? "OpenGarage.png":
                    ServicePLC.DepoKapıDurumu == PLCServiceReference.PLC.KapıDurumları.Kapanıyor ? "CloseGarage.png":
                    ServicePLC.DepoKapıDurumu == PLCServiceReference.PLC.KapıDurumları.boşta ? "GarageDoorPart.png":"",
                    Durum=string.Format("{0:[Hazır];0;[Hazır Değil]},{1},{2}"
                    ,ServicePLC.AsansörHareketEdebilir.GetHashCode()
                    ,ServicePLC.DepoKapıDurumu == PLCServiceReference.PLC.KapıDurumları.Açılıyor ? "[Kapı Açılıyor]":
                    ServicePLC.DepoKapıDurumu == PLCServiceReference.PLC.KapıDurumları.Kapanıyor ? "[Kapı Kapanıyor]":
                    ServicePLC.DepoKapıDurumu == PLCServiceReference.PLC.KapıDurumları.boşta ? "[Kapı Boşta]":""
                    ,ServicePLC.Pozisyon==PLCServiceReference.PLC.Pozisyonlar.depo ? "[Depoda]" :
                    ServicePLC.Pozisyon==PLCServiceReference.PLC.Pozisyonlar.depoimalat ? "[Depo-imalat Arası]" :
                    ServicePLC.Pozisyon==PLCServiceReference.PLC.Pozisyonlar.imalat ? "[İmalatta]":
                    ServicePLC.Pozisyon==PLCServiceReference.PLC.Pozisyonlar.imalatçatı ? "[imalat-Çatı Arası]" :
                    ServicePLC.Pozisyon==PLCServiceReference.PLC.Pozisyonlar.zemin ? "[Zeminde]" :
                    ServicePLC.Pozisyon==PLCServiceReference.PLC.Pozisyonlar.zemindepo ? "[Zemin-Depo Arası]" :
                    ServicePLC.Pozisyon==PLCServiceReference.PLC.Pozisyonlar.çatı ? "[Çatıda]":""),
                    KapıDetail=ServicePLC.DepoKapıDurumu == PLCServiceReference.PLC.KapıDurumları.Açılıyor ? "Açılıyor":
                    ServicePLC.DepoKapıDurumu == PLCServiceReference.PLC.KapıDurumları.Kapanıyor ? "Kapanıyor":
                    ServicePLC.DepoKapıDurumu == PLCServiceReference.PLC.KapıDurumları.boşta ? "Boşta":""  ,
                    imginfo="Asinfo.png"
                    },
                                new AsList()
                {
                    Title ="Zemin",
                    imgSource =
                    ServicePLC.ZeminKapıDurumu  == PLCServiceReference.PLC.KapıDurumları.Açılıyor ? "OpenGarage.png":
                    ServicePLC.ZeminKapıDurumu == PLCServiceReference.PLC.KapıDurumları.Kapanıyor ? "CloseGarage.png":
                    ServicePLC.ZeminKapıDurumu == PLCServiceReference.PLC.KapıDurumları.boşta ? "GarageDoorPart.png":"",
                    Durum=string.Format("{0:[Hazır];0;[Hazır Değil]},{1},{2}"
                    ,ServicePLC.AsansörHareketEdebilir.GetHashCode()
                    ,ServicePLC.ZeminKapıDurumu == PLCServiceReference.PLC.KapıDurumları.Açılıyor ? "[Kapı Açılıyor]":
                    ServicePLC.ZeminKapıDurumu == PLCServiceReference.PLC.KapıDurumları.Kapanıyor ? "[Kapı Kapanıyor]":
                    ServicePLC.ZeminKapıDurumu == PLCServiceReference.PLC.KapıDurumları.boşta ? "[Kapı Boşta]":""
                    ,ServicePLC.Pozisyon==PLCServiceReference.PLC.Pozisyonlar.depo ? "[Depoda]" :
                    ServicePLC.Pozisyon==PLCServiceReference.PLC.Pozisyonlar.depoimalat ? "[Depo-imalat Arası]" :
                    ServicePLC.Pozisyon==PLCServiceReference.PLC.Pozisyonlar.imalat ? "[İmalatta]":
                    ServicePLC.Pozisyon==PLCServiceReference.PLC.Pozisyonlar.imalatçatı ? "[imalat-Çatı Arası]" :
                    ServicePLC.Pozisyon==PLCServiceReference.PLC.Pozisyonlar.zemin ? "[Zeminde]" :
                    ServicePLC.Pozisyon==PLCServiceReference.PLC.Pozisyonlar.zemindepo ? "[Zemin-Depo Arası]" :
                    ServicePLC.Pozisyon==PLCServiceReference.PLC.Pozisyonlar.çatı ? "[Çatıda]":""),
                    KapıDetail=ServicePLC.ZeminKapıDurumu == PLCServiceReference.PLC.KapıDurumları.Açılıyor ? "Açılıyor":
                    ServicePLC.ZeminKapıDurumu == PLCServiceReference.PLC.KapıDurumları.Kapanıyor ? "Kapanıyor":
                    ServicePLC.DepoKapıDurumu == PLCServiceReference.PLC.KapıDurumları.boşta ? "Boşta":""  ,
                    imginfo="Asinfo.png",
                    Visible=false
                    }};
        }


    }
}