using Splitify.Shared.Services.Misc.Implementation;

namespace Splitify.Shared.Services.UnitTests.Misc
{
    public class EncodingServiceTests
    {
        [Test]
        public void EncodeDecode_PassClass_ClassDecoded()
        {
            var encoder = new EncodingService();
            var source = "abc+def";

            var encoded = encoder.Encode(source);
            var decoded = encoder.Decode(encoded);

            Assert.That(source, Is.EqualTo(decoded));
        }
    }
}
