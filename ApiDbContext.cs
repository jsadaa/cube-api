using ApiCube.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCube;

public class ApiDbContext : DbContext
{
    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
    {
    }
    
    public DbSet<StudentModel> Students { get; set; }
    
    public DbSet<ProduitModel> Produits { get; set; }
    
    public DbSet<PromotionModel> Promotions { get; set; }
    
    public DbSet<FamilleProduitModel> FamillesProduits { get; set; }
    
    public DbSet<CommandeClientModel> CommandesClients { get; set; }
    
    public DbSet<FactureClientModel> FacturesClients { get; set; }
    
    public DbSet<LigneCommandeClientModel> LignesCommandesClients { get; set; }
    
    public DbSet<CommandeFournisseurModel> CommandesFournisseurs { get; set; }
    
    public DbSet<LigneCommandeFournisseurModel> LignesCommandesFournisseurs { get; set; }
    
    public DbSet<FactureFournisseurModel> FacturesFournisseurs { get; set; }
    
    public DbSet<EmployeModel> Employes { get; set; }
    
    public DbSet<RoleModel> Roles { get; set; }
    
    public DbSet<ClientModel> Clients { get; set; }
    
    public DbSet<FournisseurModel> Fournisseurs { get; set; }
    
    public DbSet<TransactionStockModel> TransactionsStock { get; set; }
}