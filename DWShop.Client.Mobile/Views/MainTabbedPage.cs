using DWShop.Client.Mobile.Services;

namespace DWShop.Client.Mobile.Views
{
    public class MainTabbedPage: TabbedPage
    {
        private readonly UtilityService utilityService;

        public MainTabbedPage(ProductListView productListView, 
            BasketView basketView, 
            ProductView productView, 
            UtilityService utilityService)
        {
            Children.Add(productListView);
            Children.Add(basketView);
            Children.Add(productView);
            this.utilityService = utilityService;
        }

        protected override async void OnAppearing()
        {

           

            base.OnAppearing();
        }
    }
}
