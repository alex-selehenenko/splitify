using MassTransit;
using Splitify.EventBus.Contracts;

namespace Splitify.EmailSender.Consumers
{
    public class SendResetPasswordTokenConsumer
        : IConsumer<SendResetPasswordTokenMessage>
    {
        private readonly IEmailClient _client;
        private readonly ILogger<SendResetPasswordTokenConsumer> _logger;

        public SendResetPasswordTokenConsumer(
            IEmailClient client,
            ILogger<SendResetPasswordTokenConsumer> logger)
        {
            _client = client;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<SendResetPasswordTokenMessage> context)
        {
            _logger.LogInformation("Receive message {name}. Start sending email to {email}", nameof(SendResetPasswordTokenMessage), context.Message.Email);
            await _client.SendAsync(
                "Reset password",
                $"Please follow this link to reset password: {context.Message.ResetUrl}",
                context.Message.Email);
        }
    }
}
