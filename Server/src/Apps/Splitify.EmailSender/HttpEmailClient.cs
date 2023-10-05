namespace Splitify.EmailSender
{
    public class HttpEmailClient : HttpClient, IHttpEmailClient
    {
        private readonly HttpEmailClientOptions _options;

        public HttpEmailClient(HttpEmailClientOptions options)
        {
            _options = options;
            DefaultRequestHeaders.Authorization = new(_options.ApiKey);
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

            await PostAsync(_options.Path, formData);
        }
    }
}
