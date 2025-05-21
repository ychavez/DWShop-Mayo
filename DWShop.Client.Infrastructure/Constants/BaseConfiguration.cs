namespace DWShop.Client.Infrastructure.Constants
{
    public static class BaseConfiguration
    {
        public const string AuthToken = "authToken";
        public const string Scheme = "Bearer";

#if DEBUG
        public const string BaseAddress = "http://127.0.0.1";
#else
        public const string BaseAddress = "www.google.com";
#endif
    }
}
