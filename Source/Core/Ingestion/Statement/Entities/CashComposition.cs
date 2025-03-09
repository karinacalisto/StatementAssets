namespace Statement.Entities;

public class CashComposition : Entity<Guid>// Composição do saldo
{
    public string ProductName { get; set; } = string.Empty;  // Nome do produto
    public int ProductCode { get; set; } // Código do produto
    public decimal InvestedBalance { get; set; } // Saldo investido
}