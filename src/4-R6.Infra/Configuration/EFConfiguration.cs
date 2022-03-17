using Microsoft.EntityFrameworkCore;
using R6.Core.Enums;
using R6.Infra.Mappings;

namespace R6.Infra.Configuration
{
    public static class EFConfigurations
    {
        public static ModelBuilder AddMappings(this ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OperatorMapping());

            return modelBuilder;
        }

        public static ModelBuilder AddPostgresEnums(this ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresEnum<DificultType>();
            modelBuilder.HasPostgresEnum<SpeedType>();
            modelBuilder.HasPostgresEnum<ArmorType>();

            return modelBuilder;
        }
    }
}
