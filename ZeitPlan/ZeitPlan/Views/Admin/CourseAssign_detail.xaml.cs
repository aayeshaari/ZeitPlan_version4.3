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
    public partial class CourseAssign_detail : ContentPage
    {
        public CourseAssign_detail(TBL_COURSE_ASSIGN ca)
        {
            InitializeComponent();
            CourseAssignID.Text = ca.COURSE_ASSIGN_ID.ToString();
            ClassFID.Text = ca.CLASS_FID.ToString();
            CourseFID.Text = ca.COURSE_FID.ToString();
        }
    }
}