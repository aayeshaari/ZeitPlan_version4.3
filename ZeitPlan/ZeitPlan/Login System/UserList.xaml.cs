using ZeitPlan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Firebase.Database.Query;
using ZeitPlan.Views.Admin;

namespace ZeitPlan.LoginSystem
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UsersList : ContentPage
    {
        public UsersList()
        {
            InitializeComponent();

        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                LoadingInd.IsRunning = true;
                LoadData();
                LoadingInd.IsRunning = false;
            }
            catch (Exception ex)
            {
                LoadingInd.IsRunning = false;
                await DisplayAlert("Error", "Something went wrong, please  try again later" + ex.Message, "ok");
                return;
            }
          

        }

        async void LoadData()
        {
            DataList.ItemsSource = (await App.firebaseDatabase.Child("Users").OnceAsync<users>()).Select(x => new users
            { //user is replsced with category after i check the output
             
                UsersId = x.Object.UsersId,
                Name = x.Object.Name,
                Phone = x.Object.Phone,
                Password = x.Object.Password,
                Email = x.Object.Email,


            }).ToList();
        }

        private async void DataList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selected = e.Item as users;

            var item = (await App.firebaseDatabase.Child("Users").OnceAsync<users>()).FirstOrDefault(a => a.Object.UsersId == selected.UsersId);

            var choice = await DisplayActionSheet("Options", "Close", "Delete", "View", "Edit", "FAvoriate", "Archived");
                if (choice == "View")
            {
                await DisplayAlert("Detail", "" +
                    "\nUser ID:" + item.Object.UsersId +
                    " \nName:" + item.Object.Name +
                    "\nEmail:" + item.Object.Email +
                    "\nPassword:  *******" +
                    "\nPhone:" + item.Object.Phone +
                   "", "ok");
                await Navigation.PushAsync(new UserProfile(selected));
            }
            if (choice == "Delete")
            {
                var q = DisplayAlert("Confirmation", "Are you sure you want to delete" + item.Object.UsersId, "Yes", "No");
                if (await q)
                {
                     await App.firebaseDatabase.Child("Users").Child(item.Key).DeleteAsync();
                    LoadData();
                    await DisplayAlert("Confirmation", item.Object.UsersId + "Deleted permanently", "ok");
                }
            }
            if (choice == "Edit")
            { }
        }
    }
}