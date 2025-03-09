namespace Statement.Data.DbContexts.Context;

public class StatementDbContext : DbContext
{
    public DbSet<Account> Accounts { get; set; } = null!;
    public DbSet<CashComposition> CashCompositions { get; set; } = null!;
    public DbSet<Investments> InvestmentStatement { get; set; } = null!;

    public StatementDbContext() { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connection;
#if DEBUG
        connection = StatementConfig.Database.Statement;
#endif

        optionsBuilder.UseNpgsql(
            connection,
            options => options.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorCodesToAdd: null));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new AccountMapping());
        modelBuilder.ApplyConfiguration(new CashCompositionMapping());
        modelBuilder.ApplyConfiguration(new InvestmentsMapping());

        modelBuilder.Entity<Account>()
            .Property<string>("Data")
            .HasColumnType("JSONB");

        modelBuilder.Entity<Account>()
            .Property<DateTimeOffset>("CreatedAt")
            .HasColumnName("created_at")
            .HasColumnType("timestamp with time zone");

        modelBuilder.Entity<Account>()
            .Property<DateTimeOffset>("UpdatedAt")
            .HasColumnName("updated_at")
            .HasColumnType("timestamp with time zone");

        modelBuilder.NamesToSnakeCase();
        modelBuilder.ConfigurePostgresFields();
    }
}