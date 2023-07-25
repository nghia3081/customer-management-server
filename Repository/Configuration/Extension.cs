using BusinessObject;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Repository.Entities;
using Repository.IRepositories;
using Repository.IRepositories.Base;
using Repository.IRepositories.IServices;
using Repository.Repositories;
using Repository.Repositories.Base;
using Repository.Repositories.Services;
using System.Text;

namespace Repository.Configuration
{
    public static class Extension
    {
        public static IServiceCollection AddRepoDI(this IServiceCollection services)
        {
            return services.AddScoped<CustomerManageContext>()
                .AddScoped<IContractDetailRepository, ContractDetailRepository>()
                .AddScoped<IContractRepository, ContractRepository>()
                .AddScoped<ICustomerRepository, CustomerRepository>()
                .AddScoped<IMenuRepository, MenuRepository>()
                .AddScoped<IPackageRepository, PackageRepository>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddSingleton<IEmailService, EmailService>()
                .AddScoped<ICategoryRepository<Repository.Entities.StatusCategory, BusinessObject.Models.StatusCategory>, CategoryRepository<Repository.Entities.StatusCategory, BusinessObject.Models.StatusCategory>>()
                .AddScoped<ICategoryRepository<Repository.Entities.TaxCategory, BusinessObject.Models.TaxCategory>, CategoryRepository<Repository.Entities.TaxCategory, BusinessObject.Models.TaxCategory>>();
        }
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                var appSetting = services.BuildServiceProvider().GetService<IOptions<AppSetting>>()?.Value;
                options.RequireHttpsMetadata = true;
                options.SaveToken = true;

                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = appSetting?.Jwt.Audience,
                    ValidIssuer = appSetting?.Jwt.Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSetting?.Jwt.Key)),
                };
            });
            return services;
        }
        public static string RandomString(int length = 10)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var randomString = new string(Enumerable.Repeat(chars, length)
                                                    .Select(s => s[random.Next(s.Length)]).ToArray());
            return randomString;
        }
        public static string Format(this string value, params object?[] args)
        {
            return string.Format(value, args);
        }
        public static string ToBase64String(this byte[] byteArray) => Convert.ToBase64String(byteArray);
        public static bool IsNullOrEmpty(this string s) => string.IsNullOrEmpty(s);
        public static bool IsNullOrWhiteSpace(this string s) => string.IsNullOrEmpty(s);
        public static string FromBase64ToString(this string base64String) => Encoding.UTF8.GetString(Convert.FromBase64String(base64String));
        public static string ToBase64String(this string s) => Convert.ToBase64String(Encoding.UTF8.GetBytes(s));
        public static bool IsInRange(this int num, int min, int max) => num >= min && num <= max;
        public static bool IsInRange(this DateTime dateTime, DateTime min, DateTime max) => dateTime >= min && dateTime <= max;
    }
}
