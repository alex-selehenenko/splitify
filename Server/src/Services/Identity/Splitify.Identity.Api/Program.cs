using Microsoft.EntityFrameworkCore;
using Splitify.Identity.Application.Commands;
using Splitify.Identity.Application.Services;
using Splitify.Identity.Domain;
using Splitify.Identity.Infrastructure;
using Splitify.Identity.Infrastructure.Repositories;
using Splitify.Shared.Services.Misc;
using Splitify.Shared.Services.Misc.Implementation;

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

            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining<CreateUserCommand>();
            });

            builder.Services.AddSingleton<IDateTimeService, DateTimeService>();
            builder.Services.AddSingleton<IJwtService, JwtService>(x =>
                new JwtService(new("this-issuer", "this-aud", "this-secret", 80000000)));
            
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddDbContext<ApplicationDbContext>(cfg =>
            {
                cfg.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationDbContext"));
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}