namespace hms.Identity.API.Services;
public interface IEmailSender {
  Task SendEmailAsync(string to, string subject, string body);
}
