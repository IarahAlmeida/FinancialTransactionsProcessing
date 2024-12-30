namespace Application.Features.Transactions.Queries.GetUsersSummary;

public record GetUsersSummaryResponse(Guid UserId, decimal TotalIncome, decimal TotalExpense);
