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
    public partial class TeacherCourseAssign_Detail : ContentPage
    {
        public TeacherCourseAssign_Detail(TBL_TEACHER_COURSE_ASSIGN tc)
        {
            InitializeComponent();
            TeacherCourseAssignID.Text=tc.TEACHER_COURSE_ASSIGN_ID.ToString();
            CourseFID.Text = tc.CLASS_FID.ToString();
            TeacherFID.Text = tc.TEACHER_FID.ToString();
        }
    }
}