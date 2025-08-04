using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api_event_panel.Migrations
{
    /// <inheritdoc />
    public partial class addServicePackage2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PanelUsers_ServicePackages_ServicePackageid",
                table: "PanelUsers");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "ServicePackages",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ServicePackageid",
                table: "PanelUsers",
                newName: "ServicePackageId");

            migrationBuilder.RenameIndex(
                name: "IX_PanelUsers_ServicePackageid",
                table: "PanelUsers",
                newName: "IX_PanelUsers_ServicePackageId");

            migrationBuilder.AddForeignKey(
                name: "FK_PanelUsers_ServicePackages_ServicePackageId",
                table: "PanelUsers",
                column: "ServicePackageId",
                principalTable: "ServicePackages",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PanelUsers_ServicePackages_ServicePackageId",
                table: "PanelUsers");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ServicePackages",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "ServicePackageId",
                table: "PanelUsers",
                newName: "ServicePackageid");

            migrationBuilder.RenameIndex(
                name: "IX_PanelUsers_ServicePackageId",
                table: "PanelUsers",
                newName: "IX_PanelUsers_ServicePackageid");

            migrationBuilder.AddForeignKey(
                name: "FK_PanelUsers_ServicePackages_ServicePackageid",
                table: "PanelUsers",
                column: "ServicePackageid",
                principalTable: "ServicePackages",
                principalColumn: "id");
        }
    }
}
