﻿using Xamarin.Forms;
using System;

namespace ZeitPlan.Views.Teacher
{
    public class Student_Progress:ContentPage
    {
        ProgressBar defaultProgressBar;
        ProgressBar styledProgressBar;
        float progress = 0f;

        public Student_Progress()
        {
            Title = "Student Progress";
            Padding = 24;

            defaultProgressBar = new ProgressBar
            {
                WidthRequest = 500,
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            styledProgressBar = new ProgressBar
            {
                ProgressColor = Color.Orange,
                WidthRequest = 500,
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            var progressButton = new Button
            {
                Text = "Click to Progress",
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            progressButton.Clicked += OnButtonClicked;

            Content = new StackLayout
            {
                Children =
                {
                    defaultProgressBar,
                    styledProgressBar,
                    progressButton
                }
            };
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

