using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prismo.Core.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Nit = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RouterTelemtries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RouterId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ChannelOccupancy = table.Column<double>(type: "double precision", nullable: false),
                    ConnectedDevices = table.Column<int>(type: "integer", nullable: false),
                    PacketLossRate = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RouterTelemtries", x => new { x.TimeStamp, x.Id });
                });

            migrationBuilder.CreateIndex(
                name: "IX_Companies_Nit",
                table: "Companies",
                column: "Nit",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RouterTelemtries_RouterId",
                table: "RouterTelemtries",
                column: "RouterId");

            migrationBuilder.Sql("CREATE EXTENSION IF NOT EXISTS timescaledb CASCADE;");

            // 2. Convertimos la tabla limpia en una Hypertable real particionada por tiempo
            // Nota: Usamos "RouterTelemtries" exactamente igual a como EF Core la mapeó arriba
            migrationBuilder.Sql("SELECT create_hypertable('\"RouterTelemtries\"', 'TimeStamp', migrate_data => true);");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "RouterTelemtries");
        }
    }
}
