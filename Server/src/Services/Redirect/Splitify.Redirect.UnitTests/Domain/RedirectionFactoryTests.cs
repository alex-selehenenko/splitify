using Splitify.Redirect.Domain.Factories;

namespace Splitify.Redirect.UnitTests.Domain
{
    public class RedirectionFactoryTests
    {
        [Test]
        public void Create_ValidParameters_Success()
        {            
            var firstDest = DestinationFactory.Create("1", "https://google.com", DateTime.Now).Value;
            var secondDest = DestinationFactory.Create("2", "https://google.com", DateTime.Now).Value;

            var redirectionResult = RedirectFactory.Create("camp_1", new[] { firstDest, secondDest }, DateTime.Now);

            Assert.That(redirectionResult.IsSuccess, Is.True);
        }

        [Test]
        [TestCase("")]
        [TestCase("\t")]
        [TestCase(" ")]
        [TestCase(null)]
        public void Create_InvalidCampaignId_Failed(string campaignId)
        {
            var firstDest = DestinationFactory.Create("1", "https://google.com", DateTime.Now).Value;
            var secondDest = DestinationFactory.Create("2", "https://google.com", DateTime.Now).Value;

            var redirectionResult = RedirectFactory.Create(campaignId, new[] { firstDest, secondDest }, DateTime.Now);

            Assert.That(redirectionResult.IsSuccess, Is.False);
        }

        [Test]
        public void Create_InvalidDestinationsCount_Failed()
        {
            var firstDest = DestinationFactory.Create("1", "https://google.com", DateTime.Now).Value;

            var redirectionResult = RedirectFactory.Create("camp_1", new[] { firstDest }, DateTime.Now);

            Assert.That(redirectionResult.IsSuccess, Is.False);
        }

        [Test]
        public void Create_DestinationsAreNull_Failed()
        {
            var redirectionResult = RedirectFactory.Create("camp_1", null, DateTime.Now);

            Assert.That(redirectionResult.IsSuccess, Is.False);
        }
    }
}
