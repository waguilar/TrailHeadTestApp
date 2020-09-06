using System;
using System.Collections.Generic;
using System.Text;

namespace TrailHeadTestApp.Models
{
    public enum MenuItemType
    {
        Employees,
        BarcodeScanner,
        GenerateQR,
        About
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
