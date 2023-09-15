using CarSeller.Data;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Connect MSSQL with DbContext
builder.Services.AddDbContext<CarDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

//Configure RabbiMQ
builder.Services.AddMassTransit(x =>
{
    //Add Outbox
    x.AddEntityFrameworkOutbox<CarDbContext>(o => {
        o.QueryDelay= TimeSpan.FromSeconds(10);

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
