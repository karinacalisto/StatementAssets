namespace Statement.Data.DbContexts.Mapping;

public class AccountMapping : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable(name: "accounts", schema: "statement");

        builder.HasKey(c => c.Id); // Chave primária

        builder.Property(a => a.Agency)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(a => a.AccountNumber)
            .IsRequired();

        builder.Property(a => a.AccountName)
            .IsRequired();

        builder.Property(a => a.DAC)
            .IsRequired();
    }
}