using Microsoft.Maui.LifecycleEvents;

namespace zuoye
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>(); 
            return builder.Build();
        }
    }
}