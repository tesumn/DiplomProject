using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using DiplomProject.Data;
using DiplomProject.Services;
using DiplomProject.ViewModels;
using DiplomProject.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Cryptography;
using System.Text;

namespace DiplomProject
{
    public partial class App : Application
    {
        private ServiceProvider? _serviceProvider;

        public override void Initialize() => AvaloniaXamlLoader.Load(this);

        public override void OnFrameworkInitializationCompleted()
        {
            var services = new ServiceCollection();

            // Server=(localdb)\\mssqllocaldb;Database=ServiceCenterDb;Trusted_Connection=True;
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ServiceCenterDb;Trusted_Connection=True;"),
                ServiceLifetime.Transient);

            services.AddTransient<ClientService>();
            services.AddSingleton(_ =>
            {
                var key = SHA256.HashData(Encoding.UTF8.GetBytes("key..."));
                return new EncryptionService(key);
            });

            services.AddTransient<MainWindowViewModel>();
            services.AddTransient<ClientsViewModel>();
            services.AddTransient<DashboardViewModel>();
            services.AddTransient<OrdersViewModel>();

            _serviceProvider = services.BuildServiceProvider();

            using var scope = _serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            dbContext.Database.Migrate();

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    DataContext = _serviceProvider.GetRequiredService<MainWindowViewModel>()
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}