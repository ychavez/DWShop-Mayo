namespace DWShop.Client.Mobile.Views
{
    public class MainTabbedPage: TabbedPage
    {

        public MainTabbedPage(ProductListView productListView, BasketView basketView, ProductView productView)
        {
            Children.Add(productListView);
            Children.Add(basketView);
            Children.Add(productView);
        }
    }
}
