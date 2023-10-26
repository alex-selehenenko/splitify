using Splitify.Campaign.Domain.Factories;
using Splitify.Shared.Services.Misc.Implementation;

namespace Splitify.Campaign.UnitTests.Domain
{
    public class LinkFactoryTests
    {
        [TestCase("https://google.com")]
        [TestCase("http://google.com")]
        [Test]
        public void Create_ValidUrl_Success(string url)
        {
            var actual = LinkFactory.Create(url, new DateTimeService());
            Assert.That(actual.IsSuccess, Is.True);
        }

        [Test]
        [TestCase(" https://google.com")]
        [TestCase("https://google.com ")]
        public void Create_ValidUrlWithLeadingTrailingSpaces_UrlsAreTrimmed(string url)
        {
            var expectedUrl = url.Trim();

            var actual = LinkFactory.Create(url, new DateTimeService());

            Assert.Multiple(() =>
            {
                Assert.That(actual.IsSuccess, Is.True);
                Assert.That(actual.Value, Is.Not.Null);
                Assert.That(actual.Value.Url, Is.EqualTo(expectedUrl));
            });
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("\n")]
        [TestCase("https:/google.")]
        public void Create_InvalidUrl_Failed(string url)
        {
            var actual = LinkFactory.Create(url, new DateTimeService());

            Assert.Multiple(() =>
            {
                Assert.That(actual.IsSuccess, Is.False);
            });
        }
    }
}