using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure.Persistence.Contexts;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        var username = Environment.GetEnvironmentVariable("POSTGRES_USER");
        var password = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD");
        var host = Environment.GetEnvironmentVariable("POSTGRES_HOST");
        var port = Environment.GetEnvironmentVariable("POSTGRES_PORT");
        var database = Environment.GetEnvironmentVariable("POSTGRES_DB");

        var connectionString = $"Host={host};Port={port};Database={database};Username={username};Password={password}";

        optionsBuilder.UseNpgsql(connectionString);

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}
