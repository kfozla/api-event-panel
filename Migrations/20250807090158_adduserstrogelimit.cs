using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api_event_panel.Migrations
{
    /// <inheritdoc />
    public partial class adduserstrogelimit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "storageLimit",
                table: "PanelUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "usingStorage",
                table: "PanelUsers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "storageLimit",
                table: "PanelUsers");

            migrationBuilder.DropColumn(
                name: "usingStorage",
                table: "PanelUsers");
        }
    }
}
