using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api_event_panel.Migrations
{
    /// <inheritdoc />
    public partial class AddUserModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileType",
                table: "Media",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PosterPath",
                table: "Media",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Media",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SessionId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Media_UserId",
                table: "Media",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Media_Users_UserId",
                table: "Media",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Media_Users_UserId",
                table: "Media");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Media_UserId",
                table: "Media");

            migrationBuilder.DropColumn(
                name: "FileType",
                table: "Media");

            migrationBuilder.DropColumn(
                name: "PosterPath",
                table: "Media");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Media");
        }
    }
}
