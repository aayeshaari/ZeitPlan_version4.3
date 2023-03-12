using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ZeitPlan.Views.User
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserHome : ContentPage
    {
        public UserHome()
        {
            InitializeComponent();
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new MainPage();
        }
    }
}