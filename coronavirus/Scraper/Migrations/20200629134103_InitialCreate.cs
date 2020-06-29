using Microsoft.EntityFrameworkCore.Migrations;

namespace Scraper.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: true),
                    Code1 = table.Column<string>(nullable: true),
                    Code2 = table.Column<string>(nullable: true),
                    Region = table.Column<string>(nullable: true),
                    SubRegion = table.Column<string>(nullable: true),
                    Population = table.Column<int>(nullable: false),
                    Capital = table.Column<string>(nullable: true),
                    Latitude = table.Column<double>(nullable: true),
                    Longitude = table.Column<double>(nullable: true),
                    FlagUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
