using Application.Contracts.Repositories;
using MediatR;

namespace Application.Features.Transactions.Queries.GetTopCategories;

public class GetTopCategoriesQueryHandler(ITransactionRepository repository) : IRequestHandler<GetTopCategoriesQuery, IEnumerable<GetTopCategoriesResponse>>
{
    private readonly ITransactionRepository _repository = repository;

    public async Task<IEnumerable<GetTopCategoriesResponse>> Handle(GetTopCategoriesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetTopCategories(cancellationToken);
    }
}
