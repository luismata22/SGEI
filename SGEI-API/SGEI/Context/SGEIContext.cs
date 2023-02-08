using Microsoft.EntityFrameworkCore;
using SGEI.Models;
using SGEI.Models.Authenticate;

namespace SGEI.Context
{
  public class SGEIContext : DbContext
  {
    public SGEIContext(DbContextOptions<SGEIContext> options) : base(options)
    {

    }

    public DbSet<User> usuarios => Set<User>();

    public DbSet<CodesResetPasswordxUsers> codigoresetearpasswordxusuario => Set<CodesResetPasswordxUsers>();
  }
}
