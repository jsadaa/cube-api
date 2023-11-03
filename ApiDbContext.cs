using ApiCube.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCube;

public class ApiDbContext : DbContext
{
    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
    {
    }
    
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
    
    public DbSet<StockModel> Stocks { get; set; }
    
    public override int SaveChanges()
    {
        UpdateTimestamps();
        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
    {
        UpdateTimestamps();
        return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private void UpdateTimestamps()
    {
        var entities = ChangeTracker.Entries().Where(
            x => (x.State == EntityState.Added || x.State == EntityState.Modified));

        foreach (var entity in entities)
        {
            var type = entity.Entity.GetType();
            var dateCreationProperty = type.GetProperty("DateCreation");
            var dateModificationProperty = type.GetProperty("DateModification");

            if (entity.State == EntityState.Added && dateCreationProperty != null)
            {
                dateCreationProperty.SetValue(entity.Entity, DateTime.UtcNow);
            }

            if (dateModificationProperty != null)
            {
                dateModificationProperty.SetValue(entity.Entity, DateTime.UtcNow);
            }
        }
    }
}