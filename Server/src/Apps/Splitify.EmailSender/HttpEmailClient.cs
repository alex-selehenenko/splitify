namespace Splitify.EmailSender
{
    public class HttpEmailClient : IEmailClient
    {
        private readonly HttpEmailClientOptions _options;
        private readonly ILogger<HttpEmailClient> _logger;

        public HttpEmailClient(IConfiguration config, ILogger<HttpEmailClient> logger)
        {
            var smtpSection = config.GetSection("SmtpClient");

            var path = smtpSection["ApiPath"];
            var key = smtpSection["ApiKey"];
            var email = smtpSection["SenderEmail"];
            var name = smtpSection["SenderName"];
            
            _options = new HttpEmailClientOptions(path, key, email, name);
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
                using var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Authorization = new(_options.ApiKey);
                
                await httpClient.PostAsync(_options.Path, formData);
                
                _logger.LogInformation("Successfully sent email to {email}", recipient);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send email to {email}. See the exception details", recipient);
            }
        }
    }
}
