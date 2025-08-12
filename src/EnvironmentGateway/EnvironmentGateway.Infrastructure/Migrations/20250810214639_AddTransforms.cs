using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace environmentgateway.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTransforms : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "transforms",
                table: "routes",
                type: "jsonb",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "transforms",
                table: "routes");
        }
    }
}
