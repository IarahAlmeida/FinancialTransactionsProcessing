using System.Text.Json;
using Application;
using Application.Contracts.Parsers;
using Application.Contracts.Repositories;
using Application.Features.Transactions.Commands.ImportTransactions;
using Application.Features.Transactions.Queries.GetAllAnalyzers;
using Infrastructure;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using static System.Console;

namespace Console;

internal static class Program
{
    private static readonly JsonSerializerOptions s_jsonOptions = new() { WriteIndented = true, PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower };

    private static async Task Main()
    {
        var filePath = Environment.GetEnvironmentVariable("CSV_FILE_PATH");
        if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
        {
            WriteLine("Invalid file path from environment variable CSV_FILE_PATH");
            return;
        }

        var services = new ServiceCollection();
        ConfigureServices(services);
        var serviceProvider = services.BuildServiceProvider();

        if (!string.IsNullOrWhiteSpace(filePath))
        {
            using var fileStream = File.OpenRead(filePath);
            var command = new ImportTransactionsCommand(fileStream);
            var handler = new ImportTransactionsCommandHandler(
                serviceProvider.GetRequiredService<ICsvParser>(),
                serviceProvider.GetRequiredService<ITransactionRepository>()
            );

            await handler.Handle(command, CancellationToken.None);
            WriteLine("Imported transactions from file, generating analyzers...");

            // Get analyzers
            var mediator = serviceProvider.GetRequiredService<IMediator>();
            var analyzers = await GetAllAnalyzers(mediator, CancellationToken.None);

            var jsonResult = JsonSerializer.Serialize(analyzers, s_jsonOptions);
            await File.WriteAllTextAsync("/app/output/analyzers.json", jsonResult);

            WriteLine("Analyzers saved to analyzers.json");
        }

        var wait = ReadLine();
        WriteLine(wait);
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        var configuration = new ConfigurationBuilder().AddEnvironmentVariables().Build();

        services.AddApplication().AddInfrastructure(configuration);
    }

    public static async Task<GetAllAnalyzersResponse> GetAllAnalyzers(IMediator mediator, CancellationToken cancellationToken)
    {
        var query = new GetAllAnalyzersQuery();
        var result = await mediator.Send(query, cancellationToken);
        return result;
    }
}
