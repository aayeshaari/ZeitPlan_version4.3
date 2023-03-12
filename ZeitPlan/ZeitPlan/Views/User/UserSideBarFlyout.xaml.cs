using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ZeitPlan.Views.User
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserSideBarFlyout : ContentPage
    {
        public ListView ListView;

        public UserSideBarFlyout()
        {
            InitializeComponent();

            BindingContext = new UserSideBarFlyoutViewModel();
            ListView = MenuItemsListView;
        }

        class UserSideBarFlyoutViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<UserSideBarFlyoutMenuItem> MenuItems { get; set; }

            public UserSideBarFlyoutViewModel()
            {
                MenuItems = new ObservableCollection<UserSideBarFlyoutMenuItem>(new[]
                {
                    new UserSideBarFlyoutMenuItem { Id = 0, Icon="home_icon.png"  , Title = "Home" ,TargetType=typeof(UserHome) },
                    new UserSideBarFlyoutMenuItem { Id = 1, Icon="table_icon.png", Title = "View table",TargetType=typeof(ViewTable) },
                    new UserSideBarFlyoutMenuItem { Id = 2, Icon="Progress_icon.png", Title = "Student Progress" },
                    new UserSideBarFlyoutMenuItem { Id = 3, Icon="icon_feed.png", Title = "Teacher Diary" },
                    new UserSideBarFlyoutMenuItem { Id = 4, Icon="icon_feed.png", Title = "View Category", TargetType=typeof(Category_List) },
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}