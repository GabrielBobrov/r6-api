using Npgsql.TypeMapping;
using R6.Core.Enums;

namespace R6.Infra.Configuration
{
    public static class NpgsqlConfiguration
    {
        public static void AddGlobalTypeMappers(this INpgsqlTypeMapper mapper)
        {
            mapper.MapEnum<DificultType>();
            mapper.MapEnum<SpeedType>();
            mapper.MapEnum<ArmorType>();
        }
    }
}
