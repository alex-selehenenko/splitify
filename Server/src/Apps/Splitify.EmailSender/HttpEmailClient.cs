namespace Splitify.EmailSender
{
    public class HttpEmailClient : HttpClient, IEmailClient
    {
        private readonly HttpEmailClientOptions _options;
        private readonly ILogger<HttpEmailClient> _logger;

        public HttpEmailClient(HttpEmailClientOptions options, ILogger<HttpEmailClient> logger)
        {
            _options = options;
            DefaultRequestHeaders.Authorization = new(_options.ApiKey);
            _logger = logger;

        }

        public async Task SendAsync(string subject, string body, string recipient)
        {
            var formData = new MultipartFormDataContent
            {
                { new StringContent(_options.SenderName), "name" },
                { new StringContent(_options.SenderEmail), "from" },
                { new StringContent(subject), "subject" },
                { new StringContent(recipient), "to" },
                { new StringContent(body), "html" }
            };
            try
            {
                await PostAsync(_options.Path, formData);
                _logger.LogInformation("Successfully sent email to {email}", recipient);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send email to {email}. See the exception details", recipient);
            }
        }
    }
}
