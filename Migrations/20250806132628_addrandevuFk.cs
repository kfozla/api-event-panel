using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api_event_panel.Migrations
{
    /// <inheritdoc />
    public partial class addrandevuFk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Randevu_panelUserId",
                table: "Randevu",
                column: "panelUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Randevu_PanelUsers_panelUserId",
                table: "Randevu",
                column: "panelUserId",
                principalTable: "PanelUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Randevu_PanelUsers_panelUserId",
                table: "Randevu");

            migrationBuilder.DropIndex(
                name: "IX_Randevu_panelUserId",
                table: "Randevu");
        }
    }
}
