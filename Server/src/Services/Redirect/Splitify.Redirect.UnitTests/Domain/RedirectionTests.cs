using Splitify.Redirect.Domain.Factories;
using Splitify.Shared.Services.Misc;
using Splitify.Shared.Services.Misc.Implementation;

namespace Splitify.Redirect.UnitTests.Domain
{
    public class RedirectionTests
    {
        private readonly IDateTimeService _dt = new DateTimeService();

        [Test]
        public void GetLeastVisitedDestination_TwoIsMoreVisited_ReturnsLeastVisited()
        {
            var firstDestination = DestinationFactory.Create("first", "https://google.com/1", DateTime.UtcNow).Value;
            var secondDestination = DestinationFactory.Create("second", "https://google.com/2", DateTime.UtcNow).Value;
            var thirdDestination = DestinationFactory.Create("third", "https://google.com/3", DateTime.UtcNow).Value;

            firstDestination.RegisterUniqueVisitor(_dt);
            thirdDestination.RegisterUniqueVisitor(_dt);

            var redirection = RedirectionFactory.Create(
                "campaign",
                new[] { firstDestination, secondDestination, thirdDestination },
                DateTime.UtcNow).Value;

            var actual = redirection.GetDestinationForUniqueVisitor(_dt);

            Assert.That(actual.Value.Id, Is.EqualTo(secondDestination.Id));
        }

        [Test]
        public void GetLeastVisitedDestination_SameUniqueVisitors_ReturnsFirst()
        {
            var firstDestination = DestinationFactory.Create("first", "https://google.com/1", DateTime.UtcNow).Value;
            var secondDestination = DestinationFactory.Create("second", "https://google.com/2", DateTime.UtcNow).Value;
            var thirdDestination = DestinationFactory.Create("third", "https://google.com/3", DateTime.UtcNow).Value;

            firstDestination.RegisterUniqueVisitor(_dt);
            secondDestination.RegisterUniqueVisitor(_dt);
            thirdDestination.RegisterUniqueVisitor(_dt);

            var redirection = RedirectionFactory.Create(
                "campaign",
                new[] { firstDestination, secondDestination, thirdDestination },
                DateTime.UtcNow).Value;

            var actual = redirection.GetDestinationForUniqueVisitor(_dt);

            Assert.That(actual.Value.Id, Is.EqualTo(firstDestination.Id));
        }
    }
}
