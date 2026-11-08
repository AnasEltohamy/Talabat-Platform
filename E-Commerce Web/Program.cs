using DomainLayer.Contracts.BasketContracts;
using DomainLayer.Contracts.ProductContracts;
using DomainLayer.Models.IdentityModuls;
using E_Commerce_Web.CustomMiddleWares;
using E_Commerce_Web.Factories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Peresistence;
using Peresistence.IdentityStore;
using Peresistence.Store;
using Peresistence.Store.Data.Repositories;
using Service;
using ServiceAbstraction;
using Shared.ErrorModels;
using StackExchange.Redis;
using System.Text;

namespace E_Commerce_Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IDataSeeding, DataSeeding>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IBasketReposatory, BasketReposatory>();
            builder.Services.AddScoped<IServiceManager, ServiceManager>();
            builder.Services.AddAutoMapper(cfg => { }, typeof(ServiceAssembly).Assembly);
            builder.Services.AddSingleton<IConnectionMultiplexer>
                (
                    Opt => 
                        {
                            return ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RedisConnection")!);
                        }
                ); 
            builder.Services.Configure<ApiBehaviorOptions>((opt) =>
            {
                opt.InvalidModelStateResponseFactory = ApiResponseFactories.GenerateApiValidationErrorsResponse;
            });

            builder.Services.AddDbContext<StoreDbContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("StoreConnection"));
            });

            builder.Services.AddDbContext<IdentityStoreDbContext>(opt =>{
                opt.UseSqlServer(builder.Configuration.GetConnectionString("IdentityStoreConnection"));
            });


            builder.Services
                .AddIdentityCore<AppUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<IdentityStoreDbContext>();

            builder.Services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = builder.Configuration.GetSection("JWTOptions")["issuer"],
                    ValidAudience = builder.Configuration.GetSection("JWTOptions")["audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JWTOptions")["SecretKey"]!))
                };
            });




            var app = builder.Build();

            using var Scope = app.Services.CreateScope();
            var Service = Scope.ServiceProvider.GetRequiredService<IDataSeeding>();
            await Service.DataSeedAsync();
            await Service.IdentityDataSeedAsync();



            // Configure the HTTP request pipeline.

            app.UseMiddleware<CustomExceptionHandlerMiddleWares>();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
