using SGEI.Models.Utils;
using System.Threading.Tasks;

namespace SGEI.Interfaces.mail
{
  public interface IMailService
  {
    Task SendEmailAsync(MailRequest mailRequest);
  }
}
