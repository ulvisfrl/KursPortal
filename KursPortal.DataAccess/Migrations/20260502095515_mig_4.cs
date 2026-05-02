using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KursPortal.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class mig_4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Item1",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Item2",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Item3",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Item4",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Item1",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Item2",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Item3",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Item4",
                table: "Courses");
        }
    }
}
