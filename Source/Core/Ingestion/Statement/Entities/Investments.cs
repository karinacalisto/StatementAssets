namespace Statement.Entities;

public class Investments : Entity<Guid>
{
    public Guid ProductId { get; set; } 
    public virtual CashComposition CashComposition { get; set; } = null!;
    public Guid AccountId { get; set; }
    public virtual Account Account { get; set; } = null!;
    public decimal InvestedBalance { get; set; }
    public DateTimeOffset InvestedAt { get; set; }
    public string ProductName { get; set; } = null!;
    public int ProductCode { get; set; }
}