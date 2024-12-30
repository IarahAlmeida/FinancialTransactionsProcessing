using Application.Contracts.Repositories;
using Application.Features.Transactions.Queries.GetHighestSpender;
using Application.Features.Transactions.Queries.GetTopCategories;
using Application.Features.Transactions.Queries.GetUsersSummary;
using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class TransactionRepository(IDbContextFactory<ApplicationDbContext> contextFactory, ApplicationDbContext context) : ITransactionRepository
{
    private readonly IDbContextFactory<ApplicationDbContext> _contextFactory = contextFactory;
    private readonly ApplicationDbContext _context = context;

    public async Task TruncateAsync(CancellationToken cancellationToken)
    {
        await _context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE transactions", cancellationToken);
    }

    public async Task BulkInsertAsync(IEnumerable<Transaction> transactions, CancellationToken cancellationToken)
    {
        await using var currentContext = await _contextFactory.CreateDbContextAsync(cancellationToken);
        try
        {
            currentContext.ChangeTracker.AutoDetectChangesEnabled = false;
            await currentContext.Transactions.AddRangeAsync(transactions, cancellationToken);
            await currentContext.SaveChangesAsync(cancellationToken);
        }
        finally
        {
            currentContext.ChangeTracker.Clear();
        }
    }

    public async Task<IEnumerable<GetUsersSummaryResponse>> GetUsersSummary(CancellationToken cancellationToken)
    {
        return await _context.Transactions
            .GroupBy(t => t.UserId)
            .Select(g => new GetUsersSummaryResponse(
                g.Key,
                g.Where(t => t.Amount > 0).Sum(t => t.Amount),
               Math.Abs(g.Where(t => t.Amount < 0).Sum(t => t.Amount))
            ))
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<GetTopCategoriesResponse>> GetTopCategories(CancellationToken cancellationToken)
    {
        return await _context.Transactions
            .GroupBy(t => t.Category)
            .Select(g => new
            {
                Category = g.Key,
                Count = g.Count()
            })
            .OrderByDescending(x => x.Count)
            .Take(3)
            .Select(x => new GetTopCategoriesResponse(x.Category, x.Count))
            .ToListAsync(cancellationToken);
    }

    public async Task<GetHighestSpenderResponse?> GetHighestSpender(CancellationToken cancellationToken)
    {
        return await _context.Transactions
            .GroupBy(t => t.UserId)
            .Select(g => new
            {
                UserId = g.Key,
                TotalExpense = g.Where(t => t.Amount < 0).Sum(t => t.Amount)
            }).OrderByDescending(x => x.TotalExpense)
            .Take(1)
            .Select(x => new GetHighestSpenderResponse(x.UserId, x.TotalExpense))
            .FirstOrDefaultAsync(cancellationToken);
    }
}
