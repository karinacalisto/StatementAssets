namespace Statement.Data.DbContexts.UseCases.Statement;

public class StatementRepository : IStatementRepository
{
    private readonly StatementDbContext _context;

    public StatementRepository(StatementDbContext context)
    {
        _context = context;
    }

    public List<Investments> GetInvestments(StatementRequest request)
    {
        var investments = _context.InvestmentStatement
            .Include(i => i.Account)
            .Include(i => i.CashComposition)
            .Where(i => i.Account.Agency == request.Agency &&
                        i.Account.AccountNumber == request.AccountNumber &&
                        i.Account.DAC == request.DAC)
            .Select(i => new Investments
            {
                ProductName = i.CashComposition.ProductName,
                ProductCode = i.CashComposition.ProductCode,
                InvestedBalance = i.InvestedBalance
            })
            .ToList();

        return investments;
    }
}    