using System;

namespace SGEI.Models.Exceptions
{
  public class InvalidCredentialsException : ArgumentException
  {
    public InvalidCredentialsException()
      : base("Credenciales incorrectas")
    {

    }
  }
}
