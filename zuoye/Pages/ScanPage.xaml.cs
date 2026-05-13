using Microsoft.Maui.Controls;

namespace zuoye;

public partial class ScanPage : ContentPage
{
    public ScanPage()
    {
        InitializeComponent();
        ResultLabel.Text = "✅ System Ready";
    }

    // 搜索按钮（100%可点击，内置数据，不用数据库）
    private void SearchDatabaseClicked(object sender, EventArgs e)
    {
        string input = SearchEntry.Text?.Trim().ToLower();

        if (string.IsNullOrEmpty(input))
        {
            ResultLabel.Text = "Please enter waste name";
            return;
        }

        // 内置垃圾分类数据，直接匹配
        switch (input)
        {
            case "paper":
                ResultLabel.Text = "Result: paper → Recyclable Waste";
                break;
            case "plastic":
                ResultLabel.Text = "Result: plastic → Recyclable Waste";
                break;
            case "battery":
                ResultLabel.Text = "Result: battery → Hazardous Waste";
                break;
            case "apple":
            case "banana":
                ResultLabel.Text = $"Result: {input} → Kitchen Waste";
                break;
            default:
                ResultLabel.Text = $"Not Found: {input}";
                break;
        }
    }

    // 返回按钮（100%可点击）
    private async void GoBackToHome(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}