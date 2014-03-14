using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using DykplatserWinPhone.Resources;
using Microsoft.Phone.Tasks;
using DykplatserWinPhone.ViewModels;
using System.Device.Location;

namespace DykplatserWinPhone
{
    public partial class DetailsPage : PhoneApplicationPage
    {
        int theIndex;
        DiveSpotViewModel diveSpot;
        // Constructor
        public DetailsPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        // When page is navigated to set data context to selected item in list
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (DataContext == null)
            {
                string selectedIndex = "";
                if (NavigationContext.QueryString.TryGetValue("selectedItem", out selectedIndex))
                {
                    int index = int.Parse(selectedIndex);
                    theIndex = index;
                    DataContext = App.ViewModel.Items[index];
                    diveSpot = App.ViewModel.Items[index] as DiveSpotViewModel;

                    TheMap.Center = new GeoCoordinate(diveSpot.Latitude, diveSpot.Longitude);
                    TheMap.ZoomLevel = 10;
                }
            }
        }

        private void GotoMap_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MapPage.xaml?selectedItem=" + theIndex, UriKind.Relative));

        }

        private void GotoInfo_Click(object sender, RoutedEventArgs e)
        {
            WebBrowserTask wbt = new WebBrowserTask();
            wbt.Uri = new Uri(diveSpot.MobileInfoUrl, UriKind.Absolute);
            wbt.Show();
        }

       

      
        // 
        //private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        //{
        //    WebBrowserTask webBrowserTask = new WebBrowserTask();

        //    HyperlinkButton theButton = sender as HyperlinkButton;
        //    webBrowserTask.Uri = theButton.NavigateUri;

        //    //webBrowserTask.Uri = new Uri(theButton.NavigateUri, UriKind.Absolute);

        //    webBrowserTask.Show();
        //}

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}