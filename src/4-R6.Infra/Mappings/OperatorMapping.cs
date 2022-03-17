using R6.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace R6.Infra.Mappings
{
    public class OperatorMap : IEntityTypeConfiguration<Operator>
    {
        public void Configure(EntityTypeBuilder<Operator> builder)
        {
            builder.ToTable("Operator");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .UseMySqlIdentityColumn()
                .HasColumnType("BIGINT");

            builder.Property( x => x.Name)
                .IsRequired()
                .HasMaxLength(80)
                .HasColumnName("Name")
                .HasColumnType("VARCHAR(80)");
            
            builder.Property(x => x.Armor)
                .IsRequired()
                .HasColumnName("Armor")
                .HasConversion<string>();

            builder.Property(x => x.Dificult)
                .IsRequired()
                .HasMaxLength(180)
                .HasColumnName("Dificult")
                .HasColumnType("int");

        }
    }
}