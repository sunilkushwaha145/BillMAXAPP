using BillMaxAPP.ViewModels;
using BillMaxAPP.Views;
using Microsoft.Extensions.DependencyInjection;

namespace BillMaxAPP
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }
        protected override Window CreateWindow(IActivationState? activationState)
        {
            var splashPage = IPlatformApplication.Current!
                .Services
                .GetRequiredService<SplashPage>();

            return new Window(new NavigationPage(splashPage)
            {
                BarBackgroundColor = Colors.White
            });
        }
        
    }
}