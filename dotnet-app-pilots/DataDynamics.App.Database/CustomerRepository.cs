using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DataDynamics.App.Database;

public class CustomerRepository : DbContext
{
    public DbSet<Customer> Customers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=localhost;Database=test;Username=postgres;Password=Clave:123");
}

public class Customer
{
    public int Id { get; set; }
    public string FullName { get; set; }
}