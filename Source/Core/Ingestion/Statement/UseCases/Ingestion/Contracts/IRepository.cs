namespace Statement.UseCases.Ingestion.Contracts;

public interface IRepository
{
    void SaveAccount(Account account);
    void SaveCashComposition(CashComposition cashComposition);
    List<Account> GetAccounts();
    List<CashComposition> GetCashCompositions();
}