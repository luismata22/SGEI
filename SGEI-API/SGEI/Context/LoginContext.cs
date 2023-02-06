using Microsoft.EntityFrameworkCore;
using SGEI.Models;
using SGEI.Models.Authenticate;
using System.Threading.Tasks;

namespace SGEI.Context
{
  public class LoginContext : DbContext
  {
    public DbSet<User> usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseNpgsql(CONNECTION_STRING);
      base.OnConfiguring(optionsBuilder);
    }
    private const string CONNECTION_STRING = "Host=localhost;Port=5432;" +
                "Username=postgres;" +
                "Password=ci24598291;" +
                "Database=SGEI";

  }
}
