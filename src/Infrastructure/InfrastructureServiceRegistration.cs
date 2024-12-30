using Application.Contracts.Parsers;
using Application.Contracts.Repositories;
using Infrastructure.File;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContextFactory<ApplicationDbContext>(options => options.UseNpgsql(connectionString), ServiceLifetime.Scoped);

        services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));

        services.AddScoped<ITransactionRepository, TransactionRepository>();
        services.AddScoped<ICsvParser, CsvParser>();

        // the block below is for development only, in a production environment with horizontal scalability
        // a better approach should be to run the migrations on the devops pipeline, that way if you have multiple
        // applications running, you won't run into the scenario where all of them are running the same migrations
        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
        {
            using var scope = services.BuildServiceProvider().CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            dbContext.Database.Migrate();
        }

        return services;
    }
}
