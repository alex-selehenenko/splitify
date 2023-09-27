namespace Splitify.Redirect.Application.Models
{
    public class DestinationModel
    {
        public string Url { get; set; }

        public string Id { get; set; }

        public DestinationModel(string url, string id)
        {
            Url = url ?? throw new ArgumentNullException(nameof(url));
            Id = id ?? throw new ArgumentNullException(nameof(id));
        }
    }
}
