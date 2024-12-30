using Application.Contracts.Parsers;
using Domain.Entities;
using nietras.SeparatedValues;

namespace Infrastructure.File;

public class CsvParser : ICsvParser
{
    private const int BATCH_SIZE = 10_000;

    public IEnumerable<IEnumerable<Transaction>> ParseTransactionsFromCsv(Stream fileStream, CancellationToken cancellationToken)
    {
        using var reader = Sep.Reader(r => r with { HasHeader = true }).From(fileStream);

        var transactionIdIdx = reader.Header.IndexOf("TransactionId");
        var userIdIdx = reader.Header.IndexOf("UserId");
        var dateIdx = reader.Header.IndexOf("Date");
        var amountIdx = reader.Header.IndexOf("Amount");
        var categoryIdx = reader.Header.IndexOf("Category");
        var descriptionIdx = reader.Header.IndexOf("Description");
        var merchantIdx = reader.Header.IndexOf("Merchant");

        var batch = new List<Transaction>(BATCH_SIZE);

        while (reader.MoveNext())
        {
            cancellationToken.ThrowIfCancellationRequested();

            var row = reader.Current;

            var transaction = new Transaction(
                transactionId: row[transactionIdIdx].Parse<Guid>(),
                userId: row[userIdIdx].Parse<Guid>(),
                date: row[dateIdx].Parse<DateTimeOffset>(),
                amount: row[amountIdx].Parse<decimal>(),
                category: row[categoryIdx].ToString(),
                description: row[descriptionIdx].ToString(),
                merchant: row[merchantIdx].ToString()
            );

            batch.Add(transaction);

            if (batch.Count >= BATCH_SIZE)
            {
                yield return batch.ToList();
                batch = new List<Transaction>(BATCH_SIZE);
            }
        }

        if (batch.Count > 0)
        {
            yield return batch;
        }
    }
}
