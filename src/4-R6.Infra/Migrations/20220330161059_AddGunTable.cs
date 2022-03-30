using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using R6.Core.Enums;

#nullable disable

namespace R6.Infra.Migrations
{
    public partial class AddGunTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:armor_type", "light,medium,heavy")
                .Annotation("Npgsql:Enum:dificult_type", "easy,medium,hard")
                .Annotation("Npgsql:Enum:primary_gun_type", "smg,ar,lmg")
                .Annotation("Npgsql:Enum:secondary_gun_type", "hand_gun,machine_pistol,shotgun")
                .Annotation("Npgsql:Enum:speed_type", "slow,medium,fast")
                .OldAnnotation("Npgsql:Enum:armor_type", "light,medium,heavy")
                .OldAnnotation("Npgsql:Enum:dificult_type", "easy,medium,hard")
                .OldAnnotation("Npgsql:Enum:speed_type", "slow,medium,fast");

            migrationBuilder.CreateTable(
                name: "Gun",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    PrimaryGunType = table.Column<PrimaryGunType>(type: "primary_gun_type", nullable: true),
                    SecondaryGunType = table.Column<SecondaryGunType>(type: "secondary_gun_type", nullable: true),
                    operatorId = table.Column<long>(type: "BIGINT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gun", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gun_Operator_operatorId",
                        column: x => x.operatorId,
                        principalTable: "Operator",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Gun_operatorId",
                table: "Gun",
                column: "operatorId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Gun");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:armor_type", "light,medium,heavy")
                .Annotation("Npgsql:Enum:dificult_type", "easy,medium,hard")
                .Annotation("Npgsql:Enum:speed_type", "slow,medium,fast")
                .OldAnnotation("Npgsql:Enum:armor_type", "light,medium,heavy")
                .OldAnnotation("Npgsql:Enum:dificult_type", "easy,medium,hard")
                .OldAnnotation("Npgsql:Enum:primary_gun_type", "smg,ar,lmg")
                .OldAnnotation("Npgsql:Enum:secondary_gun_type", "hand_gun,machine_pistol,shotgun")
                .OldAnnotation("Npgsql:Enum:speed_type", "slow,medium,fast");
        }
    }
}
