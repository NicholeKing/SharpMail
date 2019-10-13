using Microsoft.EntityFrameworkCore.Migrations;

namespace csharp_email.Migrations
{
    public partial class secondMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "PermDeleted",
                table: "Emails",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PermDeleted",
                table: "Emails");
        }
    }
}
