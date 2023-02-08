using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGEI.Models.Authenticate
{
  public class CodesResetPasswordxUsers
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    public long IdUsuario { get; set; }

    public string Codigo { get; set; }

    public bool Activo { get; set; }
  }
}
