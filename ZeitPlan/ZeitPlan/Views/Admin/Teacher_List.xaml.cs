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
    public partial class Teacher_List : ContentPage
    {
        public Teacher_List()
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
            DataList.ItemsSource = (await App.firebaseDatabase.Child("TBL_TEACHER").OnceAsync<TBL_TEACHER>()).Select(x => new TBL_TEACHER
            {
                TEACHER_ID = x.Object.TEACHER_ID,
                TEACHER_NAME = x.Object.TEACHER_NAME,
                TEACHER_ADDRESS = x.Object.TEACHER_ADDRESS,
                TEACHER_PASSWORD = x.Object.TEACHER_PASSWORD,
                TEACHER_EMAIL = x.Object.TEACHER_EMAIL,
                TEACHER_PHNO = x.Object.TEACHER_PHNO,
                DEPARTMENT_FID = x.Object.DEPARTMENT_FID,
                //TIMETABLE_FID=x.Object.TIMETABLE_FID,
                Image = x.Object.Image,


            }).ToList();
        }

        private async void DataList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selected = e.Item as TBL_TEACHER;

            var item = (await App.firebaseDatabase.Child("TBL_TEACHER").OnceAsync<TBL_TEACHER>()).FirstOrDefault(a => a.Object.TEACHER_ID == selected.TEACHER_ID);


            await Navigation.PushAsync(new Teacher_Detail(selected));

        }
    }
}