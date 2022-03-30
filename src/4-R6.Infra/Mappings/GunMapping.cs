using R6.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace R6.Infra.Mappings
{
    public class GunMapping : IEntityTypeConfiguration<Gun>
    {
        public void Configure(EntityTypeBuilder<Gun> builder)
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

            builder.HasOne(c => c.Operator)
                .WithOne(o => o.Gun)
                .HasForeignKey<Gun>(f => f.operatorId);

            builder.Property(x => x.PrimaryGunType)
                .HasColumnName("PrimaryGunType")
                .IsRequired(false);

            builder.Property(x => x.SecondaryGunType)
                .HasColumnName("SecondaryGunType")
                .IsRequired(false);

        }
    }
}