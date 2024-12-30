using Application.Features.Transactions.Queries.GetAllAnalyzers;
using Application.Features.Transactions.Queries.GetHighestSpender;
using Application.Features.Transactions.Queries.GetTopCategories;
using Application.Features.Transactions.Queries.GetUsersSummary;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace WebAPI.Endpoints;

internal static class DataAnalyzerEndpoint
{
    internal static void MapDataAnalyzerEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/data-analyzer")
            .WithTags("data-analyzer")
            .WithOpenApi()
            .RequireRateLimiting("fixed");

        group.MapGet(string.Empty, GetAllAnalyzers)
            .WithName(nameof(GetAllAnalyzers));

        group.MapGet("users-summary", GetUsersSummary)
            .WithName(nameof(GetUsersSummary));

        group.MapGet("top-categories", GetTopCategories)
            .WithName(nameof(GetTopCategories));

        group.MapGet("highest-spender", GetHighestSpender)
                .WithName(nameof(GetHighestSpender));
    }

    internal static async Task<Ok<GetAllAnalyzersResponse>> GetAllAnalyzers(IMediator mediator, CancellationToken cancellationToken)
    {
        var query = new GetAllAnalyzersQuery();
        var result = await mediator.Send(query, cancellationToken);
        return TypedResults.Ok(result);
    }

    internal static async Task<Ok<IEnumerable<GetUsersSummaryResponse>>> GetUsersSummary(IMediator mediator, CancellationToken cancellationToken)
    {
        var query = new GetUsersSummaryQuery();
        var result = await mediator.Send(query, cancellationToken);
        return TypedResults.Ok(result);
    }

    internal static async Task<Ok<IEnumerable<GetTopCategoriesResponse>>> GetTopCategories(IMediator mediator, CancellationToken cancellationToken)
    {
        var query = new GetTopCategoriesQuery();
        var result = await mediator.Send(query, cancellationToken);
        return TypedResults.Ok(result);
    }

    internal static async Task<Results<Ok<GetHighestSpenderResponse>, NotFound>> GetHighestSpender(IMediator mediator, CancellationToken cancellationToken)
    {
        var query = new GetHighestSpenderQuery();
        var result = await mediator.Send(query, cancellationToken);
        if (result != null)
        {
            return TypedResults.Ok(result);
        }
        else
        {
            return TypedResults.NotFound();
        }
    }
}
