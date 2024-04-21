using MailKit.Net.Smtp;
using MimeKit;
using MailKit.Security;
using hms.Identity.API.Services.Interfaces;
// using System.Net.Mail;
// using System.Net;

namespace hms.Identity.API.Services;
public class SMTPEmailSender : IEmailSender
{

  private string _smtpServerAddress = "smtp.gmail.com";
  private int _smtpServerPort = 587;
  private string _mailSenderPassword = "fwwv iuhe dtin fvau";
  private string _mailSenderAddress =
      "hospital.management.system.nepal@gmail.com";

  public async Task SendEmailAsync(string to, string title, string body)
  {
    var email = new MimeMessage();
    email.From.Add(new MailboxAddress("Hospital", _mailSenderAddress));
    email.To.Add(new MailboxAddress("Customer", to));
    email.Subject = title;
    email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = body };

    using (var smtp = new SmtpClient())
    {
      smtp.Connect(_smtpServerAddress, _smtpServerPort,
                   SecureSocketOptions.StartTls);
      smtp.Authenticate(_mailSenderAddress, _mailSenderPassword);
      smtp.Send(email);
      smtp.Disconnect(true);
    }
  }
}
