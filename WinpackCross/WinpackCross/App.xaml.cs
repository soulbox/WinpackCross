
using System;
using WinpackCross.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace WinpackCross
{
    public partial class App : Application
    {

        public App()
        {

            InitializeComponent();

            //if(CrossAutoLogin.Current.UserIsSaved)
            //MainPage = new MasterPage();
            //else
            MainPage = new NavigationPage(new MainPage());
            //MainPage = new NavigationPage(new MainPage());

        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
