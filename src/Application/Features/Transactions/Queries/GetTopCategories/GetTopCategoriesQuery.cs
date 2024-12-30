using MediatR;

namespace Application.Features.Transactions.Queries.GetTopCategories;
public record GetTopCategoriesQuery : IRequest<IEnumerable<GetTopCategoriesResponse>>;
