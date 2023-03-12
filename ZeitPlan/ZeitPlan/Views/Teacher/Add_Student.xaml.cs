
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ZeitPlan.Views.Teacher
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Add_Student : ContentPage
    {
        public Add_Student()
        {
            InitializeComponent();
            LoadData();
        }

        async void LoadData()
        {
            var firebaseList = (await App.firebaseDatabase.Child("TBL_CLASS").OnceAsync<TBL_CLASS>()).Select(x => new TBL_CLASS
            {

                CLASS_NAME = x.Object.CLASS_NAME,

            }).ToList();
            var refinedList = firebaseList.Select(x => x.CLASS_NAME).ToList();
            ddlClass.ItemsSource = refinedList;

        }
        private async void btnstudent_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtStName.Text) || string.IsNullOrEmpty(txtStEmail.Text) || string.IsNullOrEmpty(txtStPassword.Text))
                {
                    await DisplayAlert("Error", "please fill all the required fields try again", "ok");
                    return;
                }
                if (ddlClass.SelectedItem == null)
                {
                    await DisplayAlert("Error", "please select Class and try again", "ok");
                    return;
                }

                var check = (await App.firebaseDatabase.Child("TBL_STUDENT").OnceAsync<TBL_STUDENT>()).FirstOrDefault(x => x.Object.STUDENT_EMAIL == txtStEmail.Text);

                if (check != null)
                {
                    await DisplayAlert("Error", check.Object.STUDENT_EMAIL + "This Student is already Added .", "ok");
                    return;
                }
                LoadingInd.IsRunning = true;
                int LastID, NewID = 1;

                var LastRecord = (await App.firebaseDatabase.Child("TBL_STUDENT").OnceAsync<TBL_STUDENT>()).FirstOrDefault();
                if (LastRecord != null)
                {
                    LastID = (await App.firebaseDatabase.Child("TBL_STUDENT").OnceAsync<TBL_STUDENT>()).Max(a => a.Object.STUDENT_ID);
                    NewID = ++LastID;
                }
                var Class = (await App.firebaseDatabase.Child("TBL_CLASS").OnceAsync<TBL_CLASS>()).FirstOrDefault(x => x.Object.CLASS_NAME == ddlClass.SelectedItem.ToString());


                TBL_STUDENT s = new TBL_STUDENT()
                {
                    STUDENT_ID = NewID,
                    STUDENT_NAME = txtStName.Text,
                    STUDENT_EMAIL = txtStEmail.Text,
                    CLASS_FID = Class.Object.CLASS_ID,
                };

                await App.firebaseDatabase.Child("TBL_STUDENT").PostAsync(s);
                LoadingInd.IsRunning = false;
                await DisplayAlert("Success", "Student Added  ", "Ok");

            }
            catch (Exception ex)
            {
                LoadingInd.IsRunning = false;
                await DisplayAlert("Error", "Something went wrong Please try again later.\nError:" + ex.Message, "ok");
                return;
            }
        }

    }
}