namespace DWShop.Client.Infrastructure.Constants
{
    public static class BaseConfiguration
    {
        public const string AuthToken = "authToken";
        public const string Scheme = "Bearer";

#if DEBUG
        public const string BaseAddress = "Localhost";
#else
        public const string BaseAddress = "www.google.com";
#endif
    }
}
