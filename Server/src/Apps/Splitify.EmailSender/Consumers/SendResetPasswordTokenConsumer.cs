using MassTransit;
using Splitify.EventBus.Contracts;

namespace Splitify.EmailSender.Consumers
{
    public class SendResetPasswordTokenConsumer
        : IConsumer<SendResetPasswordTokenMessage>
    {
        private readonly IEmailClient _client;

        public SendResetPasswordTokenConsumer(IEmailClient client)
        {
            _client = client;
        }

        public async Task Consume(ConsumeContext<SendResetPasswordTokenMessage> context)
        {
            await _client.SendAsync(
                "Reset password",
                $"Please follow this link to reset password: {context.Message.ResetUrl}",
                context.Message.Email);
        }
    }
}
