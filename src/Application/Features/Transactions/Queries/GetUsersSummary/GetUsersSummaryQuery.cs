using MediatR;

namespace Application.Features.Transactions.Queries.GetUsersSummary;

public record GetUsersSummaryQuery : IRequest<IEnumerable<GetUsersSummaryResponse>>;
