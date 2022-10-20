using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace goodreads.Migrations
{
    public partial class inputtablesupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CardId",
                table: "InputValue",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CardId",
                table: "InputValue");
        }
    }
}
