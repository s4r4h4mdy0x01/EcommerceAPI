
using Asp.Versioning;
using Basket.Application.Mappers;
using Basket.Application.Queries;
using Basket.Core.Repositories;
using Basket.Infrastructure.Repositories;
using Microsoft.OpenApi;
using System.Reflection;

namespace Basket.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(
                    Assembly.GetExecutingAssembly(),
                    typeof(GetBasketByUserNameQuery).Assembly
                );
            });
            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<BasketMappingProfile>();
            });


            builder.Services.AddScoped<IBasketRepository, BasketRepository>();
            builder.Services.AddStackExchangeRedisCache(opt =>
            {
                opt.Configuration = builder.Configuration.GetValue<string>("CacheSettings:ConnectionString");
            });
            builder.Services.AddApiVersioning(option =>
            {
                option.ReportApiVersions = true;
                option.AssumeDefaultVersionWhenUnspecified = true;
                option.DefaultApiVersion = new ApiVersion(1, 0);
            });

            builder.Services.AddSwaggerGen(
               options =>
               {
                   options.SwaggerDoc("v1", new OpenApiInfo
                   {
                       Title = "Basket API",
                       Version = "v1",
                       Description = "API E-commerce Basket Service Using Microservices",
                       Contact = new OpenApiContact
                       {
                           Name = "Sara Hamdy ",
                           Email = "sara101hamdy@gmail.com"
                       }


                   });
               }
               );
            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
