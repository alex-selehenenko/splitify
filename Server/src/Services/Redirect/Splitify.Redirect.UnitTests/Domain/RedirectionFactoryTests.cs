using Splitify.Redirect.Domain.Factories;

namespace Splitify.Redirect.UnitTests.Domain
{
    public class RedirectionFactoryTests
    {
        [Test]
        public void Create_ValidParameters_Success()
        {
            var destinationFactory = new DestinationFactory();
            
            var firstDest = destinationFactory.Create("1", "https://google.com", DateTime.Now).Value;
            var secondDest = destinationFactory.Create("2", "https://google.com", DateTime.Now).Value;

            var factory = new RedirectionFactory();

            var redirectionResult = factory.Create("camp_1", new[] { firstDest, secondDest }, DateTime.Now);

            Assert.That(redirectionResult.IsSuccess, Is.True);
        }

        [Test]
        [TestCase("")]
        [TestCase("\t")]
        [TestCase(" ")]
        [TestCase(null)]
        public void Create_InvalidCampaignId_Failed(string campaignId)
        {
            var destinationFactory = new DestinationFactory();

            var firstDest = destinationFactory.Create("1", "https://google.com", DateTime.Now).Value;
            var secondDest = destinationFactory.Create("2", "https://google.com", DateTime.Now).Value;

            var factory = new RedirectionFactory();

            var redirectionResult = factory.Create(campaignId, new[] { firstDest, secondDest }, DateTime.Now);

            Assert.That(redirectionResult.IsSuccess, Is.False);
        }

        [Test]
        public void Create_InvalidDestinationsCount_Failed()
        {
            var destinationFactory = new DestinationFactory();

            var firstDest = destinationFactory.Create("1", "https://google.com", DateTime.Now).Value;

            var factory = new RedirectionFactory();

            var redirectionResult = factory.Create("camp_1", new[] { firstDest }, DateTime.Now);

            Assert.That(redirectionResult.IsSuccess, Is.False);
        }

        [Test]
        public void Create_DestinationsAreNull_Failed()
        {
            var factory = new RedirectionFactory();

            var redirectionResult = factory.Create("camp_1", null, DateTime.Now);

            Assert.That(redirectionResult.IsSuccess, Is.False);
        }
    }
}
