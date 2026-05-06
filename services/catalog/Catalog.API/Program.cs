using Asp.Versioning;
using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Core.Repository;
using Catalog.Infrastructure.Repositories;
using Catalog.Infrastructure.Seeding.Contexts;
using Microsoft.OpenApi;
using System.Reflection;


namespace Catalog.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(
                    Assembly.GetExecutingAssembly(),
                    typeof(GetProductByIdQuery).Assembly
                );
            });
            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<ProductMappingProfile>();
            });

            //builder.Services.AddAutoMapper(cfg => { }, Assembly.GetExecutingAssembly());
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<ICatalogContext, CatalogContext>();
            builder.Services.AddScoped<ITypeProductRepository, ProductRepository>();
            builder.Services.AddScoped<IBrandRepository, ProductRepository>();
            builder.Services.AddApiVersioning(option =>
            {
                option.ReportApiVersions = true;
                option.AssumeDefaultVersionWhenUnspecified = true;
                option.DefaultApiVersion = new ApiVersion(1, 0);
            });
            builder.Services.AddOpenApi();
            builder.Services.AddSwaggerGen(
                options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Title = "Catalog API",
                        Version = "v1",
                        Description = "API E-commerce Catalog Service Using Microservices",
                        Contact = new OpenApiContact
                        {
                            Name = "Sara Hamdy ",
                            Email = "sara101hamdy@gmail.com"
                        }


                    });
                }
                );
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
