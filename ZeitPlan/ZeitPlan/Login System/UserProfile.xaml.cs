using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZeitPlan.Models;

namespace ZeitPlan.LoginSystem
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserProfile : ContentPage
    {
        public UserProfile(users u)
        {
            InitializeComponent();
            lbluserid.Text = u.UsersId.ToString();
            lblName.Text = u.Name;
            lblEmail.Text = u.Email;
            lblPassword.Text = "*******";
            lblPhone.Text = u.Phone;

        }
    }
}