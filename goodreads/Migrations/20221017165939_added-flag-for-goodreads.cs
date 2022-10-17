using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace goodreads.Migrations
{
    public partial class addedflagforgoodreads : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAddedToGoodreads",
                table: "Book",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAddedToGoodreads",
                table: "Book");
        }
    }
}
