using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PrTracker.Data;
using PrTracker.Graph;
using PrTracker.Helpers;
using PrTracker.ViewModel;
using System.Windows;

namespace PrTracker
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        //this is the "program.cs"
        //removed StartupUri="MainWindow.xaml"
        public static IHost? AppHost { get; private set; }

        public App()
        {
            AppHost = Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<MainWindow>();
                    services.AddSingleton<IMainWindowViewModel, MainWindowViewModel>();
                    services.AddSingleton<LiftContext>();
                    services.AddSingleton<DBInteraction>();
                    services.AddSingleton<LiftRelationConversions>();                
                })
                .Build();
        }
        protected override async void OnStartup(StartupEventArgs startupEventArgs)
        {
            await AppHost!.StartAsync(); //not null cause created in ctor

            //Shows initial form, which is the entrypoint for the application
            //If this closes, the app closes
            MainWindow startupForm = AppHost.Services.GetRequiredService<MainWindow>();
            startupForm.Show();

            //run the rest of the startup
            base.OnStartup(startupEventArgs);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await AppHost!.StopAsync();
            base.OnExit(e);
        }
    }

}
