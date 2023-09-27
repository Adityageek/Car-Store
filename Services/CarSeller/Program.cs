using BenchmarkDotNet.Running;
using CarSeller.Controllers;
using CarSeller.Middlewares;
using CarStore.Infrastructure;
using CarStore.Services.Interface;
using CarStore.Services.Services;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDIServices(builder.Configuration);
builder.Services.AddTransient<ICarService, CarService>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Register ILogger service
builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddSeq(builder.Configuration.GetSection("Seq"));
});

//Configure RabbitMQ
builder.Services.AddMassTransit(x =>
{
    //Add Outbox
    x.AddEntityFrameworkOutbox<DbContextClass>(o =>
    {
        o.QueryDelay = TimeSpan.FromSeconds(10);

        o.UseSqlServer();
        o.UseBusOutbox();
    });

    x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("car", false));

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMq:Host"], "/", host =>
        {
            host.Username(builder.Configuration.GetValue("RabbitMq:Username", "guest"));
            host.Password(builder.Configuration.GetValue("RabbitMq:Password", "guest"));
        });
        cfg.ConfigureEndpoints(context);
    });

});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers(); // Map your regular API controllers

    // Add a custom endpoint for triggering benchmarks
    endpoints.MapGet("/runbenchmarks", async context =>
    {
        // You can run the benchmarks here
        var summary = BenchmarkRunner.Run<CarController>();

        await context.Response.WriteAsync("Benchmarks completed. Check console for results.");
    });
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseRequestResponseLogging();
app.Run();
