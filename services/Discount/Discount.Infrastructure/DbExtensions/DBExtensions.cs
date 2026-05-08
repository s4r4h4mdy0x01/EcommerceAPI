using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace Discount.Infrastructure.DbExtensions
{
    public static class DBExtensions
    {
        public static IHost MigrateDatabase<TContext>(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var configuration = services.GetRequiredService<IConfiguration>();
                var logger = services.GetRequiredService<ILogger<TContext>>();
                try
                {
                    logger.LogInformation("Migrating database associated with context {DbContextName}",
                        typeof(TContext).Name);
                    ApplyMigrations(configuration);
                    logger.LogInformation("Migrated database associated with context {DbContextName}",
                        typeof(TContext).Name);

                }
                catch (Exception ex)
                {
                    logger.LogError(ex,
                        "An error occurred while migrating the database associated with context {DbContextName}",
                        typeof(TContext).Name);
                    throw;
                }



            }
            return host;

        }

        private static void ApplyMigrations(IConfiguration configuration)
        {
            var retryCount = 5;
            while (retryCount > 0)
            {
                try
                {
                    using var connection = new NpgsqlConnection(configuration.GetValue<string>
                        ("DatabaseSettings:ConnectionString"));
                    connection.Open();
                    using var command = new NpgsqlCommand
                    {
                        Connection = connection,

                    };
                    command.CommandText = "Drop table if exists Coupon";

                    command.ExecuteNonQuery();
                    command.CommandText = @"CREATE TABLE Coupon(
                                                Id SERIAL PRIMARY KEY,
                                                ProductName VARCHAR(500) NOT NULL,
                                                Description TEXT,
                                                Amount INT
                                            )";
                    command.ExecuteNonQuery();
                    command.CommandText = "INSERT INTO Coupon(ProductName, Description, Amount)" +
                        " VALUES('Egypt Adidas Quick Force Indoor Badminton Shoes', 'Discount for Adidas shoes', 150)";
                    command.ExecuteNonQuery();
                    command.CommandText = "INSERT INTO Coupon(ProductName, Description, Amount) " +
                        "VALUES('PowerFit 19 FH Rubber Spike Cricket Shoes', 'Discount for PowerFit shoes', 100)";
                    command.ExecuteNonQuery();
                    break;

                }
                catch (Exception)
                {
                    retryCount--;
                    if (retryCount == 0)
                    {
                        throw;
                    }
                    Thread.Sleep(2000);
                }





            }
        }
    }
}
