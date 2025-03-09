using Statement.Entities;
using Statement.UseCases.GetExtractList;
using Statement.UseCases.GetExtractList.Contracts;

namespace StatementTests.Fakes;

public class FakeRepository : IStatementRepository
{
    public List<Investments> GetInvestments(StatementRequest request)
    {
        return new List<Investments>();
    }
}
