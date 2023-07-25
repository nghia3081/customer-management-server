using BusinessObject;
using Microsoft.Extensions.Options;
using Repository.IRepositories.IServices;
using System.Net;
using System.Net.Mail;

namespace Repository.Repositories.Services
{
    internal class EmailService : IEmailService
    {
        private static NetworkCredential networkCredential;
        private static SmtpClient smtpClient { get; set; }
        private readonly AppSetting appSetting;
        public EmailService(IOptions<AppSetting> options)
        {
            this.appSetting = options.Value;
            if (smtpClient == null)
            {
                networkCredential = new NetworkCredential()
                {
                    UserName = appSetting.HostMail.Account,
                    Password = appSetting.HostMail.Password
                };
                smtpClient = new SmtpClient()
                {
                    Host = appSetting.HostMail.Host,
                    Port = appSetting.HostMail.Port,
                    EnableSsl = appSetting.HostMail.UseSsl,
                    Credentials = networkCredential
                };
            }
        }
        public MailMessage CreateMailMessage(string subject, string body, bool isHtml = true, params string[] receivers)
        {
            MailMessage mailMessage = new MailMessage()
            {
                Body = body,
                Subject = subject,
                IsBodyHtml = isHtml,
            };
            mailMessage.From = new MailAddress(appSetting.HostMail.Account, appSetting.HostMail.DisplayName);
            mailMessage.To.Add(string.Join(",", receivers));
            return mailMessage;
        }
        public MailMessage AttachFile(MailMessage mailMessage, byte[] fileBytes, string contentType = "application/pdf")
        {
            using (var ms = new MemoryStream(fileBytes))
            {
                Attachment attachment = new Attachment(ms, contentType: new System.Net.Mime.ContentType(contentType));
                return AttachFile(mailMessage, attachment);
            }

        }
        public MailMessage AttachFile(MailMessage mailMessage, params Attachment[] attachments)
        {
            mailMessage.Attachments.Concat(attachments);
            return mailMessage;
        }
        public async Task SendMessage(MailMessage mailMessage)
        {
            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
