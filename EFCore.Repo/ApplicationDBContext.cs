using EFCore.Dominio;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Repo
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Endereco>()
                .HasOne<Fornecedor>(s => s.Fornecedor)
                .WithMany(g => g.Enderecos)
                .HasForeignKey(s => s.CurrentFornecedorId);
        }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }        
    }
}
