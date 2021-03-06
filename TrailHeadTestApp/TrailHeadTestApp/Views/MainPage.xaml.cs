﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;

using TrailHeadTestApp.Models;

namespace TrailHeadTestApp.Views
{
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {
        Dictionary<MenuItemType, NavigationPage> MenuPages = new Dictionary<MenuItemType, NavigationPage>();
        public MainPage()
        {
            InitializeComponent();

            MasterBehavior = MasterBehavior.Popover;

            MenuPages.Add(MenuItemType.Employees, (NavigationPage)Detail);
        }

        public async Task NavigateFromMenu(MenuItemType id)
        {
            if (!MenuPages.ContainsKey(id))
            {
                switch (id)
                {
                    case MenuItemType.Employees:
                        MenuPages.Add(id, new NavigationPage(new EmployeeListPage()));
                        break;
                    case MenuItemType.BarcodeScanner:
                        MenuPages.Add(id, new NavigationPage(new BarcodeScannerPage()));
                        break;
                    case MenuItemType.GenerateQR:
                        MenuPages.Add(id, new NavigationPage(new GenerateQRPage()));
                        break;
                    case MenuItemType.About:
                        MenuPages.Add(id, new NavigationPage(new AboutPage()));
                        break;
                }
            }

            var newPage = MenuPages[id];

            if (newPage != null && Detail != newPage)
            {
                Detail = newPage;

                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(100);

                IsPresented = false;
            }
        }
    }
}