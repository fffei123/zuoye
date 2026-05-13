using Microsoft.Maui.Controls;
using SQLite;
using System.IO;

namespace zuoye
{
    public partial class ScanPage : ContentPage
    {
        private readonly string _dbPath = Path.Combine(FileSystem.AppDataDirectory, "garbage.db3");
        private SQLiteConnection _db;

        public ScanPage()
        {
            InitializeComponent();
            InitDatabase();
        }

        private void InitDatabase()
        {
            _db = new SQLiteConnection(_dbPath);
            _db.CreateTable<Garbage>();
        }

        private void OnSearchClicked(object sender, EventArgs e)
        {
            string keyword = SearchEntry.Text?.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                ResultLabel.Text = "请输入垃圾名称";
                return;
            }

            var list = _db.Query<Garbage>("SELECT * FROM Garbage WHERE Name LIKE ?", $"%{keyword}%");

            if (list.Any())
            {
                var item = list.First();
                ResultLabel.Text = $"Result: {item.Name} → {item.Type}";
            }
            else
            {
                ResultLabel.Text = "未找到该垃圾，请重新输入";
            }
        }

        private async void GoBackToHome(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }

    public class Garbage
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }
}