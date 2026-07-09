using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoMeal.api.Migrations
{
    /// <inheritdoc />
    public partial class FixBusinessRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Businesses_BusinessTypes_BusinessTypeId1",
                table: "Businesses");

            migrationBuilder.DropIndex(
                name: "IX_Businesses_BusinessTypeId1",
                table: "Businesses");

            migrationBuilder.DropColumn(
                name: "BusinessTypeId1",
                table: "Businesses");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "BusinessTypeId1",
                table: "Businesses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Businesses_BusinessTypeId1",
                table: "Businesses",
                column: "BusinessTypeId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Businesses_BusinessTypes_BusinessTypeId1",
                table: "Businesses",
                column: "BusinessTypeId1",
                principalTable: "BusinessTypes",
                principalColumn: "BusinessTypeId");
        }
    }
}
