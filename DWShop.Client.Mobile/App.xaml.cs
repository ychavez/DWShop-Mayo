using CommunityToolkit.Mvvm.Messaging;
using DWShop.Client.Mobile.Messages;
using DWShop.Client.Mobile.Services;
using DWShop.Client.Mobile.Views;

namespace DWShop.Client.Mobile
{
    public partial class App : Microsoft.Maui.Controls.Application
    {
   
        private readonly LoginView loginViewInstance;

        public App( LoginView loginView)
        {
            InitializeComponent();
            this.loginViewInstance = loginView;
        }

        
        

        protected override  Window CreateWindow(IActivationState activationState)
        {
            
            return new Window(loginViewInstance);
        }
    }
}