using SGEI.Context;
using SGEI.Interfaces;
using SGEI.Models.Master;
using System.Collections.Generic;
using System.Linq;

namespace SGEI.Repository
{
  public class StudentRepository : IStudentRepository
  {
    private readonly SGEIContext _context;
    public StudentRepository(SGEIContext context)
    {
      _context = context;
    }

    public List<TypeCourse> GetTypeCourse()
    {
      var typeCourses = _context.tipocurso.ToList();
      return typeCourses;
    }
  }
}
