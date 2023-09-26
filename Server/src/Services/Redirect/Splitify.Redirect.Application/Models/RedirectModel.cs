namespace Splitify.Redirect.Application.Models
{
    public class RedirectModel
    {
        public string Url { get; set; }

        public string Token { get; set; }

        public RedirectModel(string url, string token)
        {
            Url = url ?? throw new ArgumentNullException(nameof(url));
            Token = token ?? throw new ArgumentNullException(nameof(token));
        }
    }
}
