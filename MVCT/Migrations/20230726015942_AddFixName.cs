using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCT.Migrations
{
    /// <inheritdoc />
    public partial class AddFixName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DataTime",
                table: "Attandances",
                newName: "DateTime");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "Attandances",
                newName: "DataTime");
        }
    }
}
