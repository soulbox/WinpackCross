using System;
using System.Net;
using WebDav;
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

            //MainPage = new NavigationPage(new  Login());
            MainPage = new Login();
            //MainPage = new Views.IsıNem ();



        }


        protected  override void OnStart()
        {

            // Handle when your app start


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
