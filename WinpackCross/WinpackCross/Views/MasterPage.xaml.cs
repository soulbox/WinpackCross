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
    public partial class MasterPage : MasterDetailPage
    {
        public class MasterPageItem
        {
            public string Title { get; set; }
            public string Icon { get; set; }
            public Type TargetType { get; set; }
        }
        public List<MasterPageItem> menuList { get; set; }
        public MasterPage()
        {
            InitializeComponent();
            //IsPresented = false;
            //txtLabel.Text = $"WithR:{btnAydınlatma.WidthRequest}\nWith:{btnAydınlatma.Width}";
            menuList = new List<MasterPageItem>();
            // Adding menu items to menuList and you can define title ,page and icon  
            menuList.Add(new MasterPageItem()
            {
                Title = "Profil",
                Icon = "User.png",
                TargetType = typeof(UserPage)
            });
            menuList.Add(new MasterPageItem()
            {
                Title = "Aydınlatma",
                Icon = "Light_On_50px.png",
                TargetType = typeof(Aydınlatma)
            });
            menuList.Add(new MasterPageItem()
            {
                Title = "Üretim",
                Icon = "Uretim.png",
                TargetType = typeof(Üretim)
            });
            menuList.Add(new MasterPageItem()
            {
                Title = "Isı ve Nem",
                Icon = "IsiNem.png",
                TargetType = typeof(IsıNem)
            });
            menuList.Add(new MasterPageItem()
            {
                Title = "Asansör",
                Icon = "Elevator.png",
                TargetType = typeof(Asaönsör)
            });
            // Setting our list to be ItemSource for ListView in MainPage.xaml  
            navigationDrawerList.ItemsSource = menuList;
            // Initial navigation, this can be used for our home page  
            //var asd = new UserPage();
            
            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(Üretim)));
            
        }

        private void NavigationDrawerList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = (MasterPageItem)e.SelectedItem;
            Type page = item.TargetType;
            Detail = new NavigationPage((Page)Activator.CreateInstance(page));
            IsPresented = false;
        }
    }


}