using Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace Infrastructure.Repository
{
    public class AppDbContext : DbContext
    {
        private readonly string _connectionString;

        // Pegando do  appsettings.json
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

        // Construtor para permitir o uso de DbContextOptions (necessário para testes com banco de dados em memória)
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Especificando o banco de dados
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured && !string.IsNullOrEmpty(_connectionString))
                optionsBuilder.UseSqlServer(_connectionString);
        }

        // Mapear as entidades
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }

        public DbSet<ContatoModel> Contato { get; set; }
    }
}
