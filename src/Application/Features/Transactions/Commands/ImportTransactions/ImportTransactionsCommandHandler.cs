using Application.Contracts.Parsers;
using Application.Contracts.Repositories;
using MediatR;

namespace Application.Features.Transactions.Commands.ImportTransactions;

public class ImportTransactionsCommandHandler(ICsvParser csvParser, ITransactionRepository repository) : IRequestHandler<ImportTransactionsCommand, bool>
{
    private readonly ICsvParser _csvParser = csvParser;
    private readonly ITransactionRepository _repository = repository;
    private const int MAX_PARALLEL_TASKS = 5;

    public async Task<bool> Handle(ImportTransactionsCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        await _repository.TruncateAsync(cancellationToken);

        using var semaphore = new SemaphoreSlim(MAX_PARALLEL_TASKS);
        var tasks = new List<Task>();

        foreach (var transactionBatch in _csvParser.ParseTransactionsFromCsv(
            request.FileStream,
            cancellationToken))
        {
            await semaphore.WaitAsync(cancellationToken);

            var task = Task.Run(async () =>
            {
                try
                {
                    await _repository.BulkInsertAsync(transactionBatch, cancellationToken);
                }
                finally
                {
                    semaphore.Release();
                }
            }, cancellationToken);

            tasks.Add(task);
        }

        await Task.WhenAll(tasks);

        return true;
    }
}
