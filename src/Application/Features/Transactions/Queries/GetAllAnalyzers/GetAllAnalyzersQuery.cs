using MediatR;

namespace Application.Features.Transactions.Queries.GetAllAnalyzers;

public record GetAllAnalyzersQuery : IRequest<GetAllAnalyzersResponse>;
