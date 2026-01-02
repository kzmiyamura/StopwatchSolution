using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StopwatchLogic;
using StopwatchWpf.Settings;
using StopwatchWpf.ViewModels;
using System.Windows;

namespace StopwatchWpf
{
    public partial class App : Application
    {
        public static IHost? AppHost { get; private set; }

        public App()
        {
            AppHost = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration(config =>
                {
                    config.AddJsonFile("appsettings.json", optional: false);
                })
                .ConfigureServices((context, services) =>
                {
                    // settings
                    services.Configure<StopwatchSettings>(
                        context.Configuration.GetSection("Stopwatch"));

                    // logic
                    services.AddSingleton<IStopwatch, HighPrecisionStopwatchV2>();

                    // viewmodel
                    services.AddSingleton<MainViewModel>();

                    // view
                    services.AddSingleton<MainWindow>();
                })
                .Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            if (AppHost is null)
                throw new InvalidOperationException("AppHost is not initialized.");

            await AppHost.StartAsync();

            var mainWindow = AppHost.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();

            base.OnStartup(e);
        }
    }
}
