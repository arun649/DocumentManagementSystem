using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using InfraStructureLayer.Data;
using ApplicationLayer.Services.Interface;
using InfraStructureLayer.Services.Repositories;
using Microsoft.Extensions.Configuration;
using UserInteraceLayer.ViewModel;
using ApplicationLayer.ViewModel;

namespace UserInteraceLayer
{
    public partial class App : Application
    {
        //protected override void OnStartup(StartupEventArgs e)
        //{
        //    base.OnStartup(e);

        //    // Assuming you have a way to resolve dependencies (e.g., using a DI container)
        //    IRegistrationRepository registrationRepository = new RegistrationRepository();
        //    IRegistrationViewModel viewModel = new RegistrationViewModel(registrationRepository);

        //    // Create the window
        //    //RegistrationWindow registrationWindow = new RegistrationWindow();

        //    // Manually inject the dependencies
        //    var registrationWindow = new RegistrationWindow(new RegistrationViewModel(new RegistrationRepository()));
        //    registrationWindow.Show();

        //    // Show the window
        //    registrationWindow.Show();
        //}
        //private IServiceProvider _serviceProvider;

        //protected override void OnStartup(StartupEventArgs e)
        //{
        //    base.OnStartup(e);

        //    var serviceCollection = new ServiceCollection();
        //    ConfigureServices(serviceCollection);

        //    _serviceProvider = serviceCollection.BuildServiceProvider();

        //    var mainWindow = _serviceProvider.GetRequiredService<RegistrationWindow>();
        //    mainWindow.Show();
        //}

        //private void ConfigureServices(IServiceCollection services)
        //{
        //    var connectionString = "Server=ARUN;Database=DBMS;Trusted_Connection=True;MultipleActiveResultSets=true";// context.Configuration.GetConnectionString("DefaultConnection");

        //    services.AddDbContext<ApplicationDbContext>(options =>
        //        options.UseSqlServer(connectionString));
        //    services.AddTransient<IRegistrationRepository, RegistrationRepository>();
        //    services.AddTransient<RegistrationViewModel>();
        //    services.AddTransient<RegistrationWindow>();
        //}



        private IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                        .ConfigureAppConfiguration((hostingContext, config) =>
                        {
                            config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                        })
                        .ConfigureServices((context, services) =>
                        {
                            var connectionString = context.Configuration.GetConnectionString("DefaultConnection");
                            
                            services.AddDbContext<ApplicationDbContext>(options =>
                                options.UseSqlServer(connectionString));

                            // Register repositories
                            services.AddScoped<IRegistrationRepository, RegistrationRepository>();
                            services.AddScoped<ILoginService, LoginService>();
                            services.AddScoped<IDocumentUploadRepository, DocumentUploadRepository>();
                            // Register the ViewModel
                            // services.AddSingleton<RegistrationViewModel>();

                            // Register the Window
                            services.AddTransient<RegistrationWindow>();
                            services.AddTransient<UserList>();
                            services.AddTransient<Login>();
                            services.AddTransient<DocumentUpload>();


                        })
                        .Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await _host.StartAsync();
            // Resolve the RegistrationWindow, which will automatically resolve its dependencies
            var mainWindow = _host.Services.GetRequiredService<DocumentUpload>();
            mainWindow.Show();
            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await _host.StopAsync();
            _host.Dispose();

            base.OnExit(e);
            }
        }
}
