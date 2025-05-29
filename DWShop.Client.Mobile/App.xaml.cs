using DWShop.Client.Mobile.Views;

namespace DWShop.Client.Mobile
{
    public partial class App : Microsoft.Maui.Controls.Application
    {
        private readonly MainTabbedPage tabbedPage;

        public App(MainTabbedPage tabbedPage)
        {
            InitializeComponent();
            this.tabbedPage = tabbedPage;
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new NavigationPage(tabbedPage));
        }
    }
}