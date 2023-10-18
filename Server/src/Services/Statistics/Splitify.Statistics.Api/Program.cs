using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Splitify.Shared.AspDotNet.Middlewares;
using Splitify.Statistics.Api.Consumers;
using Splitify.Statistics.Api.Infrastructure;
using System.Text;

namespace Splitify.Statistics.Api
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

            builder.Services.AddDbContext<ApplicationDbContext>(cfg =>
            {
                cfg.UseNpgsql(builder.Configuration.GetConnectionString("ApplicationDbContext"));
            });

            // inject event bus
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
                    cfg.ReceiveEndpoint("statistics-service", c =>
                    {
                        c.Consumer<CampaignCreatedConsumer>(ctx);
                        c.Consumer<CampaignDeletedConsumer>(ctx);
                    });
                });

                c.AddConsumer<CampaignCreatedConsumer>();
                c.AddConsumer<UniqueVisitorRegisteredConsumer>();
                c.AddConsumer<VisitorRegisteredConsumer>();
                c.AddConsumer<CampaignDeletedConsumer>();
            });

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Bearer";
                options.DefaultChallengeScheme = "Bearer";
            }).AddJwtBearer(options =>
            {
                var vars = builder.Configuration.GetSection("Jwt");
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = true,
                    ValidateIssuer = true,

                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(vars["Secret"])),
                    ValidAudience = vars["Audience"],
                    ValidIssuer = vars["Issuer"],

                    ClockSkew = TimeSpan.Zero,
                    LifetimeValidator = (notBefore, expires, token, parameters) =>
                    {
                        if (expires.HasValue && expires.Value < DateTime.UtcNow)
                        {
                            return false;
                        }
                        return true;
                    }
                };
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseCors(cfg =>
            {
                cfg.AllowAnyHeader();
                cfg.AllowAnyMethod();
                cfg.AllowAnyOrigin();
            });

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}