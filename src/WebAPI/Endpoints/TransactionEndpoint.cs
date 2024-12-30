using Application.Features.Transactions.Commands.ImportTransactions;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace WebAPI.Endpoints;

internal static class TransactionEndpoint
{
    internal static void MapTransactionEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/transaction")
            .WithTags("transaction")
            .WithOpenApi()
            .RequireRateLimiting("fixed")
            .DisableAntiforgery();

        group.MapPost("/bulk", ImportTransactions)
            .WithName(nameof(ImportTransactions));
    }

    internal static async Task<Created> ImportTransactions(IFormFile file, IMediator mediator, CancellationToken cancellationToken)
    {
        var command = new ImportTransactionsCommand(file.OpenReadStream());
        await mediator.Send(command, cancellationToken);
        return TypedResults.Created();
    }
}
