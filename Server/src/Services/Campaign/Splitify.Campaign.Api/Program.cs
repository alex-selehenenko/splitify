using Microsoft.EntityFrameworkCore;
using Splitify.Campaign.Application.Commands;
using Splitify.Campaign.Domain;
using Splitify.Campaign.Infrastructure;
using Splitify.Shared.AspDotNet.Identity;
using Splitify.Shared.Services.Identity;
using Splitify.Shared.Services.Misc;
using Splitify.Shared.Services.Misc.Implementation;

namespace Splitify.Campaign.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // inject identity
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<IUserService, ClaimsUserService>();

            // inject application
            builder.Services.AddSingleton<IDateTimeService, DateTimeService>();
            builder.Services.AddSingleton<IRandomStringService, RandomStringService>();
            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining<CreateCampaignCommand>();
            });

            // inject infrastructure
            builder.Services.AddScoped<ICampaignRepository, CampaignRepository>();
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