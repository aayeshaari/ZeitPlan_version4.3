using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZeitPlan.Models;

namespace ZeitPlan.Views.User
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Category_List : ContentPage
    {
        public Category_List()
        {
            InitializeComponent();
            LoadData();
        }
        async void LoadData()
        {
            DataList.ItemsSource = (await App.firebaseDatabase.Child("categories").OnceAsync<categories>()).Select(x => new categories
            {
                CatId = x.Object.CatId,
                Name = x.Object.Name,
                Detail = x.Object.Detail,
                Image = x.Object.Image,


            }).ToList();
           

        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {

        }
    }
}