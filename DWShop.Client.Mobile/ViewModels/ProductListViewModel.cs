using CommunityToolkit.Mvvm.Messaging;
using DWShop.Client.Mobile.Messages;
using DWShop.Client.Mobile.Models;
using DWShop.Client.Mobile.ViewModels.Base;
using DWShop.Client.Mobile.Views;
using System.Collections.ObjectModel;
using System.Timers;
using System.Windows.Input;

namespace DWShop.Client.Mobile.ViewModels
{
    public class ProductListViewModel : BaseViewModel
    {
   
        private ObservableCollection<ProductModel> productsList = new();
        private readonly ProductView productView;

        public ObservableCollection<ProductModel> ProductsList
        {
            get => productsList;
            set => SetProperty(ref productsList, value);
        }

        public ICommand DetailCommand { get; set; }


        public ProductListViewModel(ProductView productView)
        {
            var productList = new List<ProductModel>
               {
                   new ProductModel { Id = 1, ProductName = "Laptop",
                       PhotoURL = "https://mkgabinet.com/wp-content/uploads/2022/07/caracteristicas-beneficios-producto-mejorar-ventas.jpg", Price = 1200.99m },
                   new ProductModel { Id = 3, ProductName = "Headphones",
                       PhotoURL = "https://mkgabinet.com/wp-content/uploads/2022/07/caracteristicas-beneficios-producto-mejorar-ventas.jpg", Price = 199.99m },
                   new ProductModel { Id = 4, ProductName = "Smartwatch",
                       PhotoURL = "https://mkgabinet.com/wp-content/uploads/2022/07/caracteristicas-beneficios-producto-mejorar-ventas.jpg", Price = 249.99m },
                   new ProductModel { Id = 2, ProductName = "Smartphone",
                       PhotoURL = "https://mkgabinet.com/wp-content/uploads/2022/07/caracteristicas-beneficios-producto-mejorar-ventas.jpg", Price = 799.49m },
                   new ProductModel { Id = 5, ProductName = "Tablet",
                       PhotoURL = "https://mkgabinet.com/wp-content/uploads/2022/07/caracteristicas-beneficios-producto-mejorar-ventas.jpg", Price = 499.99m }
               };
            ProductsList = new ObservableCollection<ProductModel>(productList);

            DetailCommand = new Command<ProductModel>(ShowDetail);
            this.productView = productView;
        }

        public void ShowDetail(ProductModel productModel) 
        {
            Navigation.PushAsync(productView);
            WeakReferenceMessenger.Default.Send(new ProductDetailMessage { Data = productModel });
        }
    }
}
