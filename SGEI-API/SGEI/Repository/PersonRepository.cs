using SGEI.Context;
using SGEI.Interfaces;
using SGEI.Models.Master;
using System.Collections.Generic;
using System.Linq;

namespace SGEI.Repository
{
  public class PersonRepository : IPersonRepository
  {
    private readonly SGEIContext _context;
    public PersonRepository(SGEIContext context)
    {
      _context = context;
    }

    public List<Person> GetPersons()
    {
      var personas = _context.personas.ToList();
      return personas;
    }
  }
}
