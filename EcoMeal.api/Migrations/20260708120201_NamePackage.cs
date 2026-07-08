using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoMeal.api.Migrations
{
    /// <inheritdoc />
    public partial class NamePackage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Packages_Businesses_BusinessTypeId",
                table: "Packages");

            migrationBuilder.RenameColumn(
                name: "BusinessTypeId",
                table: "Packages",
                newName: "BusinessId");

            migrationBuilder.RenameIndex(
                name: "IX_Packages_BusinessTypeId",
                table: "Packages",
                newName: "IX_Packages_BusinessId");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Packages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Packages_Businesses_BusinessId",
                table: "Packages",
                column: "BusinessId",
                principalTable: "Businesses",
                principalColumn: "BusinessId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Packages_Businesses_BusinessId",
                table: "Packages");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Packages");

            migrationBuilder.RenameColumn(
                name: "BusinessId",
                table: "Packages",
                newName: "BusinessTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Packages_BusinessId",
                table: "Packages",
                newName: "IX_Packages_BusinessTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Packages_Businesses_BusinessTypeId",
                table: "Packages",
                column: "BusinessTypeId",
                principalTable: "Businesses",
                principalColumn: "BusinessId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
