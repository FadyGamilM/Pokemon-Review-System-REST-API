using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pokemonAPI.Migrations
{
    public partial class addratingcolumntoreviewstable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Rating",
                table: "Reviews",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Reviews");
        }
    }
}
