using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Wolontariat.Migrations
{
    /// <inheritdoc />
    public partial class FourthdMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Actions",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Actions",
                newName: "Location");

            migrationBuilder.RenameColumn(
                name: "Author",
                table: "Actions",
                newName: "Description");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Actions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<int>(
                name: "MaxParticipants",
                table: "Actions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Application",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ActionId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Application", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Application_Actions_ActionId",
                        column: x => x.ActionId,
                        principalTable: "Actions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Application_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Application_ActionId",
                table: "Application",
                column: "ActionId");

            migrationBuilder.CreateIndex(
                name: "IX_Application_UserId",
                table: "Application",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Application");

            migrationBuilder.DropColumn(
                name: "MaxParticipants",
                table: "Actions");

            migrationBuilder.RenameColumn(
                name: "Location",
                table: "Actions",
                newName: "Content");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Actions",
                newName: "Author");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Actions",
                newName: "CreatedAt");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Actions",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
