using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using ShopApi.Services;
using ShopApplication.Interfaces;
using ShopApplication.Interfaces.Helpers;
using ShopApplication.Interfaces.Repositories;
using ShopApplication.Interfaces.Services;
using ShopApplication.Mapping;
using ShopApplication.Services;
using ShopInfrastructure.Configuration;
using ShopInfrastructure.Data;
using ShopInfrastructure.Helpers;
using ShopInfrastructure.Repositories;
using ShopInfrastructure.Services;
using StackExchange.Redis;
using System.Text;

namespace ShopApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ShopDbContext>(options =>
            {
                //options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnection"));
                options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSqlServerConnection"));
            });

            var configuration = builder.Configuration;

            //JWT Settings
            var jwtSettings = configuration
                .GetSection("Jwt")
                .Get<JwtSettings>()
                ?? throw new Exception("JWT settings not configured.");

            builder.Services.Configure<JwtSettings>
                (configuration.GetSection("Jwt"));

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

                options.AddPolicy("ProductionPolicy", policy =>
                {
                    policy.WithOrigins("https://example.com", "https://www.example.com")
                          .WithMethods("GET", "POST", "PUT", "DELETE")
                          .WithHeaders("Content-Type", "Authorization");
                });
            });

            builder.Services.AddMemoryCache();
            builder.Services.AddEndpointsApiExplorer();

            // Swagger + JWT
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Description = "Enter JWT token"
                });

                options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
                {
                    [new OpenApiSecuritySchemeReference("Bearer", document)] = []
                });
            });

            //builder.Services.AddSwaggerGen();

            // Authentication
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSettings.Key)
                    ),

                    ClockSkew = TimeSpan.Zero
                };
            });

            builder.Services.AddAuthorization();
            builder.Services.AddControllers();

            // Redis
            builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                var config = builder.Configuration.GetConnectionString("RedisServerConnection");
                return ConnectionMultiplexer.Connect(config);
            });

            // RabbitMq
            builder.Services.Configure<RabbitMqSettings>(
                builder.Configuration.GetSection("RabbitMq")
            );

            // CACHE
            builder.Services.AddMemoryCache();

            // SERVICES
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<ICartService, CartService>();
            builder.Services.AddScoped<IImageService, ImageService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IJWTService, JWTService>();
            //builder.Services.AddSingleton<ICachingService, MemoryCachingService>();
            builder.Services.AddSingleton<ICachingService, RedisCachingService>();
            builder.Services.AddScoped<IEmailService, EmailService>();
            builder.Services.AddScoped<IQueueService, RabbitMqService>();

            // REPOSITORIES
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<ICartRepository, CartRepository>();
            builder.Services.AddScoped<IAuthRepository, AuthRepository>();
            builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            builder.Services.AddScoped<IPasswordResetTokenRepository, PasswordResetTokenRepository>();

            // HELPERS
            builder.Services.AddSingleton<IHashHelper, HashHelper>();

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseCors("AllowAll");
            app.UseCors("ProductionPolicy");

            // Database Seeding
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    await AdminSeeder.SeedAsync(services);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "Помилка під час ініціалізації бази даних або створення першого адміна.");
                }
            }

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ShopApi v1");
                });
            }

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            app.UseStaticFiles();
            app.Run();
        }
    }
}