using Microsoft.EntityFrameworkCore;
using SGEI.Models;

namespace SGEI.Context
{
  public class SGEIContext : DbContext
  {
    public SGEIContext(DbContextOptions<SGEIContext> options) : base(options)
    {

    }

    public DbSet<User> usuarios => Set<User>();
  }
}
