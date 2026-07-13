
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using ShopApi.Services;
using ShopApplication.Interfaces;
using ShopApplication.Interfaces.Helpers;
using ShopApplication.Interfaces.Repository;
using ShopApplication.Interfaces.Services;
using ShopApplication.Mapping;
using ShopApplication.Services;
using ShopInfrastructure.Data;
using ShopInfrastructure.Helpers;
using ShopInfrastructure.Repositories;
using System.Reflection;

namespace ShopApi
{
    /*public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestTimer(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestTimerMiddleware>();
        }
    }*/

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ShopDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

            });

            // AutoMapper

            builder.Services.AddAutoMapper(
                _ => { },
                typeof(CategoryProfile).Assembly,
                typeof(UserProfile).Assembly
            );

            // CORS 
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });


            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //builder.Services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo
            //    {
            //        Title = "Shop Api",
            //        Version = "v1"
            //    });
            //});

            builder.Services.AddSwaggerGen(options =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                options.IncludeXmlComments(xmlPath);
            });

            builder.Services.AddControllers();

            // SERVICES

            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IImageService, ImageService>();
            builder.Services.AddScoped<IAuthService, AuthService>();

            // REPOSITORIES

            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IAuthRepository, AuthRepository>();

            // HELPERS

            builder.Services.AddSingleton<IHashHelper, HashHelper>();

            //----

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseCors("AllowAll");

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ShopApi v1");
                });
            }
            //app.ProductsEndpoints();

            app.MapControllers();
            app.UseStaticFiles();
            //app.UseMiddleware<UserCheckMiddleware>();

            app.Run();
        }
    }
}
