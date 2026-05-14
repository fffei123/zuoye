using Microsoft.Maui.LifecycleEvents;

namespace zuoye
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiMaps(); // 只需要这1句开启地图，删掉重复的
            return builder.Build();
        }
    }
}