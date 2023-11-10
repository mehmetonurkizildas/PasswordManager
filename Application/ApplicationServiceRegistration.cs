using Application.Services.Abstract;
using Application.Services.Concrete;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountManager>();
            services.AddScoped<IPasswordCollectionService, PasswordCollectionManager>();
            services.AddScoped<ICategoryService, CategoryManager>();
            return services;
        }
    }
}
