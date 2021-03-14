using CursoEF1.Data.Configurations;
using CursoEF1.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace CursoEF1.Data
{
    public class ApplicationContext : DbContext
    {
        private static readonly ILoggerFactory _logger = LoggerFactory.Create(p => p.AddConsole());

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<PedidoItem> PedidoItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLoggerFactory(_logger)
                .EnableSensitiveDataLogging()   
                .UseSqlServer("Server=localhost;Database=CursoEFCore;Trusted_Connection=True;",
                p => p.EnableRetryOnFailure(
                    maxRetryCount: 2, 
                    maxRetryDelay: TimeSpan.FromSeconds(5), 
                    errorNumbersToAdd: null).MigrationsHistoryTable("curso_ef_core"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
            MapearPropriedadesEsquecidas(modelBuilder);
        }

        private void MapearPropriedadesEsquecidas(ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                var properties = entity.GetProperties().Where(p => p.ClrType == typeof(string));

                foreach (var property in properties)
                {
                    if (string.IsNullOrEmpty(property.GetColumnType()) 
                        && !property.GetMaxLength().HasValue)
                    {
                        //property.SetMaxLength(100);
                        property.SetColumnType("VARCHAR(100)");
                    }
                }
            }
        }
    }
}
