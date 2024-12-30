namespace Domain.Entities;

public sealed class Transaction(Guid transactionId, Guid userId, DateTimeOffset date, decimal amount, string category, string description, string merchant)
{
    public Guid TransactionId { get; } = transactionId;
    public Guid UserId { get; } = userId;
    public DateTimeOffset Date { get; } = date;
    public decimal Amount { get; } = amount;
    public string Category { get; } = category;
    public string Description { get; } = description;
    public string Merchant { get; } = merchant;
}
