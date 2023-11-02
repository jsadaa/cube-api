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
}