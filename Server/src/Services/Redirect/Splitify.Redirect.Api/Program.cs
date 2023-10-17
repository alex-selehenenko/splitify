using MassTransit;
using Microsoft.EntityFrameworkCore;
using Splitify.BuildingBlocks.EventBus;
using Splitify.EventBus.MassTransit;
using Splitify.Redirect.Api.Consumers;
using Splitify.Redirect.Application.Commands;
using Splitify.Redirect.Domain;
using Splitify.Redirect.Infrastructure;
using Splitify.Redirect.Infrastructure.Repositories;
using Splitify.Shared.AspDotNet.Middlewares;
using Splitify.Shared.Services.Misc;
using Splitify.Shared.Services.Misc.Implementation;

namespace Splitify.Redirect.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSingleton<IDateTimeService, DateTimeService>();

            // application dependencies
            builder.Services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssemblyContaining<CreateRedirectCommand>());

            // infrastructure dependencies
            builder.Services.AddScoped<IRedirectRepository, RedirectRepository>();
            builder.Services.AddDbContext<ApplicationDbContext>(cfg =>
            { 
                var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContext");
                cfg.UseNpgsql(connectionString);
            });

            // event bus dependencies
            builder.Services.AddScoped<IEventBus, MassTransitEventBus>();
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

                c.AddConsumer<CampaignCreatedConsumer>();
                c.AddConsumer<CampaignActivatedConsumer>();
                c.AddConsumer<CampaignDeactivatedConsumer>();
                c.AddConsumer<CampaignDeletedConsumer>();
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}