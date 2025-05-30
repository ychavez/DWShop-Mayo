using CommunityToolkit.Mvvm.Messaging;
using DWShop.Client.Mobile.Messages;
using DWShop.Client.Mobile.Models;
using DWShop.Client.Mobile.ViewModels.Base;
using System.Windows.Input;

namespace DWShop.Client.Mobile.ViewModels
{
    public class ProductViewModel : BaseViewModel
    {
        ProductModel productModel;

        public ICommand TakePhotoCommand { get; set; }
        public ProductModel ProductModel
        {
            get => productModel;
            set => SetProperty(ref productModel, value);
        }

        public ProductViewModel()
        {
            if (!WeakReferenceMessenger.Default.IsRegistered<ProductDetailMessage>(""))
            {

                WeakReferenceMessenger.Default.Register<ProductDetailMessage>("", (o, m) =>
                {
                    ProductModel = m.Data;
                });
            }

            TakePhotoCommand = new Command(async x => await TakePhoto());
        }

        private async Task TakePhoto()
        {
            var photo = await MediaPicker.Default.CapturePhotoAsync();
            productModel.PhotoURL = photo.FullPath;

            await Flashlight.Default.TurnOffAsync();

            await TextToSpeech.Default.SpeakAsync("Photo taken");
        }
    }
}
