using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoMeal.api.Migrations
{
    /// <inheritdoc />
    public partial class DeleteEmailBusiness : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Businesses");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Businesses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
