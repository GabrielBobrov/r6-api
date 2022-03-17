using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace R6.Infra.Migrations
{
    public partial class InsertOperatorDoc : Migration
    {
         protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO ""Operator"" VALUES (1,'Doc', 'hard', 'slow', 'medium');");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DELETE FROM ""Operator""
                WHERE ""Name""= Doc");
        }
    }
}