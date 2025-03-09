namespace Statement.Data.DbContexts.UseCases.Persist;

public class Repository : IRepository
{
    private List<Account> accounts = new List<Account>();
    private List<CashComposition> cashCompositions = new List<CashComposition>();

    public void SaveAccount(Account account)
    {
        accounts.Add(account);
    }

    public void SaveCashComposition(CashComposition cashComposition)
    {
        cashCompositions.Add(cashComposition);
    }

    public List<Account> GetAccounts()
    {
        return accounts;
    }

    public List<CashComposition> GetCashCompositions()
    {
        return cashCompositions;
    }
}