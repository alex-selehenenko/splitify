using Microsoft.EntityFrameworkCore;
using Splitify.Redirect.Application;
using Splitify.Redirect.Domain;
using Splitify.Redirect.Infrastructure;
using Splitify.Redirect.Infrastructure.Repositories;
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
                cfg.RegisterServicesFromAssemblyContaining<ApplicationMarker>());

            // infrastructure dependencies
            builder.Services.AddScoped<IRedirectionRepository, RedirectionRepository>();
            builder.Services.AddDbContext<ApplicationDbContext>(cfg =>
            { 
                var connectionString = builder.Configuration.GetConnectionString("LocalDb");
                cfg.UseSqlServer(connectionString);
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