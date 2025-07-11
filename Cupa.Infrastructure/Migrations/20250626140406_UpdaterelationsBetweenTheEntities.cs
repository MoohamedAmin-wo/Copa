using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cupa.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdaterelationsBetweenTheEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pictures_Users_UserId",
                schema: "Support",
                table: "Pictures");

            migrationBuilder.DropIndex(
                name: "IX_Pictures_UserId",
                schema: "Support",
                table: "Pictures");

            migrationBuilder.DropIndex(
                name: "IX_Palyers_UserId",
                schema: "Main",
                table: "Palyers");

            migrationBuilder.DropIndex(
                name: "IX_Managers_UserId",
                schema: "Main",
                table: "Managers");

            migrationBuilder.DropIndex(
                name: "IX_Admins_UserId",
                schema: "Support",
                table: "Admins");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "Support",
                table: "Pictures");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "Support",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "Support",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                schema: "Support",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Support",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "Support",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                schema: "Support",
                table: "Addresses");

            migrationBuilder.CreateIndex(
                name: "IX_Palyers_UserId",
                schema: "Main",
                table: "Palyers",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Managers_UserId",
                schema: "Main",
                table: "Managers",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Admins_UserId",
                schema: "Support",
                table: "Admins",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Palyers_UserId",
                schema: "Main",
                table: "Palyers");

            migrationBuilder.DropIndex(
                name: "IX_Managers_UserId",
                schema: "Main",
                table: "Managers");

            migrationBuilder.DropIndex(
                name: "IX_Admins_UserId",
                schema: "Support",
                table: "Admins");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                schema: "Support",
                table: "Pictures",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "Support",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "Support",
                table: "Addresses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                schema: "Support",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Support",
                table: "Addresses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "Support",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                schema: "Support",
                table: "Addresses",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pictures_UserId",
                schema: "Support",
                table: "Pictures",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Palyers_UserId",
                schema: "Main",
                table: "Palyers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Managers_UserId",
                schema: "Main",
                table: "Managers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Admins_UserId",
                schema: "Support",
                table: "Admins",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pictures_Users_UserId",
                schema: "Support",
                table: "Pictures",
                column: "UserId",
                principalSchema: "Idn",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}