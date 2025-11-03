using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class Update2Rdv : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "commentaire",
                table: "Rdvs",
                newName: "Registrations");

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "Rdvs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Rdvs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comment",
                table: "Rdvs");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Rdvs");

            migrationBuilder.RenameColumn(
                name: "Registrations",
                table: "Rdvs",
                newName: "commentaire");
        }
    }
}
