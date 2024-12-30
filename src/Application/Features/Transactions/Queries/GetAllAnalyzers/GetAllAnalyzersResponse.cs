using Application.Features.Transactions.Queries.GetHighestSpender;
using Application.Features.Transactions.Queries.GetTopCategories;
using Application.Features.Transactions.Queries.GetUsersSummary;

namespace Application.Features.Transactions.Queries.GetAllAnalyzers;
public record GetAllAnalyzersResponse(IEnumerable<GetUsersSummaryResponse> UsersSummary, IEnumerable<GetTopCategoriesResponse> TopCategories, GetHighestSpenderResponse? HighestSpender);
