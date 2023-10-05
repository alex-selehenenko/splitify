using MassTransit;
using Splitify.EventBus.Contracts;

namespace Splitify.EmailSender.Consumers
{
    public class UserCreatedConsumer : IConsumer<UserCreatedMessage>
    {
        private readonly IHttpEmailClient _client;

        public UserCreatedConsumer(IHttpEmailClient client)
        {
            _client = client;
        }

        public async Task Consume(ConsumeContext<UserCreatedMessage> context)
        {
            var subject = "Verify your account on Splitify";
            var body = $"Your verification code is <b>{context.Message.VerificationCode}</b>. It's valid for 10 minutes";

            await _client.SendAsync(subject, body, context.Message.Email);
        }
    }
}
