using Microsoft.EntityFrameworkCore.Migrations;

namespace ClubAtleticoOrt.Migrations
{
    public partial class ClubAtleticoOrtContextClubDatabaseContext2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "id_cancha",
                table: "Reservas",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "id_cancha",
                table: "Reservas");
        }
    }
}
