using Application.Contracts.Repositories;
using MediatR;

namespace Application.Features.Transactions.Queries.GetHighestSpender;

public class GetHighestSpenderHandler(ITransactionRepository repository) : IRequestHandler<GetHighestSpenderQuery, GetHighestSpenderResponse?>
{
    private readonly ITransactionRepository _repository = repository;

    public Task<GetHighestSpenderResponse?> Handle(GetHighestSpenderQuery request, CancellationToken cancellationToken)
    {
        return _repository.GetHighestSpender(cancellationToken);
    }
}
