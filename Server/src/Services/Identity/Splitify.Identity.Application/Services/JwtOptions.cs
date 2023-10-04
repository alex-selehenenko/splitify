namespace Splitify.Identity.Application.Services
{
    public record JwtOptions(string Issuer, string Audience, string Secret, int Lifetime);
}
