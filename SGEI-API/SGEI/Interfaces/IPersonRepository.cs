using SGEI.Models.Master;
using System.Collections.Generic;

namespace SGEI.Interfaces
{
  public interface IPersonRepository
  {
    List<Person> GetPersons();
  }
}
