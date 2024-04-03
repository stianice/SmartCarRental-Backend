using Microsoft.Extensions.Configuration;

namespace CarRental.Common
{
    public static class AppSettings
    {
        private static IConfiguration? _configuaration;

        public static IConfiguration Configuration
        {
            get
            {
                return _configuaration ?? throw new ArgumentNullException(nameof(Configuration));
            }
        }

        public static void Configure(IConfiguration configuration)
        {
            if (_configuaration != null)
            {
                throw new Exception($"{nameof(_configuaration)}不可修改！");
            }
            _configuaration = configuration;
        }

        public static class Jwt
        {
            public static string SecretKey => Configuration["Jwt:sign"]!;
            public static string Issuer => Configuration["Jwt:iss"]!;
            public static string Audience => Configuration["Jwt:aud"]!;
        }

        public static string[] AllowCors => Configuration.GetSection("AllowCors").Get<string[]>()!;
    }
}
