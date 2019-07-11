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
    public partial class UserPage : ContentPage
    {
        public UserPage()
        {

            InitializeComponent();
            tblTitle.Title = $"Hoşgeldin {MainPage.Kullanıcı.username .ToUpper()} {MainPage.Kullanıcı.surname .ToUpper()}";
            txtNameandDescription.Text = $"{MainPage.Kullanıcı.username.ToUpper()} {MainPage.Kullanıcı.surname.ToUpper()}";
            txtNameandDescription.Detail = $"{MainPage.Kullanıcı.description.Substring(0, 1).ToUpper() + MainPage.Kullanıcı.description?.Substring(1).ToLower()}";
            txtEposta.Detail = MainPage.Kullanıcı.eposta;
            txtTarih.Detail = MainPage.Kullanıcı.BasTarihi.ToShortDateString();
            txtUsername.Detail = MainPage.Kullanıcı.username;
            txtTel.Detail = MainPage.Kullanıcı.tel;
            swAdmin.On = MainPage.Kullanıcı.MainAdmin ;

        }
    }
}