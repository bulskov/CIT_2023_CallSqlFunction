using Microsoft.EntityFrameworkCore;

namespace CallSqlFunction;

public class NorthwindContex : DbContext
{
    private readonly string _connectionString;

    public NorthwindContex(string connectionString)
    {
        _connectionString = connectionString;
    }

    public DbSet<Category> Categories { get; set; }

    public DbSet<SerchResult> SerchResults { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().ToTable("categories");
        modelBuilder.Entity<Category>().Property(x => x.Id).HasColumnName("categoryid");
        modelBuilder.Entity<Category>().Property(x => x.Name).HasColumnName("categoryname");
        modelBuilder.Entity<Category>().Property(x => x.Description).HasColumnName("description");

        modelBuilder.Entity<SerchResult>().HasNoKey();
        modelBuilder.Entity<SerchResult>().Property(x => x.Id).HasColumnName("p_id");
        modelBuilder.Entity<SerchResult>().Property(x => x.Name).HasColumnName("p_name");
    }
}
