using Microsoft.Maui.Controls;
using SQLite;
using System;
using System.IO;
using System.Linq;

namespace zuoye;

public partial class ScanPage : ContentPage
{
    private SQLiteConnection _db;

    public ScanPage()
    {
        InitializeComponent();
        ResultLabel.Text = "✅ Connected to garbage.db";
        ConnectExternalDB();
    }

    // 连接你项目里已有的数据库
    private void ConnectExternalDB()
    {
        try
        {
            string targetPath = Path.Combine(FileSystem.AppDataDirectory, "garbage.db");

            if (!File.Exists(targetPath))
            {
                using var stream = FileSystem.OpenAppPackageFileAsync("garbage.db").Result;
                using var destStream = File.Create(targetPath);
                stream.CopyTo(destStream);
            }

            _db = new SQLiteConnection(targetPath);
        }
        catch (Exception ex)
        {
            ResultLabel.Text = "DB Error: " + ex.Message;
        }
    }

    // 查询按钮（只查分类名称，不显示描述）
    private void SearchDatabaseClicked(object sender, EventArgs e)
    {
        string input = SearchEntry.Text?.Trim().ToLower();

        if (string.IsNullOrEmpty(input))
        {
            ResultLabel.Text = "Please enter waste name";
            return;
        }

        var query = @"
        SELECT gt.type_name 
        FROM garbage_item gi
        JOIN garbage_type gt ON gi.type_id = gt.type_id
        WHERE LOWER(gi.item_name) = ?";

        var result = _db.Query<GarbageResult>(query, input).FirstOrDefault();

        if (result != null)
        {
            ResultLabel.Text = $"Result: {input} → {result.type_name}";
        }
        else
        {
            ResultLabel.Text = $"Not Found: {input}";
        }
    }

    // 返回主页
    private async void GoBackToHome(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}

// 数据库查询结果
public class GarbageResult
{
    public string type_name { get; set; }
}