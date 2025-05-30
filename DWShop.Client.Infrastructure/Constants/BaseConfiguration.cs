namespace DWShop.Client.Infrastructure.Constants
{
    public static class BaseConfiguration
    {
        public const string AuthToken = "authToken";
        public const string Scheme = "Bearer";

        public static string? Token { get; set; }

#if DEBUG
        public const string BaseAddress = "https://8rvwk04l-5268.usw3.devtunnels.ms/";
#else
        public const string BaseAddress = "www.google.com";
#endif
    }
}
