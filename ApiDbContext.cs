using ApiCube.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCube;

public class ApiDbContext : DbContext
{
    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
    {
    }
    
    public DbSet<StudentModel> Students { get; set; }
}