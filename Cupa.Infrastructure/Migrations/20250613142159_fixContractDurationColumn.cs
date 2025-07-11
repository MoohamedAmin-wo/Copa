using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cupa.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixContractDurationColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ContractDuration",
                schema: "Main",
                table: "ClubPlayers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "time",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "ContractDuration",
                schema: "Main",
                table: "ClubPlayers",
                type: "time",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
