using ApiCube;
using ApiCube.Domain.Entities;
using ApiCube.Domain.Factories;
using ApiCube.Repositories;
using ApiCube.Repositories.FamilleProduit;
using ApiCube.Repositories.Fournisseur;
using ApiCube.Repositories.Produit;
using ApiCube.Repositories.Promotion;
using ApiCube.Repositories.TransactionStock;
using ApiCube.Services.FamilleProduit;
using ApiCube.Services.Fournisseur;
using ApiCube.Services.Produit;
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
builder.Services.AddScoped<IFamilleProduitService , FamilleProduitService>();
builder.Services.AddScoped<ITransactionStockRepository , TransactionStockRepository>();
builder.Services.AddScoped<ProduitFactory>();
builder.Services.AddScoped<TransactionStockFactory>();
builder.Services.AddScoped<FamilleProduitFactory>();
builder.Services.AddScoped<FournisseurFactory>();
builder.Services.AddScoped<IFournisseurRepository , FournisseurRepository>();
builder.Services.AddScoped<IFournisseurService , FournisseurService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( c =>
{
    c.SwaggerDoc("v1", new() { Title = "ApiCube", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiCube v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();