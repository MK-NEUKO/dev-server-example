using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace environmentgateway.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTransforms : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "transforms",
                table: "routes");

            migrationBuilder.CreateTable(
                name: "route_transforms",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    route_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_route_transforms", x => x.id);
                    table.ForeignKey(
                        name: "fk_route_transforms_routes_route_id",
                        column: x => x.route_id,
                        principalTable: "routes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "route_transform_items",
                columns: table => new
                {
                    route_transforms_id = table.Column<Guid>(type: "uuid", nullable: false),
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    key = table.Column<string>(type: "text", nullable: false),
                    value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_route_transform_items", x => new { x.route_transforms_id, x.id });
                    table.ForeignKey(
                        name: "fk_route_transform_items_route_transforms_route_transforms_id",
                        column: x => x.route_transforms_id,
                        principalTable: "route_transforms",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_route_transforms_route_id",
                table: "route_transforms",
                column: "route_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "route_transform_items");

            migrationBuilder.DropTable(
                name: "route_transforms");

            migrationBuilder.AddColumn<string>(
                name: "transforms",
                table: "routes",
                type: "jsonb",
                nullable: false,
                defaultValue: "");
        }
    }
}
