using Microsoft.Maui.Controls;

namespace zuoye
{
    public partial class GuidePage : ContentPage
    {
        public GuidePage()
        {
            InitializeComponent();
        }


        private async void GoBackHome(object sender, EventArgs e)
        {

            await Navigation.PopToRootAsync();
        }
    }
}