using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGEI.Models.Master
{
  public class Person
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long id { get; set; }

    public string nombres { get; set; }

    public string apellidos { get; set; }

    public string cedula { get; set; }

    public DateTime fechanacimiento { get; set; }

    public string telefono { get; set; }

    public string correo { get; set; }

    public string direccion { get; set; }

    public string profesion { get; set; }
  }
}
