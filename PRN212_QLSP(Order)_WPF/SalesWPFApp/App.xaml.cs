using DataAccess.Repository;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace SalesWPFApp
{
    public partial class App : Application
    {
        private ServiceProvider serviceProvider;
        public App()
        {
            ServiceCollection services = new();
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(ServiceCollection services)
        {
            services.AddSingleton(typeof(IMemberRepository), typeof(MemberRepository));
            services.AddSingleton<LoginWindow>();

        }
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var loginWindows = serviceProvider.GetService<LoginWindow>();
            loginWindows!.ShowDialog();
        }
    }
}
