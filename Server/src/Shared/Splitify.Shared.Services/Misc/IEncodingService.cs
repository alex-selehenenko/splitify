namespace Splitify.Shared.Services.Misc
{
    public interface IEncodingService
    {
        string Encode(string source);

        string Decode(string encoded);
    }
}
