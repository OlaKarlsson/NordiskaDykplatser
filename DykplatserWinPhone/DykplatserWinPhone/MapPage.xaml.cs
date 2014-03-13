using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Maps.Controls;
using System.Device.Location;
using DykplatserWinPhone.ViewModels;
using Microsoft.Phone.Maps.Toolkit;
using System.Collections.ObjectModel;

namespace DykplatserWinPhone
{
    public partial class MapPage : PhoneApplicationPage
    {
        public MapPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (DataContext == null)
            {
                string selectedIndex = "";
                if (NavigationContext.QueryString.TryGetValue("selectedItem", out selectedIndex))
                {
                    int index = int.Parse(selectedIndex);
                    DiveSpotViewModel theDiveSpot = App.ViewModel.Items[index];
                    DataContext = theDiveSpot;
                                       
                    TheMap.Center = new GeoCoordinate(theDiveSpot.Latitude, theDiveSpot.Longitude);
                    TheMap.ZoomLevel = 10;
                    

                }
            }
        }
    }
}
