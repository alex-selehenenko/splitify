using Splitify.Redirect.Domain.Factories;

namespace Splitify.Redirect.UnitTests.Domain
{
    public class DestinationFactoryTests
    {
        [Test]
        [TestCase("http://google.com")]
        [TestCase("https://google.com")]
        public void Create_ValidUrl_Success(string url)
        {
            var destinationFactory = new DestinationFactory();

            var actual = destinationFactory.Create("dest", url, DateTime.Now);

            Assert.Multiple(() =>
            {
                Assert.That(actual.IsSuccess, Is.True);
                Assert.That(actual.Value, Is.Not.Null);
                Assert.That(actual.Value.Id, Is.EqualTo("dest"));
                Assert.That(actual.Value.Url, Is.EqualTo(url));
            });
        }

        [Test]
        [TestCase(" https://google.com")]
        [TestCase("https://google.com ")]
        public void Create_ValidUrlWithLeadingTrailingSpaces_UrlsAreTrimmed(string url)
        {
            var expectedUrl = url.Trim();
            var destinationFactory = new DestinationFactory();

            var actual = destinationFactory.Create("dest", url, DateTime.Now);
            
            Assert.Multiple(() =>
            {
                Assert.That(actual.IsSuccess, Is.True);
                Assert.That(actual.Value, Is.Not.Null);
                Assert.That(actual.Value.Id, Is.EqualTo("dest"));
                Assert.That(actual.Value.Url, Is.EqualTo(expectedUrl));
            });
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("\n")]
        [TestCase("https:/google.com")]
        [TestCase("https://google")]
        [TestCase("https://google.")]
        public void Create_InvalidUrl_Failed(string url)
        {
            var destinationFactory = new DestinationFactory();

            var actual = destinationFactory.Create("dest", url, DateTime.Now);

            Assert.Multiple(() =>
            {
                Assert.That(actual.IsSuccess, Is.False);
            });
        }
    }
}
