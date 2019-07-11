using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserServiceReference;
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
            tblTitle.Title = $"Hoşgeldin {Share.Kullanıcı .username.ToUpper()} {Share.Kullanıcı.surname.ToUpper()}";
            txtNameandDescription.Text = $"{Share.Kullanıcı.username.ToUpper()} {Share.Kullanıcı.surname.ToUpper()}";
            txtNameandDescription.Detail = $"{Share.Kullanıcı.description.Substring(0, 1).ToUpper() + Share.Kullanıcı.description?.Substring(1).ToLower()}";
            txtEposta.Detail = Share.Kullanıcı.eposta;
            txtTarih.Detail = Share.Kullanıcı.BasTarihi.ToShortDateString();
            txtUsername.Detail = Share.Kullanıcı.username;
            txtTel.Detail = Share.Kullanıcı.tel;
            swAdmin.On = Share.Kullanıcı.MainAdmin;
            UserPermission.BindingContext = Share.Kullanıcı.userPermissionDTO;

        }
    }
}