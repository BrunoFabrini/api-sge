using api_sge.Entidades;
using Microsoft.EntityFrameworkCore;

namespace api_sge.Database
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Endereco> Enderecos { get; set; }

        public DbSet<Entrega> Entregas { get; set; }

        public DbSet<Localizacao> Localizacoes { get; set; }

        public DbSet<Mercadoria> Mercadorias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Endereco>().HasData(
                new Endereco() {EnderecoCodigo = 1, Cidade = "Belo Horizonte", Estado = "MG", Logradouro = "Rua XPTO", LogradouroNumero = "1000" }
            );

            Usuario admin = new Usuario()
            {
                UsuarioCodigo = 1,
                Admin = true,
                Login = "admin"
            };

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                admin.SenhaSalt = hmac.Key;
                admin.SenhaHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes("123"));
            }

            modelBuilder.Entity<Usuario>().HasData(admin);
        }
    }
}