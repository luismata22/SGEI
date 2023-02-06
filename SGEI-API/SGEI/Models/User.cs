using Microsoft.EntityFrameworkCore;

namespace SGEI.Models
{
    public class User
    {
      public int Id { get; set; }

      public string Nombres { get; set; }

      public string Apellidos { get; set; }

      public string Correo { get; set; }

      public string Cedula { get; set; }

      public string Activo { get; set; }

      public string Clave { get; set; }
    }
}
