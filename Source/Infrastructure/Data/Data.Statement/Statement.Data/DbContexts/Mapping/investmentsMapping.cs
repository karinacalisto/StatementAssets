namespace Statement.Data.DbContexts.Mapping;

public class InvestmentsMapping : IEntityTypeConfiguration<Investments>
{
    public void Configure(EntityTypeBuilder<Investments> builder)
    {
        builder.ToTable(name:"investments", schema: "statement");

        builder.HasKey(i => i.Id);

        builder.Property(i => i.InvestedBalance)
            .HasColumnType("decimal(18, 2)")
            .IsRequired();

        builder.Property(i => i.InvestedAt)
            .IsRequired();

        builder.HasOne(i => i.Account)
            .WithMany()
            .HasForeignKey(i => i.AccountId);

        builder.HasOne(i => i.CashComposition)
            .WithMany()
            .HasForeignKey(i => i.ProductId);
    }
}