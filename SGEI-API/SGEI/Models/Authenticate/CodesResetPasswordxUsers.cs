using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGEI.Models.Authenticate
{
  public class CodesResetPasswordxUsers
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long id { get; set; }

    public long idusuario { get; set; }

    public string codigo { get; set; }

    public bool activo { get; set; }
  }
}
