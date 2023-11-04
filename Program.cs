using ApiCube;
using ApiCube.Application.Services.FamilleProduit;
using ApiCube.Application.Services.Fournisseur;
using ApiCube.Application.Services.Produit;
using ApiCube.Application.Services.Stock;
using ApiCube.Configurations;
using ApiCube.Domain.Enums.Stock;
using ApiCube.Domain.Factories;
using ApiCube.Persistence.Repositories.FamilleProduit;
using ApiCube.Persistence.Repositories.Fournisseur;
using ApiCube.Persistence.Repositories.Produit;
using ApiCube.Persistence.Repositories.Promotion;
using ApiCube.Persistence.Repositories.Stock;
using ApiCube.Persistence.Repositories.TransactionStock;
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

// Configure AutoMapper
builder.Services.AddAutoMapper(typeof(FamilleProduitMapperConfig), typeof(FournisseurMapperConfig));

// Configure repositories
builder.Services.AddScoped<IFamilleProduitRepository , FamilleProduitRepository>();
builder.Services.AddScoped<IPromotionRepository , PromotionRepository>();
builder.Services.AddScoped<IProduitRepository , ProduitRepository>();
builder.Services.AddScoped<ITransactionStockRepository , TransactionStockRepository>();
builder.Services.AddScoped<IFournisseurRepository , FournisseurRepository>();
builder.Services.AddScoped<IStockRepository , StockRepository>();

// Configure services
builder.Services.AddScoped<IProduitService , ProduitService>();
builder.Services.AddScoped<IFamilleProduitService , FamilleProduitService>();
builder.Services.AddScoped<IFournisseurService , FournisseurService>();
builder.Services.AddScoped<IStockService , StockService>();

// Configure factories
builder.Services.AddScoped<ProduitFactory>();
builder.Services.AddScoped<TransactionStockFactory>();
builder.Services.AddScoped<FournisseurFactory>();
builder.Services.AddScoped<StockFactory>();

// Configure enums mappers
builder.Services.AddScoped<TypeTransactionStockMapper>();
builder.Services.AddScoped<StatutStockMapper>();

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