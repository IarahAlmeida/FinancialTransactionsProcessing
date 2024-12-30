using MediatR;

namespace Application.Features.Transactions.Commands.ImportTransactions;
public record ImportTransactionsCommand : IRequest<bool>
{
    public Stream FileStream { get; }

    public ImportTransactionsCommand(Stream fileStream)
    {
        FileStream = fileStream;
    }
}
