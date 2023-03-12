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
    public partial class Add_Class : ContentPage
    {
        public Add_Class()
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
            var firebaseList= (await App.firebaseDatabase.Child("TBL_DEGREE").OnceAsync<TBL_DEGREE>()).Select(x => new TBL_DEGREE
            {
               
                DEGREE_NAME = x.Object.DEGREE_NAME,
    
            }).ToList();
            var refinedList = firebaseList.Select(x => x.DEGREE_NAME).ToList();
            ddlDegree.ItemsSource = refinedList;

            //DEPARTMENT_FID
            //var firebaseList1 = (await App.firebaseDatabase.Child("TBL_DEPARTMENT").OnceAsync<TBL_DEPARTMENT>()).Select(x => new TBL_DEPARTMENT
            //{

            //    DEPARTMENT_NAME = x.Object.DEPARTMENT_NAME,

            //}).ToList();
            //var refinedList1 = firebaseList1.Select(x => x.DEPARTMENT_NAME).ToList();
            //ddlDepartment.ItemsSource = refinedList1;

            //Course_Assign_fid
            //var firebaseList2 = (await App.firebaseDatabase.Child("TBL_COURSE_ASSIGN").OnceAsync<TBL_COURSE_ASSIGN>()).Select(x => new TBL_COURSE_ASSIGN
            //{

            //    COURSE_ASSIGN_ID = x.Object.COURSE_ASSIGN_ID,

            //}).ToList();
            //var refinedList2 = firebaseList2.Select(x => x.COURSE_ASSIGN_ID).ToList();
            //ddlcourseassign.ItemsSource = refinedList2;

        }
        private async void btnClass_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtClName.Text) || string.IsNullOrEmpty(txtClSession.Text) || string.IsNullOrEmpty(txtClSection.Text) || string.IsNullOrEmpty(txtClShift.Text))
                {
                    await DisplayAlert("Error", "please fill all the required fields try again", "ok");
                    return;
                }
                if(ddlDegree.SelectedItem==null)
                {
                    await DisplayAlert("Error", "please select Degree and try again", "ok");
                    return;
                }
                //if (ddlDepartment.SelectedItem == null)
                //{
                //    await DisplayAlert("Error", "please select Department and try again", "ok");
                //    return;
                //}
                //if (ddlcourseassign.SelectedItem == null)
                //{
                //    await DisplayAlert("Error", "please select  and try again", "ok");
                //    return;
                //}
                //if (ddlDegree.SelectedItem == null)
                //{
                //    await DisplayAlert("Error", "please select Degree and try again", "ok");
                //    return;
                //}

                var check = (await App.firebaseDatabase.Child("TBL_ClASS").OnceAsync<TBL_CLASS>()).FirstOrDefault(x => x.Object.CLASS_NAME == txtClName.Text);

                if (check != null)
                {
                    await DisplayAlert("Error", check.Object.CLASS_NAME + "This Session is already Picked .", "ok");
                    return;
                }
                LoadingInd.IsRunning = true;
                int LastID, NewID = 1;

                var LastRecord = (await App.firebaseDatabase.Child("TBL_CLASS").OnceAsync<TBL_CLASS>()).FirstOrDefault();
                if (LastRecord != null)
                {
                    LastID = (await App.firebaseDatabase.Child("TBL_CLASS").OnceAsync<TBL_CLASS>()).Max(a => a.Object.CLASS_ID);
                    NewID = ++LastID;
                }
                var Degree = (await App.firebaseDatabase.Child("TBL_DEGREE").OnceAsync<TBL_DEGREE>()).FirstOrDefault(x => x.Object.DEGREE_NAME == ddlDegree.SelectedItem.ToString());




                TBL_CLASS cs = new TBL_CLASS()
                {
                    CLASS_ID = NewID,
                    CLASS_NAME=txtClName.Text,
                    SEMESTER=txtClSemester.Text,
                    SESSION = txtClSession.Text,
                    SECTION = txtClSession.Text,
                    SHIFT = txtClSession.Text,
                   DEGREE_FID= Degree.Object.DEGREE_ID,
                   //DEPARTMENT_FID=selected1,
                   // COURSE_ASSIGN_FID=selected2,

                };

                await App.firebaseDatabase.Child("TBL_CLASS").PostAsync(cs);
                LoadingInd.IsRunning = false;
                await DisplayAlert("Success", "Class Added  ", "Ok");

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
