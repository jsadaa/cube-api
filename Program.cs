using System.Reflection;
using System.Text;
using ApiCube;
using ApiCube.Application.Services.Auth;
using ApiCube.Application.Services.Client;
using ApiCube.Application.Services.CommandeClient;
using ApiCube.Application.Services.CommandeFournisseur;
using ApiCube.Application.Services.Employe;
using ApiCube.Application.Services.FactureClient;
using ApiCube.Application.Services.FamilleProduit;
using ApiCube.Application.Services.Fournisseur;
using ApiCube.Application.Services.Produit;
using ApiCube.Application.Services.Promotion;
using ApiCube.Application.Services.Stock;
using ApiCube.Configurations.AutoMapper;
using ApiCube.Domain.Enums.Commande;
using ApiCube.Domain.Enums.Facture;
using ApiCube.Domain.Enums.Stock;
using ApiCube.Domain.Mappers.Client;
using ApiCube.Domain.Mappers.CommandeClient;
using ApiCube.Domain.Mappers.CommandeFournisseur;
using ApiCube.Domain.Mappers.Employe;
using ApiCube.Domain.Mappers.FactureClient;
using ApiCube.Domain.Mappers.FamilleProduit;
using ApiCube.Domain.Mappers.Fournisseur;
using ApiCube.Domain.Mappers.LigneCommandeClient;
using ApiCube.Domain.Mappers.LigneCommandeFournisseur;
using ApiCube.Domain.Mappers.LignePanierClient;
using ApiCube.Domain.Mappers.PanierClient;
using ApiCube.Domain.Mappers.Produit;
using ApiCube.Domain.Mappers.Promotion;
using ApiCube.Domain.Mappers.Stock;
using ApiCube.Domain.Mappers.TransactionStock;
using ApiCube.Domain.Services;
using ApiCube.Persistence.Models;
using ApiCube.Persistence.Repositories.Client;
using ApiCube.Persistence.Repositories.CommandeClient;
using ApiCube.Persistence.Repositories.CommandeFournisseur;
using ApiCube.Persistence.Repositories.Employe;
using ApiCube.Persistence.Repositories.FactureClient;
using ApiCube.Persistence.Repositories.FamilleProduit;
using ApiCube.Persistence.Repositories.Fournisseur;
using ApiCube.Persistence.Repositories.PanierClient;
using ApiCube.Persistence.Repositories.Produit;
using ApiCube.Persistence.Repositories.Promotion;
using ApiCube.Persistence.Repositories.Stock;
using ApiCube.Persistence.Repositories.TransactionStock;
using ApiCube.Persistence.Seeders;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApiDocument();

// Configure Entity Framework Core to use MySQL
builder.Services.AddDbContext<ApiDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)).EnableSensitiveDataLogging();
});

// Configure Identity
builder.Services.AddIdentity<ApplicationUserModel, IdentityRole>(options =>
    {
        options.User.RequireUniqueEmail = true;
        options.Password.RequiredLength = 8;
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireNonAlphanumeric = true;
    })
    .AddEntityFrameworkStores<ApiDbContext>()
    .AddDefaultTokenProviders();

// Configure JWT
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? string.Empty)),
            ClockSkew = TimeSpan.Zero // Réduire la marge de tolérance pour l'expiration des tokens
        };
    });

// Configure AutoMapper
// Note: Apart DTO to Data Model mapping and vice versa,
// AutoMapper don't manage DTO or data Models to Domain Entity mapping, only Domain Entity to DTO or Data Model
// There's specific mappers for that (see below)
builder.Services.AddAutoMapper(
    typeof(FamilleProduitMapperConfig),
    typeof(FournisseurMapperConfig),
    typeof(ProduitMapperConfig),
    typeof(StockMapperConfig),
    typeof(TransactionStockMapperConfig),
    typeof(AdresseMapperConfig),
    typeof(PromotionMapperConfig),
    typeof(ClientMapperConfig),
    typeof(EmployeMapperConfig),
    typeof(CommandeFournisseurMapperConfig),
    typeof(LigneCommandeFournisseurMapperConfig),
    typeof(PanierClientMapperConfig),
    typeof(LignePanierClientMapperConfig),
    typeof(CommandeClientMapperConfig),
    typeof(LigneCommandeClientMapperConfig)
);

