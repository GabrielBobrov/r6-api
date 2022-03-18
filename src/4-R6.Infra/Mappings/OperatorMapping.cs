using R6.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace R6.Infra.Mappings
{
    public class OperatorMapping : IEntityTypeConfiguration<Operator>
    {
        public void Configure(EntityTypeBuilder<Operator> builder)
        {
            builder.ToTable("Operator");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnType("BIGINT");

            builder.Property( x => x.Name)
                .IsRequired()
                .HasMaxLength(80)
                .HasColumnName("Name")
                .HasColumnType("VARCHAR(80)");
            
            builder.Property(x => x.Armor)
                .IsRequired()
                .HasColumnName("Armor");

            builder.Property(x => x.Dificult)
                .IsRequired()
                .HasColumnName("Dificult");

            builder.Property(x => x.Speed)
                .IsRequired()
                .HasColumnName("Speed");
        }
    }
}