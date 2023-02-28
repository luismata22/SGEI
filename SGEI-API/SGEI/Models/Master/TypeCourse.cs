using System.ComponentModel.DataAnnotations;

namespace SGEI.Models.Master
{
  public class TypeCourse
  {
    [Key]
    public int id { get; set; }

    public string nombre { get; set; }

    public bool activo { get; set; }
  }
}
