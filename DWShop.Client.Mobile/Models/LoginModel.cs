using DWShop.Client.Mobile.ViewModels.Base;

namespace DWShop.Client.Mobile.Models
{
    public class LoginModel : ObservableObject
    {
        private string userName;

        public string UserName
        {
            get { return userName; }
            set { SetProperty(ref userName, value); }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set { SetProperty(ref password, value); }
        }


    }
}
