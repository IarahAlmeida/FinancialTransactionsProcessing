using Domain.Entities;

namespace Application.Contracts.Parsers;

public interface ICsvParser
{
    IEnumerable<IEnumerable<Transaction>> ParseTransactionsFromCsv(Stream fileStream, CancellationToken cancellationToken);
}
