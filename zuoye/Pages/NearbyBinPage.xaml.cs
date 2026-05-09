using Microsoft.Maui.Controls;

namespace zuoye
{
    public partial class NearbyBinPage : ContentPage
    {
        public NearbyBinPage()
        {
            InitializeComponent();
        }

        private async void GoBackHome(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }
    }
}