using Casino.Helper;

namespace Casino.IRepository
{
    public interface IEmailService
    {
        Task SendEmail(MailRequest mailRequest);
    }
}
