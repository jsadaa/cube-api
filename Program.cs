using ApiCube;
using ApiCube.Domain.Factories;
using ApiCube.Repositories;
using ApiCube.Repositories.Interfaces;
using ApiCube.Services.ProduitService;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Configure Entity Framework Core to use MySQL
builder.Services.AddDbContext<ApiDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

builder.Services.AddScoped<IFamilleProduitRepository , FamilleProduitRepository>();
builder.Services.AddScoped<IPromotionRepository , PromotionRepository>();
builder.Services.AddScoped<IProduitRepository , ProduitRepository>();
builder.Services.AddScoped<IProduitService , ProduitService>();
builder.Services.AddScoped<ProduitFactory>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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