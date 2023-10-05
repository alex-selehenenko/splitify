using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Splitify.BuildingBlocks.EventBus;
using Splitify.EventBus.MassTransit;
using Splitify.Identity.Application.Commands;
using Splitify.Identity.Application.Services;
using Splitify.Identity.Domain;
using Splitify.Identity.Infrastructure;
using Splitify.Identity.Infrastructure.Repositories;
using Splitify.Shared.AspDotNet.Identity;
using Splitify.Shared.Services.Identity;
using Splitify.Shared.Services.Misc;
using Splitify.Shared.Services.Misc.Implementation;
using System.Text;

namespace Splitify.Identity.Api
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
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddScoped<IEventBus, MassTransitEventBus>();
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
            });

            builder.Services.AddScoped<IUserService, ClaimsUserService>();
            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining<CreateUserCommand>();
            });

            builder.Services.AddSingleton<IDateTimeService, DateTimeService>();

            var jwtVars = builder.Configuration.GetSection("Jwt");
            builder.Services.AddSingleton<IJwtService, JwtService>(x =>
                new JwtService(new JwtOptions(
                    jwtVars["Issuer"],
                    jwtVars["Audience"],
                    jwtVars["Secret"],
                    604800000)));
            
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddDbContext<ApplicationDbContext>(cfg =>
            {
                cfg.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationDbContext"));
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

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors(cfg =>
            {
                cfg.AllowAnyOrigin();
                cfg.AllowAnyMethod();
                cfg.AllowAnyHeader();
            });

            app.MapControllers();

            app.Run();
        }
    }
}