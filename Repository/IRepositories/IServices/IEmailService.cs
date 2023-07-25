using System.Net.Mail;

namespace Repository.IRepositories.IServices
{
    public interface IEmailService
    {
        MailMessage CreateMailMessage(string subject, string body, bool isHtml = true, params string[] receivers);
        MailMessage AttachFile(MailMessage mailMessage, params Attachment[] attachments);
        MailMessage AttachFile(MailMessage mailMessage, byte[] fileBytes, string contentType = "application/pdf");
        Task SendMessage(MailMessage mailMessage);
    }
}
