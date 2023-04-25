using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeepfakeDetectionFramework.Migrations
{
    /// <inheritdoc />
    public partial class MethodIDColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "MethodID",
                table: "Responses",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MethodID",
                table: "Responses");
        }
    }
}
