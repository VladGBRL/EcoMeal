using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoMeal.api.Migrations
{
    /// <inheritdoc />
    public partial class RemovePackageBusinessId1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Packages_Businesses_BusinessId1",
                table: "Packages");

            migrationBuilder.DropIndex(
                name: "IX_Packages_BusinessId1",
                table: "Packages");

            migrationBuilder.DropColumn(
                name: "BusinessId1",
                table: "Packages");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BusinessId1",
                table: "Packages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Packages_BusinessId1",
                table: "Packages",
                column: "BusinessId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Packages_Businesses_BusinessId1",
                table: "Packages",
                column: "BusinessId1",
                principalTable: "Businesses",
                principalColumn: "BusinessId");
        }
    }
}
