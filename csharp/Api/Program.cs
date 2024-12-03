using System.Net.Mime;
using Domain;
using Domain.Database;
using Domain.PriceReductionTables;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers(options =>
        {
            options.Filters.Add(new ProducesAttribute(MediaTypeNames.Application.Json));
            options.Filters.Add(new ConsumesAttribute(MediaTypeNames.Application.Json));
        });
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDbContext<LiftPassDbContext>(options =>
        {
            options.UseMySql(builder.Configuration.GetConnectionString("Default"), new MariaDbServerVersion("10.4"));
        });
        builder.Services.AddScoped<LiftPassPriceCalculator>();
        builder.Services.AddScoped<IReductionService, ReductionService>();
        builder.Services.AddScoped<HolidayRepository>();
        builder.Services.AddScoped<BasePriceRepository>();
        builder.Services.AddScoped<ISpecialReductionDaysService, SpecialReductionDaysService>();
        builder.Services.AddScoped<IPriceReductionTable, NightPriceReductionTable>();
        builder.Services.AddScoped<IPriceReductionTable, OneJourPriceReductionTable>();

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