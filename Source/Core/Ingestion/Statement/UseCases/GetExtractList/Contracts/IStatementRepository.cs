namespace Statement.UseCases.GetExtractList.Contracts;

public interface IStatementRepository
{
    List<Investments> GetInvestments(StatementRequest request);

}