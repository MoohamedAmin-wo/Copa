using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cupa.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAboutCloumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StoryAbout",
                schema: "Main",
                table: "Clubs",
                newName: "Story");

            migrationBuilder.AddColumn<string>(
                name: "About",
                schema: "Main",
                table: "Clubs",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "About",
                schema: "Main",
                table: "Clubs");

            migrationBuilder.RenameColumn(
                name: "Story",
                schema: "Main",
                table: "Clubs",
                newName: "StoryAbout");
        }
    }
}
