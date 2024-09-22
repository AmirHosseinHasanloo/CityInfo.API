using System.Net.Mail;

namespace CityInfo.API.Services
{
    public class LocalMailService : IMailService
    {
        private readonly string _SendTo = string.Empty;
        private readonly string _SendFromTo = string.Empty;

        public LocalMailService(IConfiguration configuration)
        {
            _SendFromTo = configuration["MailSetting:MailFrom"];
            _SendTo = configuration["MailSetting:MailTo"];
        }

        public void Send(string subject, string body)
        {
            Console.WriteLine($"mail from {_SendFromTo} sended to {_SendTo} " +
                              $",with {nameof(LocalMailService)}");

            //MailMessage mail = new MailMessage();
            //SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            //mail.From = new MailAddress(_SendFromTo, "امیرحسین");
            //mail.To.Add(_SendTo);
            //mail.Subject = subject;
            //mail.Body = body;
            //mail.IsBodyHtml = true;

            ////System.Net.Mail.Attachment attachment;
            //// attachment = new System.Net.Mail.Attachment("c:/textfile.txt");
            //// mail.Attachments.Add(attachment);

            //SmtpServer.Port = 587;
            //SmtpServer.Credentials = new System.Net.NetworkCredential(_SendFromTo, "vzxqiocewqvivans");
            //SmtpServer.EnableSsl = true;

            //SmtpServer.Send(mail);

            Console.WriteLine($"subject {subject}");
            Console.WriteLine($"body {body}");
        }
    }
}