// Configure Mappers
builder.Services.AddScoped<IProduitMapper, ProduitMapper>();
builder.Services.AddScoped<ITransactionStockMapper, TransactionStockMapper>();
builder.Services.AddScoped<IFournisseurMapper, FournisseurMapper>();
builder.Services.AddScoped<IStockMapper, StockMapper>();
builder.Services.AddScoped<IFamilleProduitMapper, FamilleProduitMapper>();
builder.Services.AddScoped<IPromotionMapper, PromotionMapper>();
builder.Services.AddScoped<IClientMapper, ClientMapper>();
builder.Services.AddScoped<IEmployeMapper, EmployeMapper>();
builder.Services.AddScoped<ICommandeFournisseurMapper, CommandeFournisseurMapper>();
builder.Services.AddScoped<ILigneCommandeFournisseurMapper, LigneCommandeFournisseurMapper>();
builder.Services.AddScoped<IPanierClientMapper, PanierClientMapper>();
builder.Services.AddScoped<ILignePanierClientMapper, LignePanierClientMapper>();
builder.Services.AddScoped<ICommandeClientMapper, CommandeClientMapper>();
builder.Services.AddScoped<ILigneCommandeClientMapper, LigneCommandeClientMapper>();
builder.Services.AddScoped<IFactureClientMapper, FactureClientMapper>();

// Configure enums mappers
builder.Services.AddScoped<TypeTransactionStockMapper>();
builder.Services.AddScoped<StatutStockMapper>();
builder.Services.AddScoped<StatutCommandeMapper>();
builder.Services.AddScoped<StatutFactureMapper>();

// Configure repositories
builder.Services.AddScoped<IFamilleProduitRepository, FamilleProduitRepository>();
builder.Services.AddScoped<IProduitRepository, ProduitRepository>();
builder.Services.AddScoped<ITransactionStockRepository, TransactionStockRepository>();
builder.Services.AddScoped<IFournisseurRepository, FournisseurRepository>();
builder.Services.AddScoped<IStockRepository, StockRepository>();
builder.Services.AddScoped<IPromotionRepository, PromotionRepository>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IEmployeRepository, EmployeRepository>();
builder.Services.AddScoped<ICommandeFournisseurRepository, CommandeFournisseurRepository>();
builder.Services.AddScoped<IPanierClientRepository, PanierClientRepository>();
builder.Services.AddScoped<ICommandeClientRepository, CommandeClientRepository>();
builder.Services.AddScoped<IFactureClientRepository, FactureClientRepository>();

// Configure application services
builder.Services.AddScoped<IProduitService, ProduitService>();
builder.Services.AddScoped<IFamilleProduitService, FamilleProduitService>();
builder.Services.AddScoped<IFournisseurService, FournisseurService>();
builder.Services.AddScoped<IStockService, StockService>();
builder.Services.AddScoped<IPromotionService, PromotionService>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IEmployeService, EmployeService>();
builder.Services.AddScoped<ICommandeFournisseurService, CommandeFournisseurService>();
builder.Services.AddScoped<ICommandeClientService, CommandeClientService>();
builder.Services.AddScoped<IFactureClientService, FactureClientService>();

// Configure domain services
builder.Services.AddScoped<GestionnaireDeStock>();
builder.Services.AddScoped<PreparateurDeCommandeFournisseur>();
builder.Services.AddScoped<PreparateurDeCommandeClient>();
builder.Services.AddScoped<GestionnaireDeFacturation>();

// Configure Production Seeder
builder.Services.AddScoped<RoleSeeder>();
builder.Services.AddScoped<EmployeSeeder>();
builder.Services.AddScoped<ClientSeeder>();

// Configure Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    c =>
    {
        // Configure Swagger to use the xml documentation file generated by the compiler to document the API
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiCube", Version = "v1" });
        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    }
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Fixtures (seeders)
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<ApiDbContext>();

        var familles = FamilleProduitSeeder.SeedFamilleProduits(context);
        var fournisseurs = FournisseurSeeder.SeedFournisseurs(context);
        var produits = ProduitSeeder.SeedProduits(context, familles, fournisseurs);
        StockSeeder.SeedStocksAndTransactions(context, produits);
        PromotionSeeder.SeedPromotions(context);
    }

    app.UseDeveloperExceptionPage();

    // Enable middleware to serve generated Swagger as a JSON endpoint.
    app.UseSwagger(
        c => c.SerializeAsV2 = true
    );

    // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiCube v1"));

    // Enable middleware to serve ReDoc documentation
    app.UseReDoc(
        options => { options.Path = "/redoc"; }
    );
}

// Create roles
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        await RoleSeeder.CreateRoles(services);
        await EmployeSeeder.CreateEmployes(services);
        await ClientSeeder.CreateClients(services);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Une erreur s'est produite lors de la création des rôles.");
    }
}

app.UseSwagger(c =>
{
    c.SerializeAsV2 = true;
    c.RouteTemplate = "swagger/{documentName}/swagger.json";
});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();