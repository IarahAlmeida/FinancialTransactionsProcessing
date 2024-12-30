namespace Application.Features.Transactions.Queries.GetHighestSpender;

public record GetHighestSpenderResponse(Guid UserId, decimal TotalExpense);
