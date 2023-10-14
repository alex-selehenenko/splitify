using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Splitify.EmailSender.Consumers;

namespace Splitify.EmailSender
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var smtpSection = builder.Configuration.GetSection("SmtpClient");

            var path = smtpSection["ApiPath"];
            var key = smtpSection["ApiKey"];
            var email = smtpSection["SenderEmail"];
            var name = smtpSection["SenderName"];

            var options = new HttpEmailClientOptions(path, key, email, name);

            builder.Services.AddHttpClient<IEmailClient, HttpEmailClient>((p, c) =>
                new HttpEmailClient(options, p.GetRequiredService<ILogger<HttpEmailClient>>()));
            
            builder.Services.AddMassTransit(c =>
            {
                var messagingVars = builder.Configuration.GetSection("Messaging");
                c.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(messagingVars["Host"], "/", h => {
                        h.Username(messagingVars["AccessKey"]);
                        h.Password(messagingVars["SecretKey"]);
                    });

                    cfg.ConfigureEndpoints(ctx);
                });

                c.AddConsumer<UserCreatedConsumer>();
                c.AddConsumer<SendResetPasswordTokenConsumer>();
                
            });

            var app = builder.Build();

            app.Run();
        }
    }
}