using DWShop.Client.Mobile.Views;

namespace DWShop.Client.Mobile
{
    public partial class App : Microsoft.Maui.Controls.Application
    {
        private readonly LoginView loginView;

        public App(LoginView loginView)
        {
            InitializeComponent();
            this.loginView = loginView;
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(loginView);
        }
    }
}