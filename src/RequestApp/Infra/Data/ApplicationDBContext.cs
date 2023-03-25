namespace RequestApp.Infra.Data;

public class ApplicationDBContext : IdentityDbContext<IdentityUser>
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Ignore<Notification>();

        modelBuilder.Entity<Product>()
            .Property(p => p.Name).IsRequired();
        modelBuilder.Entity<Product>()
            .Property(p => p.Description).HasMaxLength(255);
        modelBuilder.Entity<Category>()
            .Property(c => c.CreatedBy).IsRequired();
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<string>()
            .HaveMaxLength(100);
    }
}
