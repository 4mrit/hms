namespace hms.Identity.API.Services.Interfaces;
public interface IEmailSender
{
  string SendEmail(string to, string subject, string body);
}
