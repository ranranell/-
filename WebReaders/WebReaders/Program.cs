using Microsoft.EntityFrameworkCore;
using WebReaders.DB;
using WebReaders.Interfaces;
using WebReaders.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DBReaders>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("TestDbString")), ServiceLifetime.Scoped);
builder.Services.AddScoped<IReaderServices, ReaderService>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowGeneralAPI", policy =>
    {
        policy.WithOrigins("http://localhost:5283") // Порт вашего API Gateway
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
var app = builder.Build();

app.UseCors("AllowGeneralAPI");


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
