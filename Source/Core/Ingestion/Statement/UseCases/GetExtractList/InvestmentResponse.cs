namespace Statement.UseCases.GetExtractList;

public class InvestmentResponse
{
    public string ProductName { get; set; } = string.Empty;
    public int ProductCode { get; set; }
    public decimal InvestedBalance { get; set; }
}