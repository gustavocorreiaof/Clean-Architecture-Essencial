using Application.Interfaces;
using Application.Mappings;
using Application.Services;
using Domain.Interfaces;
using Infra.Data.Context;
using Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options => 
                options.UseNpgsql(Environment.GetEnvironmentVariable("DEV_DB"),
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(AppDomain.CurrentDomain.Load("Application")));

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddAutoMapper(cfg => {
                cfg.AddProfile<DomainToDTOMappingProfile>();
            });

            return services;
        }
    }
}
