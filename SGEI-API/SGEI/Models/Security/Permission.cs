using System.ComponentModel.DataAnnotations;

namespace SGEI.Models.Security
{
  public class Permission
  {
    public int id { get; set; }

    public string nombre { get; set; }

    public bool activo { get; set; }
  }
}
