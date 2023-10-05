namespace Splitify.EmailSender
{
    public interface IHttpEmailClient
    {
        Task SendAsync(string subject, string body, string recipient);
    }
}
