namespace Splitify.EmailSender
{
    public interface IEmailClient
    {
        Task SendAsync(string subject, string body, string recipient);
    }
}
