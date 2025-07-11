using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cupa.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePlayerentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ClubPlayers_ClubId",
                table: "ClubPlayers");

            migrationBuilder.RenameTable(
                name: "ClubPlayers",
                newName: "ClubPlayers",
                newSchema: "Main");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "JoinedClubOn",
                schema: "Main",
                table: "ClubPlayers",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClubPlayers_ClubId_Number",
                schema: "Main",
                table: "ClubPlayers",
                columns: new[] { "ClubId", "Number" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ClubPlayers_ClubId_Number",
                schema: "Main",
                table: "ClubPlayers");

            migrationBuilder.RenameTable(
                name: "ClubPlayers",
                schema: "Main",
                newName: "ClubPlayers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "JoinedClubOn",
                table: "ClubPlayers",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClubPlayers_ClubId",
                table: "ClubPlayers",
                column: "ClubId");
        }
    }
}
