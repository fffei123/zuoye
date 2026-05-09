using Microsoft.Maui.Controls;
using zuoye;

namespace zuoye
{
    public partial class NearbyBinPage : ContentPage
    {
        public NearbyBinPage()
        {
            InitializeComponent();
        }

        // 返回首页

        private async void OnGoBackClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}