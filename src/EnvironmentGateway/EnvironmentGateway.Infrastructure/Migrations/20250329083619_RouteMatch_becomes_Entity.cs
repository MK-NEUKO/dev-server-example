using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnvironmentGateway.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RouteMatch_becomes_Entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_clusters_gateway_config_gateway_config_id",
                table: "clusters");

            migrationBuilder.DropForeignKey(
                name: "fk_routs_gateway_configurations_gateway_config_id",
                table: "Routs");

            migrationBuilder.DropTable(
                name: "gateway_configurations");

            migrationBuilder.DropColumn(
                name: "match_path",
                table: "Routs");

            migrationBuilder.RenameTable(
                name: "Routs",
                newName: "routs");

            migrationBuilder.CreateTable(
                name: "gateway_configs",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    is_current_config = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_gateway_configs", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "route_matches",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    route_id = table.Column<Guid>(type: "uuid", nullable: false),
                    path_value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_route_matches", x => x.id);
                    table.ForeignKey(
                        name: "fk_route_matches_routes_route_id",
                        column: x => x.route_id,
                        principalTable: "routs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_route_matches_route_id",
                table: "route_matches",
                column: "route_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_clusters_gateway_configs_gateway_config_id",
                table: "clusters",
                column: "gateway_config_id",
                principalTable: "gateway_configs",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_routs_gateway_configs_gateway_config_id",
                table: "routs",
                column: "gateway_config_id",
                principalTable: "gateway_configs",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_clusters_gateway_configs_gateway_config_id",
                table: "clusters");

            migrationBuilder.DropForeignKey(
                name: "fk_routs_gateway_configs_gateway_config_id",
                table: "routs");

            migrationBuilder.DropTable(
                name: "gateway_configs");

            migrationBuilder.DropTable(
                name: "route_matches");

            migrationBuilder.RenameTable(
                name: "routs",
                newName: "Routs");

            migrationBuilder.AddColumn<string>(
                name: "match_path",
                table: "Routs",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "gateway_configurations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    is_current_config = table.Column<bool>(type: "boolean", nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_gateway_configurations", x => x.id);
                });

            migrationBuilder.AddForeignKey(
                name: "fk_clusters_gateway_config_gateway_config_id",
                table: "clusters",
                column: "gateway_config_id",
                principalTable: "gateway_configurations",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_routs_gateway_configurations_gateway_config_id",
                table: "Routs",
                column: "gateway_config_id",
                principalTable: "gateway_configurations",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
