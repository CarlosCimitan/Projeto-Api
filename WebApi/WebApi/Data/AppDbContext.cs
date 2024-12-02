using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
        }

        // Criando as Tabelas com base em meus modals 
        public DbSet<AutorModel> Autor { get; set; }
        public DbSet<LivroModel> Livro { get; set; }
    }
}
