using MassTransit;
using Splitify.EventBus.Contracts;

namespace Splitify.EmailSender.Consumers
{
    public class SendNewVerificationCodeConsumer : IConsumer<SendNewVerificationCodeMessage>
    {
        private readonly IEmailClient _client;
        private readonly ILogger<SendResetPasswordTokenConsumer> _logger;

        public SendNewVerificationCodeConsumer(IEmailClient client, ILogger<SendResetPasswordTokenConsumer> logger)
        {
            _client = client;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<SendNewVerificationCodeMessage> context)
        {
            _logger.LogInformation("Receive message {name}. Start sending email to {email}", nameof(SendNewVerificationCodeMessage), context.Message.Email);
            
            var subject = "Verify your account on Splitify";
            var body = $"Your verification code is <b>{context.Message.Code}</b><br>The code is valid for 10 minutes";

            await _client.SendAsync(subject, body, context.Message.Email);
        }
    }
}
