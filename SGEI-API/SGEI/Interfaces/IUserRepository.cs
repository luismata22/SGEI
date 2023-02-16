using SGEI.Models;
using SGEI.Models.Security.Filters;
using System.Collections.Generic;

namespace SGEI.Interfaces
{
  public interface IUserRepository
  {
    List<User> GetUsers(UserFilters filter);

    long Post(User model);
  }
}
