using MediatR;

namespace Application.Features.Transactions.Queries.GetHighestSpender;

public record GetHighestSpenderQuery : IRequest<GetHighestSpenderResponse>;
