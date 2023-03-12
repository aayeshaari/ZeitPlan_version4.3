using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ZeitPlan.Views.Admin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Student_List : ContentPage
    {
        public Student_List()
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
            DataList.ItemsSource = (await App.firebaseDatabase.Child("TBL_STUDENT").OnceAsync<TBL_STUDENT>()).Select(x => new TBL_STUDENT
            {
                STUDENT_ID = x.Object.STUDENT_ID,
                STUDENT_NAME = x.Object.STUDENT_NAME,
                STUDENT_EMAIL = x.Object.STUDENT_EMAIL,
                STUDENT_PASSWORD = x.Object.STUDENT_PASSWORD,
                CLASS_FID = x.Object.CLASS_FID,



            }).ToList();
        }


        private async void DataList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var Selected = e.Item as TBL_STUDENT;

            var item = (await App.firebaseDatabase.Child("TBL_STUDENT").OnceAsync<TBL_STUDENT>()).FirstOrDefault(a => a.Object.STUDENT_ID == Selected.STUDENT_ID);

            // var choice = await DisplayActionSheet("Options", "Close", "Delete", "View", "Edit", "FAvoriate", "Archived");


            await Navigation.PushAsync(new Student_Detail(Selected));


        }
    }
}