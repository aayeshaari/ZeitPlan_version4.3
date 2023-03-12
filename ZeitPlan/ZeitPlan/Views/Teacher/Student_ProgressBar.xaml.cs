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
    public partial class Student_ProgressBar : ContentPage
    {
        private float progress;
        public  Student_ProgressBar()
        {
            InitializeComponent();
        }
        async void OnButtonClicked(object sender, EventArgs e)
        {
            progress += 0.2f;

            if (progress > 1)
            {
                progress = 0;
            }

            // directly set the new progress value
            defaultProgressBar.Progress = progress;

            // animate to the new value over 750 milliseconds using Linear easing
            await styledProgressBar.ProgressTo(progress, 750, Easing.Linear);
        }
    }
}