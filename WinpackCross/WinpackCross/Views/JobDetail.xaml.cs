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
	public partial class JobDetail : ContentPage
	{
        public JobDetail()
        {
            InitializeComponent();
            tblJob.BindingContext = Üretim.Makine;
            Durumu.Detail = string.Format("{0:Aktif;0;Pasif}", Üretim.Makine.Durum.GetHashCode());
            ihracat.Detail = string.Format("{0:Var;0;İhracat Yok}", Üretim.Makine.İhracat.GetHashCode());
            MFG.Detail= string.Format("{0:.;0;İhracat Yok}", Üretim.Makine.İhracat.GetHashCode());

        }
    }
}