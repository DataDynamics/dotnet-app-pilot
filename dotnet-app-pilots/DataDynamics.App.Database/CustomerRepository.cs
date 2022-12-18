using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataDynamics.App.Database;

public class CustomerRepository : DbContext
{
    public DbSet<Customer> Customers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Database=test;Username=postgres;Password=Clave:123");
        optionsBuilder.LogTo(Console.WriteLine);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // TODO : 여기에서 Entity를 추가한다.
        // modelBuilder.Entity<AuditEntry>();
    }
}

[Table("customers", Schema = "public")]
public class Customer
{
    [Column("id")] public long Id { get; set; }

    [Column("full_name")] public string FullName { get; set; }
}