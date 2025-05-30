using DWShop.Client.Mobile.Models;
using DWShop.Client.Mobile.ViewModels;

namespace DWShop.Client.Mobile.Views;

public partial class ProductView : ContentPage
{ 
    public ProductView(ProductViewModel viewModel )
    {
		InitializeComponent();
        BindingContext = viewModel;
        viewModel.Navigation = this.Navigation;   
    }
}