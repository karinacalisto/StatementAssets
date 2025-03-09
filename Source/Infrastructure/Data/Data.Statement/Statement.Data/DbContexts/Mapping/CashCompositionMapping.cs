namespace Statement.Data.DbContexts.Mapping;
public class CashCompositionMapping : IEntityTypeConfiguration<CashComposition>
{
    public void Configure(EntityTypeBuilder<CashComposition> builder)
    {
        builder.ToTable(name: "cash_composition", schema: "statement"); // Nome da tabela

        builder.HasKey(c => c.Id); // Chave primária

        builder.Property(c => c.ProductName)
            .IsRequired()
            .HasMaxLength(100); // Nome do produto

        builder.Property(c => c.ProductCode)
            .IsRequired(); // Código do produto

        builder.Property(c => c.InvestedBalance)
            .IsRequired(); // Saldo investido
    }
}