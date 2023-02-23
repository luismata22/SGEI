using SGEI.Models.Master;
using System.Collections.Generic;

namespace SGEI.Interfaces
{
  public interface IStudentRepository
  {
    List<TypeCourse> GetTypeCourse();
  }
}
