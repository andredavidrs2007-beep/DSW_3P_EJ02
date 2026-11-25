using Microsoft.EntityFrameworkCore;
using DSW_3P_EJ02.Models;

namespace DSW_3P_EJ02.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Personaje> Personajes => Set<Personaje>();
}