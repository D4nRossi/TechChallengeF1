using Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repository
{
    public class AppDbContext : DbContext
    {

        private readonly string _connectionString;

        //ctor pra migration nn dar pau, vai pegar a connection string direto do appsettings 
        public AppDbContext()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            _connectionString = configuration.GetConnectionString("DB_CONNECTION");
        }

        public AppDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        //Especificando o bando de dados
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(_connectionString);
        }

        //Mapear as entidades
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Entidades mapeadas no Configurations
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }

        public DbSet<ContatoModel> Contato { get; set; }
        public DbSet<MunicipioModel> Municipio { get; set; }

    }
}
