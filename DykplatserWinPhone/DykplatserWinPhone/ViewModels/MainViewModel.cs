using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Xml;
using DykplatserWinPhone.Resources;
using System.IO;
using System.Text;
using System.Xml.Linq;
using System.Linq;
using SharpKml.Engine;
using SharpKml;
using SharpKml.Dom;
using SharpKml.Base;
using System.Collections.Generic;
using DykplatserWinPhone.Helpers;
using System.Globalization;

namespace DykplatserWinPhone.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            this.Items = new ObservableCollection<DiveSpotViewModel>();
        }

        /// <summary>
        /// A collection for DiveSpotViewModel objects.
        /// </summary>
        public ObservableCollection<DiveSpotViewModel> Items { get; private set; }

        private string _sampleProperty = "Sample Runtime Property Value";
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding
        /// </summary>
        /// <returns></returns>
        public string SampleProperty
        {
            get
            {
                return _sampleProperty;
            }
            set
            {
                if (value != _sampleProperty)
                {
                    _sampleProperty = value;
                    NotifyPropertyChanged("SampleProperty");
                }
            }
        }

        /// <summary>
        /// Sample property that returns a localized string
        /// </summary>
        public string LocalizedSampleProperty
        {
            get
            {
                return AppResources.SampleProperty;
            }
        }

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates and adds a few DiveSpotViewModel objects into the Items collection.
        /// </summary>
        public void LoadData()
        {
            var res = App.GetResourceStream(new Uri("dykplatser-dykarna-nu.kml", UriKind.Relative));
            string myKml = new StreamReader(res.Stream).ReadToEnd();

            //TextReader tr = new StreamReader(res.Stream);

            //XmlReaderSettings settings = new XmlReaderSettings {DtdProcessing = DtdProcessing.Parse};
            //XmlReader reader = XmlReader.Create(tr, settings);

            //var xDoc = XDocument.Parse(myKml);
            //XNamespace ns = "http://earth.google.com/kml/2.2";


            List<DiveSpot> divespots = new List<DiveSpot>();
            XElement xmlDivespots = XElement.Parse(myKml);
            var ns = "{http://earth.google.com/kml/2.0}";

            string baseUrl = "http://www.dykarna.nu";

            int i = 0;

            foreach (XElement diveSpot in xmlDivespots.Descendants(ns + "Placemark"))
            {
                var theDiveSpot = new DiveSpot();
                theDiveSpot.Name = diveSpot.Element(ns + "name").Value;
                theDiveSpot.InfoUrl = Helper.ExtractURLsFromString(diveSpot.Element(ns + "description").Value)[0];
                theDiveSpot.MobileInfoUrl = string.Format("{0}/mobil/{1}", baseUrl, theDiveSpot.InfoUrl.Substring(22)); 
                //theDiveSpot.DiveLocation = new Location();
                theDiveSpot.Longitude = Double.Parse(diveSpot.Element(ns + "LookAt").Element(ns + "longitude").Value, CultureInfo.InvariantCulture);
                theDiveSpot.Latitude = Double.Parse(diveSpot.Element(ns + "LookAt").Element(ns + "latitude").Value, CultureInfo.InvariantCulture);

                divespots.Add(theDiveSpot);

                this.Items.Add(new DiveSpotViewModel() { ID = i.ToString(), Name = theDiveSpot.Name, LineTwo = string.Format("Long: {0} & Lat: {1}", theDiveSpot.Longitude, theDiveSpot.Latitude), Latitude = theDiveSpot.Latitude, Longitude = theDiveSpot.Longitude, LineThree = theDiveSpot.MobileInfoUrl, MobileInfoUrl = theDiveSpot.MobileInfoUrl });
                i++;

            }

            var ettes = divespots;





            //var placemarks = xDoc.Document.Descendants("Placemark")
            //                    .Select(p => new
            //                    {
            //                        Name = p.Element("name").Value,
            //                        Desc = p.Element("description").Value
            //                    })
            //                    .ToList();


            //var whoop = xDoc.Root
            //               .Element(ns + "Document")
            //               .Elements(ns + "Placemark")
            //               .Select(x => new PlaceMark
            //               {
            //                   Name = x.Element(ns + "name").Value,
            //                   Description = x.Element(ns + "description").Value,
            //                   // etc
            //               }).ToList();



            ////var doc = XDocument.Parse(myKml);
            //var query = xDoc.Root
            //               .Element(ns + "Document")
            //               .Elements(ns + "Placemark")
            //               .Select(x => new PlaceMark
            //               {
            //                   Name = x.Element(ns + "name").Value,
            //                   Description = x.Element(ns + "description").Value,
            //                   // etc
            //               }).ToList();



            //var myBreak = string.Empty;


            //TextReader tr = new StreamReader(myKml);

            //For security reasons DTD is prohibited in this XML document. 
            //To enable DTD processing set the DtdProcessing property on XmlReaderSettings to Parse and pass the settings into XmlReader.Create method.
           // var parser = new Parser();
           // parser.Parse(res.Stream);
           // Element parsedKml = parser.Root;
           //     //KmlFile file = KmlFile.Parse(res.Stream);
           

           // //C KmlFile.Load();

           // var kml = parsedKml;
           //// Kml kml = file.Root as Kml;
           // if (kml != null)
           // {
           //     int i = 0;
           //     foreach (var placemark in kml.Flatten().OfType<Placemark>())
           //     {
           //         //Console.WriteLine(placemark.Name);
           //         this.Items.Add(new DiveSpotViewModel() { ID = i.ToString(), LineOne = placemark.Name, Latitude = "Maecenas praesent accumsan bibendum", LineThree = "Facilisi faucibus habitant inceptos interdum lobortis nascetur pharetra placerat pulvinar sagittis senectus sociosqu" });
           //         i++;
           //     }
           // }

            //var xDoc = XDocument.Parse(res);
            //XNamespace ns = "http://earth.google.com/kml/2.2";

            //var placemarks = xDoc.Descendants(ns + "Placemark")
            //                    .Select(p => new
            //                    {
            //                        Name = p.Element(ns + "name").Value,
            //                        Desc = p.Element(ns + "description").Value
            //                    })
            //                    .ToList();

            //XNamespace ns = "http://earth.google.com/kml/2.2";
            //var doc = XDocument.Parse(kml);
            //var query = doc.Root
            //               .Element(ns + "Document")
            //               .Elements(ns + "Placemark")
            //               .Select(x => new PlaceMark
            //               {
            //                   Name = x.Element(ns + "name").Value,
            //                   Description = x.Element(ns + "description").Value,
            //                   // etc
            //               }).ToList();



            // Sample data; replace with real data
            //this.Items.Add(new DiveSpotViewModel() { ID = "0", LineOne = "runtime one", Latitude = "Maecenas praesent accumsan bibendum", LineThree = "Facilisi faucibus habitant inceptos interdum lobortis nascetur pharetra placerat pulvinar sagittis senectus sociosqu" });
            //this.Items.Add(new DiveSpotViewModel() { ID = "1", LineOne = "runtime two", Latitude = "Dictumst eleifend facilisi faucibus", LineThree = "Suscipit torquent ultrices vehicula volutpat maecenas praesent accumsan bibendum dictumst eleifend facilisi faucibus" });
            //this.Items.Add(new DiveSpotViewModel() { ID = "2", LineOne = "runtime three", Latitude = "Habitant inceptos interdum lobortis", LineThree = "Habitant inceptos interdum lobortis nascetur pharetra placerat pulvinar sagittis senectus sociosqu suscipit torquent" });
            //this.Items.Add(new DiveSpotViewModel() { ID = "3", LineOne = "runtime four", Latitude = "Nascetur pharetra placerat pulvinar", LineThree = "Ultrices vehicula volutpat maecenas praesent accumsan bibendum dictumst eleifend facilisi faucibus habitant inceptos" });
            //this.Items.Add(new DiveSpotViewModel() { ID = "4", LineOne = "runtime five", Latitude = "Maecenas praesent accumsan bibendum", LineThree = "Maecenas praesent accumsan bibendum dictumst eleifend facilisi faucibus habitant inceptos interdum lobortis nascetur" });
            //this.Items.Add(new DiveSpotViewModel() { ID = "5", LineOne = "runtime six", Latitude = "Dictumst eleifend facilisi faucibus", LineThree = "Pharetra placerat pulvinar sagittis senectus sociosqu suscipit torquent ultrices vehicula volutpat maecenas praesent" });
            //this.Items.Add(new DiveSpotViewModel() { ID = "6", LineOne = "runtime seven", Latitude = "Habitant inceptos interdum lobortis", LineThree = "Accumsan bibendum dictumst eleifend facilisi faucibus habitant inceptos interdum lobortis nascetur pharetra placerat" });
            //this.Items.Add(new DiveSpotViewModel() { ID = "7", LineOne = "runtime eight", Latitude = "Nascetur pharetra placerat pulvinar", LineThree = "Pulvinar sagittis senectus sociosqu suscipit torquent ultrices vehicula volutpat maecenas praesent accumsan bibendum" });
            //this.Items.Add(new DiveSpotViewModel() { ID = "8", LineOne = "runtime nine", Latitude = "Maecenas praesent accumsan bibendum", LineThree = "Facilisi faucibus habitant inceptos interdum lobortis nascetur pharetra placerat pulvinar sagittis senectus sociosqu" });
            //this.Items.Add(new DiveSpotViewModel() { ID = "9", LineOne = "runtime ten", Latitude = "Dictumst eleifend facilisi faucibus", LineThree = "Suscipit torquent ultrices vehicula volutpat maecenas praesent accumsan bibendum dictumst eleifend facilisi faucibus" });
            //this.Items.Add(new DiveSpotViewModel() { ID = "10", LineOne = "runtime eleven", Latitude = "Habitant inceptos interdum lobortis", LineThree = "Habitant inceptos interdum lobortis nascetur pharetra placerat pulvinar sagittis senectus sociosqu suscipit torquent" });
            //this.Items.Add(new DiveSpotViewModel() { ID = "11", LineOne = "runtime twelve", Latitude = "Nascetur pharetra placerat pulvinar", LineThree = "Ultrices vehicula volutpat maecenas praesent accumsan bibendum dictumst eleifend facilisi faucibus habitant inceptos" });
            //this.Items.Add(new DiveSpotViewModel() { ID = "12", LineOne = "runtime thirteen", Latitude = "Maecenas praesent accumsan bibendum", LineThree = "Maecenas praesent accumsan bibendum dictumst eleifend facilisi faucibus habitant inceptos interdum lobortis nascetur" });
            //this.Items.Add(new DiveSpotViewModel() { ID = "13", LineOne = "runtime fourteen", Latitude = "Dictumst eleifend facilisi faucibus", LineThree = "Pharetra placerat pulvinar sagittis senectus sociosqu suscipit torquent ultrices vehicula volutpat maecenas praesent" });
            //this.Items.Add(new DiveSpotViewModel() { ID = "14", LineOne = "runtime fifteen", Latitude = "Habitant inceptos interdum lobortis", LineThree = "Accumsan bibendum dictumst eleifend facilisi faucibus habitant inceptos interdum lobortis nascetur pharetra placerat" });
            //this.Items.Add(new DiveSpotViewModel() { ID = "15", LineOne = "runtime sixteen", Latitude = "Nascetur pharetra placerat pulvinar", LineThree = "Pulvinar sagittis senectus sociosqu suscipit torquent ultrices vehicula volutpat maecenas praesent accumsan bibendum" });

            this.IsDataLoaded = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}