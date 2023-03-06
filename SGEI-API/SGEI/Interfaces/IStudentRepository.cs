using SGEI.Models.Master;
using SGEI.Models.Master.Filters;
using System.Collections.Generic;

namespace SGEI.Interfaces
{
  public interface IStudentRepository
  {
    List<TypeCourse> GetTypeCourse();

    List<Student> GetStudents(StudentFilters filter);

    Student GetStudentById(long idStudent);

    long Post(Student model);
  }
}
