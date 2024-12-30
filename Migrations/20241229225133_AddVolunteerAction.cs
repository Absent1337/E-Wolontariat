using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Wolontariat.Migrations
{
    /// <inheritdoc />
    public partial class AddVolunteerAction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VolunteerActions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VolunteerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ActionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VolunteerActions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VolunteerActions_Actions_ActionId",
                        column: x => x.ActionId,
                        principalTable: "Actions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VolunteerActions_AspNetUsers_VolunteerId",
                        column: x => x.VolunteerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VolunteerActions_ActionId",
                table: "VolunteerActions",
                column: "ActionId");

            migrationBuilder.CreateIndex(
                name: "IX_VolunteerActions_VolunteerId",
                table: "VolunteerActions",
                column: "VolunteerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VolunteerActions");
        }
    }
}
