using System;

namespace CityInfo.API.Services
{
    public class CloudMailService : IMailService
    {
        private readonly string _SendTo = "amirhosseinhasanloo2005@gmail.com";
        private readonly string _SendFromTo = "amirhosseinhasanloo2005@gmail.com";

        public CloudMailService(IConfiguration configuration)
        {
            _SendFromTo = configuration["MailSetting:MailFrom"];
            _SendTo = configuration["MailSetting:MailTo"];
        }


        public void Send(string subject, string body)
        {
            Console.WriteLine($"mail from {_SendFromTo} sended to {_SendTo} " +
                              $",with {nameof(LocalMailService)} in cloud mail service");

            Console.WriteLine($"subject {subject}");
            Console.WriteLine($"body {body}");
        }
    }
}