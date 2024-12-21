using Microsoft.EntityFrameworkCore;
using WebApiBib.Interfaces;
using WebApiBib.Service;
using WebBooksZhanr.DBContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<BooksZhanrDB>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("TestDbString")), ServiceLifetime.Scoped);

builder.Services.AddScoped<IBook, BookService>();
builder.Services.AddScoped<IZhanrServices, ZhanrService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowGeneralAPI", policy =>
    {
        policy.WithOrigins("http://localhost:5283") // Порт вашего API Gateway
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowWebReaders", policy =>
    {
        policy.WithOrigins("http://localhost:1111") // Порт вашего API Gateway
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors("AllowGeneralAPI");
app.UseCors("AllowWebReaders");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
