namespace DWShop.Client.Infrastructure.Constants
{
    public static class BaseConfiguration
    {
        public const string AuthToken = "authToken";
        public const string Scheme = "Bearer";

        public static string? Token { get; set; }

#if DEBUG
        public const string BaseAddress = "http://localhost:5268/";
#else
        public const string BaseAddress = "www.google.com";
#endif
    }
}
