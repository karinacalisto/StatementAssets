namespace Statement.Entities;

public class Account : Entity<Guid>
{
    public string Agency { get; set; } = string.Empty; //Agência
    public string AccountNumber { get; set; } = string.Empty; //Conta
    public int DAC { get; set; } // Dígito
    public string AccountName { get; set; } = string.Empty; //nome do cliente

    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    public bool IsDraft { get; set; } = false;
}