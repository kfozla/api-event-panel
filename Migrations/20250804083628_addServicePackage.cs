using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api_event_panel.Migrations
{
    /// <inheritdoc />
    public partial class addServicePackage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ServicePackageAddedOn",
                table: "PanelUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ServicePackageExpiration",
                table: "PanelUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ServicePackageid",
                table: "PanelUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ServicePackages",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    activeFor = table.Column<DateTime>(type: "datetime2", nullable: false),
                    maxEvents = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicePackages", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PanelUsers_ServicePackageid",
                table: "PanelUsers",
                column: "ServicePackageid");

            migrationBuilder.AddForeignKey(
                name: "FK_PanelUsers_ServicePackages_ServicePackageid",
                table: "PanelUsers",
                column: "ServicePackageid",
                principalTable: "ServicePackages",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PanelUsers_ServicePackages_ServicePackageid",
                table: "PanelUsers");

            migrationBuilder.DropTable(
                name: "ServicePackages");

            migrationBuilder.DropIndex(
                name: "IX_PanelUsers_ServicePackageid",
                table: "PanelUsers");

            migrationBuilder.DropColumn(
                name: "ServicePackageAddedOn",
                table: "PanelUsers");

            migrationBuilder.DropColumn(
                name: "ServicePackageExpiration",
                table: "PanelUsers");

            migrationBuilder.DropColumn(
                name: "ServicePackageid",
                table: "PanelUsers");
        }
    }
}
