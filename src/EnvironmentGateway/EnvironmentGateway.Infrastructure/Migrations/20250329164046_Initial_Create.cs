using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace environmentgateway.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial_Create : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "gateway_configs",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    is_current_config = table.Column<bool>(type: "boolean", nullable: false),
                    name_value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_gateway_configs", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "clusters",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    gateway_config_id = table.Column<Guid>(type: "uuid", nullable: false),
                    cluster_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_clusters", x => x.id);
                    table.ForeignKey(
                        name: "fk_clusters_gateway_configs_gateway_config_id",
                        column: x => x.gateway_config_id,
                        principalTable: "gateway_configs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "routes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    gateway_config_id = table.Column<Guid>(type: "uuid", nullable: false),
                    cluster_name_value = table.Column<string>(type: "text", nullable: false),
                    route_name_value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_routes", x => x.id);
                    table.ForeignKey(
                        name: "fk_routes_gateway_configs_gateway_config_id",
                        column: x => x.gateway_config_id,
                        principalTable: "gateway_configs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "destinations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    cluster_id = table.Column<Guid>(type: "uuid", nullable: false),
                    address_value = table.Column<string>(type: "text", nullable: false),
                    destination_name_value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_destinations", x => x.id);
                    table.ForeignKey(
                        name: "fk_destinations_clusters_cluster_id",
                        column: x => x.cluster_id,
                        principalTable: "clusters",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
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
                        principalTable: "routes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_clusters_gateway_config_id",
                table: "clusters",
                column: "gateway_config_id");

            migrationBuilder.CreateIndex(
                name: "ix_destinations_cluster_id",
                table: "destinations",
                column: "cluster_id");

            migrationBuilder.CreateIndex(
                name: "ix_route_matches_route_id",
                table: "route_matches",
                column: "route_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_routes_gateway_config_id",
                table: "routes",
                column: "gateway_config_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "destinations");

            migrationBuilder.DropTable(
                name: "route_matches");

            migrationBuilder.DropTable(
                name: "clusters");

            migrationBuilder.DropTable(
                name: "routes");

            migrationBuilder.DropTable(
                name: "gateway_configs");
        }
    }
}
