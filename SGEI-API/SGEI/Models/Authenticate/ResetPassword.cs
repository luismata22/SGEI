namespace SGEI.Models.Authenticate
{
  public class ResetPassword
  {
    public string User { get; set; }

    public string Code { get; set; }

    public string Password { get; set; }
  }
}
