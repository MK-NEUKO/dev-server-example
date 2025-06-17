using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace environmentgateway.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DestinationOwnedFromCluster : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "destinations");

            migrationBuilder.CreateTable(
                name: "destination",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    cluster_id = table.Column<Guid>(type: "uuid", nullable: false),
                    destination_name = table.Column<string>(type: "text", nullable: false),
                    address = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_destination", x => x.id);
                    table.ForeignKey(
                        name: "fk_destination_clusters_cluster_id",
                        column: x => x.cluster_id,
                        principalTable: "clusters",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_destination_cluster_id",
                table: "destination",
                column: "cluster_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "destination");

            migrationBuilder.CreateTable(
                name: "destinations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    cluster_id = table.Column<Guid>(type: "uuid", nullable: false),
                    address_value = table.Column<string>(type: "text", nullable: false),
                    destination_name_value = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
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
                name: "ix_destinations_cluster_id",
                table: "destinations",
                column: "cluster_id");
        }
    }
}
