namespace Splitify.EmailSender
{
    public record HttpEmailClientOptions(string Path, string ApiKey, string SenderEmail, string SenderName);
}
