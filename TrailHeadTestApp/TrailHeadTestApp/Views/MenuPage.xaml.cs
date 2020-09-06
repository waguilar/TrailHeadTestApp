using TrailHeadTestApp.Models;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;

namespace TrailHeadTestApp.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MenuPage : ContentPage
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        List<HomeMenuItem> menuItems;
        public MenuPage()
        {
            InitializeComponent();

            menuItems = new List<HomeMenuItem>
            {
                new HomeMenuItem {Id = MenuItemType.Employees, Title="Employees" },
                new HomeMenuItem {Id = MenuItemType.BarcodeScanner, Title="BarcodeScanner" },
                new HomeMenuItem {Id = MenuItemType.GenerateQR, Title="QR Generator" },
                new HomeMenuItem {Id = MenuItemType.About, Title="About" }
            };

            ListViewMenu.ItemsSource = menuItems;

            ListViewMenu.SelectedItem = menuItems[0];
            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;

                await RootPage.NavigateFromMenu(((HomeMenuItem)e.SelectedItem).Id);
            };
        }
    }
}