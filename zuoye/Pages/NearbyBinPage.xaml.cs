using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Platform;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace zuoye
{
    public partial class NearbyBinPage : ContentPage
    {
        public ObservableCollection<string> BinList { get; set; }

        public NearbyBinPage()
        {
            InitializeComponent();
            BinList = new ObservableCollection<string>();
            BindingContext = this;

            this.Appearing += async (s, e) => await GetGPSLocation();
        }

        private async Task GetGPSLocation()
        {
            try
            {
                var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
                if (status != PermissionStatus.Granted)
                {
                    status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                    if (status != PermissionStatus.Granted)
                    {
                        lblGPS.Text = "Location permission required";
                        return;
                    }
                }

                Location location;
                try
                {
                    var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(5));
                    location = await Geolocation.GetLocationAsync(request);
                }
                catch
                {
                    location = new Location(39.9042, 116.4074);
                }

                if (location != null)
                {
                    lblGPS.Text = $"Located\nLat: {location.Latitude:F5}\nLng: {location.Longitude:F5}";
                    LoadMap(location);
                    LoadBinList();
                }
                else
                {
                    lblGPS.Text = "Could not get location";
                }
            }
            catch (Exception ex)
            {
                lblGPS.Text = $"Error: {ex.Message}";
            }
        }

        private void LoadMap(Location userLoc)
        {
            double lat1 = userLoc.Latitude + 0.0004;
            double lng1 = userLoc.Longitude + 0.0003;
            double lat2 = userLoc.Latitude - 0.0005;
            double lng2 = userLoc.Longitude + 0.0001;
            double lat3 = userLoc.Latitude + 0.0002;
            double lng3 = userLoc.Longitude - 0.0005;
            double lat4 = userLoc.Latitude - 0.0003;
            double lng4 = userLoc.Longitude - 0.0002;

            string html = $@"
<!DOCTYPE html>
<html>
<head>
<meta charset='utf-8'>
<meta name='viewport' content='width=device-width, initial-scale=1.0, user-scalable=no'>
<link rel='stylesheet' href='https://unpkg.com/leaflet@1.9.4/dist/leaflet.css'/>
<script src='https://unpkg.com/leaflet@1.9.4/dist/leaflet.js'></script>
<style>
  html,body{{margin:0;padding:0;height:100%;width:100%;}}
  #map{{height:100%;width:100%;}}
</style>
</head>
<body>
<div id='map'></div>
<script>
var map = L.map('map').setView([{userLoc.Latitude},{userLoc.Longitude}], 17);
L.tileLayer('https://{{s}}.tile.openstreetmap.org/{{z}}/{{x}}/{{y}}.png',{{
  attribution:'© OpenStreetMap'
}}).addTo(map);

L.marker([{userLoc.Latitude},{userLoc.Longitude}])
  .bindPopup('My Location').addTo(map);

L.marker([{lat1},{lng1}]).bindPopup('Waste Bin 1').addTo(map);
L.marker([{lat2},{lng2}]).bindPopup('Waste Bin 2').addTo(map);
L.marker([{lat3},{lng3}]).bindPopup('Waste Bin 3').addTo(map);
L.marker([{lat4},{lng4}]).bindPopup('Waste Bin 4').addTo(map);
</script>
</body>
</html>";

            MapWebView.Source = new HtmlWebViewSource { Html = html };
        }

        private void LoadBinList()
        {
            BinList.Clear();
            BinList.Add("Waste Bin 1 · 50m");
            BinList.Add("Waste Bin 2 · 120m");
            BinList.Add("Waste Bin 3 · 200m");
            BinList.Add("Waste Bin 4 · 350m");
        }

        private async void GoBackHome(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}