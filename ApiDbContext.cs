using ApiCube.Persistence.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ApiCube;

public class ApiDbContext : IdentityDbContext<ClientModel>
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

    //public DbSet<RoleModel> Roles { get; set; }

    public DbSet<ClientModel> Clients { get; set; }

    public DbSet<FournisseurModel> Fournisseurs { get; set; }

    public DbSet<TransactionStockModel> TransactionsStock { get; set; }

    public DbSet<StockModel> Stocks { get; set; }

    public override int SaveChanges()
    {
        UpdateTimestamps();
        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        UpdateTimestamps();
        return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private void UpdateTimestamps()
    {
        var entities = ChangeTracker.Entries().Where(
            x => x.State is EntityState.Added or EntityState.Modified);

        foreach (var entity in entities)
        {
            var type = entity.Entity.GetType();
            var dateModificationProperty = type.GetProperty("DateModification");

            if (dateModificationProperty != null)
            {
                dateModificationProperty.SetValue(entity.Entity, DateTime.Now);
            }
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ClientModel>()
            .HasIndex(c => c.Email)
            .IsUnique();

        modelBuilder.Entity<EmployeModel>()
            .HasIndex(e => new { e.Nom, e.Prenom, e.Email })
            .IsUnique();

        modelBuilder.Entity<FournisseurModel>()
            .HasIndex(f => new { f.Nom, f.Email })
            .IsUnique();

        modelBuilder.Entity<ProduitModel>()
            .HasIndex(p => new { p.Nom, p.Annee })
            .IsUnique();

        modelBuilder.Entity<FamilleProduitModel>()
            .HasIndex(f => f.Nom)
            .IsUnique();

        modelBuilder.Entity<RoleModel>()
            .HasIndex(r => r.Nom)
            .IsUnique();

        modelBuilder.Entity<StockModel>()
            .HasIndex(s => s.ProduitId)
            .IsUnique();
    }
}