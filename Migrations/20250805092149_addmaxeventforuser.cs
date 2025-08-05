using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api_event_panel.Migrations
{
    /// <inheritdoc />
    public partial class addmaxeventforuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "maxEvents",
                table: "PanelUsers",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "maxEvents",
                table: "PanelUsers");
        }
    }
}
