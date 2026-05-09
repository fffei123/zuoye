using zuoye;

namespace zuoye;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void GoToNearbyBinPage(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new NearbyBinPage());
    }

    private async void GoToScanPage(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ScanPage());
    }

    private async void GoToGuidePage(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new GuidePage());
    }
}