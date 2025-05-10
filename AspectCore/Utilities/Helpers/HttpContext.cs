using Microsoft.AspNetCore.Http;

namespace ErpMikroservis.AspectCore
{
    public static class HttpContext
    {
        private static IHttpContextAccessor _httpContextAccessor;
        public static Microsoft.AspNetCore.Http.HttpContext Current
        {
            get { return _httpContextAccessor.HttpContext; }
        }

        internal static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
    }
}
