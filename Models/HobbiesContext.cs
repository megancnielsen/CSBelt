using CSBelt.Models;
using Microsoft.EntityFrameworkCore;

namespace Forum.Models
{
  public class CSBeltContext : DbContext
  {
    public CSBeltContext(DbContextOptions options) : base(options) { }
    // tables in db
    public DbSet<User> Users { get; set; }
    public DbSet<Hobby> Hobbies { get; set; }
    public DbSet<Add> Adds { get; set; }
  }
}