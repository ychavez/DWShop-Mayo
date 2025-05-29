using DWShop.Client.Mobile.ViewModels.Base;
using SQLite;

namespace DWShop.Client.Mobile.Models
{
    public class ProductModel : ObservableObject
    {
        private int id;
        [PrimaryKey]
        public int Id
        {
            get { return id; }
            set => SetProperty(ref id, value);
        }

        private string productName;

        public string ProductName
        {
            get { return productName; }
            set => SetProperty(ref productName, value);
        }

        private string photoURL;
        public string PhotoURL
        {
            get { return photoURL; }
            set => SetProperty(ref photoURL, value);
        }

        private decimal price;
        public decimal Price
        {
            get { return price; }
            set => SetProperty(ref price, value);
        }
    }
}
