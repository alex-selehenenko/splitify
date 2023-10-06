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
                new HttpEmailClient(options));
            
            builder.Services.AddMassTransit(c =>
            {
                c.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host("localhost", "/", h => {
                        h.Username("guest");
                        h.Password("guest");
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