using CursoEF1.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CursoEF1.Data.Configurations
{
    public class PedidoItemConfiguration : IEntityTypeConfiguration<PedidoItem>
    {
        public void Configure(EntityTypeBuilder<PedidoItem> builder)
        {
            builder.ToTable("PedidoItems");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Quantidade).HasDefaultValue(1).IsRequired();
            builder.Property(p => p.Valor).IsRequired();
            builder.Property(p => p.Desconto).IsRequired();
        }
    }
}
