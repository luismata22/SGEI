using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGEI.Models.Master
{
  public class PersonsxStudent
  {
    [Key]
    public long id { get; set; }

    public long idpersona { get; set; }

    public long idestudiante { get; set; }

    public bool activo { get; set; }

    [ForeignKey(nameof(idpersona))]
    public Person modulo { get; set; }

    [ForeignKey(nameof(idestudiante))]
    public Student permiso { get; set; }
  }
}
