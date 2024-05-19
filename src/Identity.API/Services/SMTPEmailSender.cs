using MailKit.Net.Smtp;
using MimeKit;
using MailKit.Security;
using thms.Identity.API.Services.Interfaces;
// using System.Net.Mail;
// using System.Net;

namespace thms.Identity.API.Services;
public class SMTPEmailSender : IEmailSender
{

  private string _smtpServerAddress = "smtp.gmail.com";
  private int _smtpServerPort = 587;
  private string _mailSenderPassword = "fwwv iuhe dtin fvau";
  private string _mailSenderAddress =
      "hospital.management.system.nepal@gmail.com";

  public string SendEmail(string to, string title, string body)
  {
    var email = new MimeMessage();
    email.From.Add(new MailboxAddress("Hospital", _mailSenderAddress));
    email.To.Add(new MailboxAddress("Customer", to));
    email.Subject = title;
    email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = body };
    string result;
    using (var smtp = new SmtpClient())
    {
      smtp.Connect(_smtpServerAddress, _smtpServerPort,
                   SecureSocketOptions.StartTls);
      smtp.Authenticate(_mailSenderAddress, _mailSenderPassword);
      result = smtp.Send(email);
      smtp.Disconnect(true);
    }
    return result;
  }
}
