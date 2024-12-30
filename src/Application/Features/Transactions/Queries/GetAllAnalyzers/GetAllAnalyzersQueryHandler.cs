using Application.Contracts.Repositories;
using MediatR;

namespace Application.Features.Transactions.Queries.GetAllAnalyzers;

public class GetAllAnalyzersQueryHandler(ITransactionRepository repository) : IRequestHandler<GetAllAnalyzersQuery, GetAllAnalyzersResponse>
{
    private readonly ITransactionRepository _repository = repository;

    public async Task<GetAllAnalyzersResponse> Handle(GetAllAnalyzersQuery request, CancellationToken cancellationToken)
    {
        var usersSummary = await _repository.GetUsersSummary(cancellationToken);
        var topCategories = await _repository.GetTopCategories(cancellationToken);
        var highestSpender = await _repository.GetHighestSpender(cancellationToken);

        return new GetAllAnalyzersResponse(usersSummary, topCategories, highestSpender);
    }
}
