using CommunityToolkit.Mvvm.Messaging;
using DWShop.Client.Infrastructure.Managers.Products;
using DWShop.Client.Mobile.Context;
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
        private readonly IGetProductsManager productsManager;
        private readonly ProductRepo productRepo;

        public ObservableCollection<ProductModel> ProductsList
        {
            get => productsList;
            set => SetProperty(ref productsList, value);
        }

        public ICommand DetailCommand { get; set; }
        public ICommand RefreshCommand { get; set; }

        public ICommand EliminarCommand { get; set; }


        public ProductListViewModel(ProductView productView, IGetProductsManager productsManager,
            ProductRepo productRepo)
        {
            DetailCommand = new Command<ProductModel>(ShowDetail);
            RefreshCommand = new Command(async x => { await LoadProducts(); });
            EliminarCommand = new Command<ProductModel>(async x =>
            {
                bool answer = await Microsoft.Maui.Controls
                .Application.Current.MainPage.DisplayAlert("Confirmar",
                "Estas seguro que quieres borrar el producto", "Si", "No");

                _ = await Microsoft.Maui.Controls
            .Application.Current.MainPage.DisplayActionSheet("Opciones", "Cancelar", null,
            "No estoy seguro", "Tal vez", "Super si", "No", "No lo se");


                if (answer)
                    ProductsList = new(ProductsList.Where(y => y.Id != x.Id).ToList());

            });


            this.productView = productView;
            this.productsManager = productsManager;
            this.productRepo = productRepo;
            if (!WeakReferenceMessenger.Default.IsRegistered<ProductListRefreshMessage>(""))
            {
                WeakReferenceMessenger.Default.Register<ProductListRefreshMessage>("", async (o, s) =>
                {

                    await LoadProducts();
                });
            }
        }

        public async Task LoadProducts()
        {
            IsBusy = true;

            var dbProducts = await productRepo.GetProducts();

            if (dbProducts.Any())
            {
                ProductsList = new ObservableCollection<ProductModel>(dbProducts);
            }

            var response = await productsManager.GetAllProducts();
            if (response.Succeded)
            {
                foreach (var product in response.Data)
                {
                    await productRepo.AddProduct(new()
                    {
                        Id = product.Id,
                        PhotoURL = product.PhotoURL,
                        Price = product.Price,
                        ProductName = product.Name
                    });
                }
            }

            dbProducts = await productRepo.GetProducts();

            ProductsList = new ObservableCollection<ProductModel>(dbProducts);

            IsBusy = false;
        }

        public void ShowDetail(ProductModel productModel)
        {
            Navigation.PushAsync(productView);
            WeakReferenceMessenger.Default.Send(new ProductDetailMessage { Data = productModel });
        }
    }
}
