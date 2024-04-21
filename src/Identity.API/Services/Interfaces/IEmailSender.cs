namespace hms.Identity.API.Services.Interfaces;
public interface IEmailSender
{
  Task SendEmailAsync(string to, string subject, string body);
}
