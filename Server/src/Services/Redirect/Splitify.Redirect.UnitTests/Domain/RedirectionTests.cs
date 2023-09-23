using Splitify.Redirect.Domain.Factories;

namespace Splitify.Redirect.UnitTests.Domain
{
    public class RedirectionTests
    {
        [Test]
        public void GetLeastVisitedDestination_TwoIsMoreVisited_ReturnsLeastVisited()
        {
            var destinationFactory = new DestinationFactory();

            var firstDestination = destinationFactory.Create("first", "https://google.com", DateTime.UtcNow).Value;
            var secondDestination = destinationFactory.Create("second", "https://google.com", DateTime.UtcNow).Value;
            var thirdDestination = destinationFactory.Create("third", "https://google.com", DateTime.UtcNow).Value;

            firstDestination.RegisterUniqueVisitor();
            thirdDestination.RegisterUniqueVisitor();

            var redirectionFactory = new RedirectionFactory();

            var redirection = redirectionFactory.Create(
                "campaign",
                new[] { firstDestination, secondDestination, thirdDestination },
                DateTime.UtcNow).Value;

            var actual = redirection.GetLeastVisitedDestination();

            Assert.That(actual.Id, Is.EqualTo(secondDestination.Id));
        }

        [Test]
        public void GetLeastVisitedDestination_SameUniqueVisitors_ReturnsFirst()
        {
            var destinationFactory = new DestinationFactory();

            var firstDestination = destinationFactory.Create("first", "https://google.com", DateTime.UtcNow).Value;
            var secondDestination = destinationFactory.Create("second", "https://google.com", DateTime.UtcNow).Value;
            var thirdDestination = destinationFactory.Create("third", "https://google.com", DateTime.UtcNow).Value;

            firstDestination.RegisterUniqueVisitor();
            secondDestination.RegisterUniqueVisitor();
            thirdDestination.RegisterUniqueVisitor();

            var redirectionFactory = new RedirectionFactory();

            var redirection = redirectionFactory.Create(
                "campaign",
                new[] { firstDestination, secondDestination, thirdDestination },
                DateTime.UtcNow).Value;

            var actual = redirection.GetLeastVisitedDestination();

            Assert.That(actual.Id, Is.EqualTo(firstDestination.Id));
        }
    }
}
