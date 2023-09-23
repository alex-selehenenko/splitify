using System.Text;

namespace Splitify.Shared.Services.Misc.Implementation
{
    public class EncodingService : IEncodingService
    {
        public string Decode(string encoded)
        {
            byte[] decodedBytes = Convert.FromBase64String(encoded);
            return Encoding.UTF8.GetString(decodedBytes);
        }

        public string Encode(string source)
        {
            byte[] bytesToEncode = Encoding.UTF8.GetBytes(source);
            return Convert.ToBase64String(bytesToEncode);
        }
    }
}
