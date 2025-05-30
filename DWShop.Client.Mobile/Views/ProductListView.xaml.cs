using DWShop.Client.Mobile.Models;
using DWShop.Client.Mobile.ViewModels;

namespace DWShop.Client.Mobile.Views;

public partial class ProductListView : ContentPage
{
    public ProductListView(ProductListViewModel viewModel)
    {
        BindingContext = viewModel;

        InitializeComponent();
        viewModel.Navigation = this.Navigation;
    }

  

    private void productListView_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        if (e.Item is not null && BindingContext is ProductListViewModel viewModel)
        {
            var selectedItem = e.Item as ProductModel;

            viewModel.DetailCommand.Execute(selectedItem);
        }
    }
}