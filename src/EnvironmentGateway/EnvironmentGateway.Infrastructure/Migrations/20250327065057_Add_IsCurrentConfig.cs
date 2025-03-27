using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnvironmentGateway.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_IsCurrentConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "gateway-configurations",
                newName: "gateway_configurations");

            migrationBuilder.AddColumn<bool>(
                name: "is_current_config",
                table: "gateway_configurations",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_current_config",
                table: "gateway_configurations");

            migrationBuilder.RenameTable(
                name: "gateway_configurations",
                newName: "gateway-configurations");
        }
    }
}
