using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketTix.DataAccess.Migrations
{
    public partial class extendTicketToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Destination",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Origin",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Destination",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "Origin",
                table: "Tickets");
        }
    }
}
