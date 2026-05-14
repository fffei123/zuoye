using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Maps;
using System;
using System.Collections.Generic;
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

            // 页面加载完成后，再执行定位+地图（避免启动直接卡死）
            this.Loaded += async (s, e) => await GetGPSLocation();
        }

        private async Task GetGPSLocation()
        {
            try
            {
                // 申请定位权限
                var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
                if (status != PermissionStatus.Granted)
                {
                    status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                    if (status != PermissionStatus.Granted)
                    {
                        lblGPS.Text = "请开启定位权限";
                        return;
                    }
                }

                // 极低精度+短超时，模拟器专用
                var location = await Geolocation.GetLastKnownLocationAsync();
                if (location == null)
                {
                    location = await Geolocation.GetLocationAsync(new GeolocationRequest
                    {
                        DesiredAccuracy = GeolocationAccuracy.Low,
                        Timeout = TimeSpan.FromSeconds(3)
                    });
                }

                if (location != null)
                {
                    lblGPS.Text = $"已定位\n纬度：{location.Latitude:F5}\n经度：{location.Longitude:F5}";

                    // 延迟1秒再操作地图，避免UI阻塞
                    await Task.Delay(1000);
                    BinMap.MoveToRegion(MapSpan.FromCenterAndRadius(location, Distance.FromKilometers(1)));

                    // 再延迟加载大头针
                    await Task.Delay(500);
                    AddBinPins(location);

                    LoadBinList();
                }
                else
                {
                    lblGPS.Text = "无法获取位置，请打开GPS";
                }
            }
            catch (Exception ex)
            {
                lblGPS.Text = $"定位失败：{ex.Message}";
            }
        }

        private void AddBinPins(Location userLocation)
        {
            BinMap.Pins.Clear();

            var bins = new List<Location>
            {
                new Location(userLocation.Latitude + 0.0004, userLocation.Longitude + 0.0003),
                new Location(userLocation.Latitude - 0.0005, userLocation.Longitude + 0.0001),
                new Location(userLocation.Latitude + 0.0002, userLocation.Longitude - 0.0005),
                new Location(userLocation.Latitude - 0.0003, userLocation.Longitude - 0.0002)
            };

            string[] names = {
                "垃圾分类点",
                "可回收垃圾桶",
                "厨余垃圾点",
                "有害垃圾回收箱"
            };

            for (int i = 0; i < bins.Count; i++)
            {
                BinMap.Pins.Add(new Pin
                {
                    Label = names[i],
                    Location = bins[i]
                });
            }
        }

        private void LoadBinList()
        {
            BinList.Clear();
            BinList.Add("垃圾分类点 · 50米");
            BinList.Add("可回收垃圾桶 · 120米");
            BinList.Add("厨余垃圾点 · 200米");
            BinList.Add("有害垃圾回收箱 · 350米");
        }

        private async void GoBackHome(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}