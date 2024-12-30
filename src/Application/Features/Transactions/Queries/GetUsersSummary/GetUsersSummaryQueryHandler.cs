using Application.Contracts.Repositories;
using MediatR;

namespace Application.Features.Transactions.Queries.GetUsersSummary;

public class GetUsersSummaryQueryHandler(ITransactionRepository repository) : IRequestHandler<GetUsersSummaryQuery, IEnumerable<GetUsersSummaryResponse>>
{
    private readonly ITransactionRepository _repository = repository;

    public async Task<IEnumerable<GetUsersSummaryResponse>> Handle(
        GetUsersSummaryQuery request,
        CancellationToken cancellationToken)
    {
        return await _repository.GetUsersSummary(cancellationToken);
    }
}
