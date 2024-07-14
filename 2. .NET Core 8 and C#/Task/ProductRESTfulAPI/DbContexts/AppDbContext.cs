using Microsoft.EntityFrameworkCore;
using ProductRESTfulAPI.Models;

namespace ProductRESTfulAPI.DbContexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Product> Products { get; set; }
}