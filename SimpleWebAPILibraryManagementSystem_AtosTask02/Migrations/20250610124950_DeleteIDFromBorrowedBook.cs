using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleWebAPILibraryManagementSystem_AtosTask02.Migrations
{
    /// <inheritdoc />
    public partial class DeleteIDFromBorrowedBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "BorrowedBooks");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "BorrowedBooks",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
