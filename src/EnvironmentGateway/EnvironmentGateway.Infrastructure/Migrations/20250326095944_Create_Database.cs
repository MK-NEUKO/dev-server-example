using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnvironmentGateway.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Create_Database : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "gateway-configurations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_gateway_configurations", x => x.id);
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
                        name: "fk_clusters_gateway_config_gateway_config_id",
                        column: x => x.gateway_config_id,
                        principalTable: "gateway-configurations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Routs",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    gateway_config_id = table.Column<Guid>(type: "uuid", nullable: false),
                    cluster_name_value = table.Column<string>(type: "text", nullable: false),
                    match_path = table.Column<string>(type: "text", nullable: false),
                    route_name_value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_routs", x => x.id);
                    table.ForeignKey(
                        name: "fk_routs_gateway_configurations_gateway_config_id",
                        column: x => x.gateway_config_id,
                        principalTable: "gateway-configurations",
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

            migrationBuilder.CreateIndex(
                name: "ix_clusters_gateway_config_id",
                table: "clusters",
                column: "gateway_config_id");

            migrationBuilder.CreateIndex(
                name: "ix_destinations_cluster_id",
                table: "destinations",
                column: "cluster_id");

            migrationBuilder.CreateIndex(
                name: "ix_routs_gateway_config_id",
                table: "Routs",
                column: "gateway_config_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "destinations");

            migrationBuilder.DropTable(
                name: "Routs");

            migrationBuilder.DropTable(
                name: "clusters");

            migrationBuilder.DropTable(
                name: "gateway-configurations");
        }
    }
}
