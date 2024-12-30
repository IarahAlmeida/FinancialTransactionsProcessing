using Application.Features.Transactions.Queries.GetHighestSpender;
using Application.Features.Transactions.Queries.GetTopCategories;
using Application.Features.Transactions.Queries.GetUsersSummary;
using Domain.Entities;

namespace Application.Contracts.Repositories;

public interface ITransactionRepository
{
    Task TruncateAsync(CancellationToken cancellationToken);

    Task BulkInsertAsync(IEnumerable<Transaction> transactions, CancellationToken cancellationToken);

    Task<IEnumerable<GetUsersSummaryResponse>> GetUsersSummary(CancellationToken cancellationToken);

    Task<IEnumerable<GetTopCategoriesResponse>> GetTopCategories(CancellationToken cancellationToken);

    Task<GetHighestSpenderResponse?> GetHighestSpender(CancellationToken cancellationToken);
}
