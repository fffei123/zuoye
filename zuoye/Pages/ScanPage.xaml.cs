namespace zuoye
{
    public partial class ScanPage : ContentPage
    {
        public ScanPage()
        {
            InitializeComponent();
        }

        
        private async void GoBackToHome(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}