using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using R6.Core.Enums;

#nullable disable

namespace R6.Infra.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:armor_type", "light,medium,heavy")
                .Annotation("Npgsql:Enum:dificult_type", "easy,medium,hard")
                .Annotation("Npgsql:Enum:speed_type", "slow,medium,fast");

            migrationBuilder.CreateTable(
                name: "Operator",
                columns: table => new
                {
                    Id = table.Column<long>(type: "BIGINT", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "VARCHAR(80)", maxLength: 80, nullable: false),
                    Dificult = table.Column<DificultType>(type: "dificult_type", nullable: false),
                    Speed = table.Column<SpeedType>(type: "speed_type", nullable: false),
                    Armor = table.Column<ArmorType>(type: "armor_type", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operator", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Operator");
        }
    }
}
