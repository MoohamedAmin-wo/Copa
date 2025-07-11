using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cupa.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRelationBetweenUserandAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Addresses_AddressId",
                schema: "Idn",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_AddressId",
                schema: "Idn",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AddressId",
                schema: "Idn",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                schema: "Support",
                table: "Addresses",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_UserId",
                schema: "Support",
                table: "Addresses",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Users_UserId",
                schema: "Support",
                table: "Addresses",
                column: "UserId",
                principalSchema: "Idn",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Users_UserId",
                schema: "Support",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_UserId",
                schema: "Support",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "Support",
                table: "Addresses");

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                schema: "Idn",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_AddressId",
                schema: "Idn",
                table: "Users",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Addresses_AddressId",
                schema: "Idn",
                table: "Users",
                column: "AddressId",
                principalSchema: "Support",
                principalTable: "Addresses",
                principalColumn: "Id");
        }
    }
}
