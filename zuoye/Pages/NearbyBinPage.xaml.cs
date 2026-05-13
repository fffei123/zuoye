using Microsoft.Maui.Devices.Sensors;
using Microsoft.Maui.ApplicationModel;
using System.Collections.ObjectModel;

namespace zuoye;

public partial class NearbyBinPage : ContentPage
{
    public ObservableCollection<string> BinList { get; set; }

    public NearbyBinPage()
    {
        InitializeComponent();

        BinList = new ObservableCollection<string>
        {
            "♻️ Recyclable Bin - 100m",
            "☠️ Hazardous Bin - 150m",
            "🗑️ General Waste Bin - 300m"
        };
        BindingContext = this;

        _ = GetGPS();
    }

    private async Task GetGPS()
    {
        try
        {
            var status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            if (status != PermissionStatus.Granted)
            {
                lblGPS.Text = "Location permission denied";
                return;
            }

            var req = new GeolocationRequest(GeolocationAccuracy.Low, TimeSpan.FromSeconds(8));
            var loc = await Geolocation.Default.GetLocationAsync(req);

            if (loc != null)
                lblGPS.Text = $"Current GPS Location: {loc.Latitude:F4}, {loc.Longitude:F4}";
            else
                lblGPS.Text = "Location unavailable";
        }
        catch
        {
            lblGPS.Text = "Location error";
        }
    }

    private async void GoBackHome(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}