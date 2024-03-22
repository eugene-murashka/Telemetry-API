using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Telemetries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UnitId = table.Column<int>(type: "INTEGER", nullable: false),
                    Type = table.Column<string>(type: "TEXT", maxLength: 8, nullable: false),
                    Prop1 = table.Column<string>(type: "TEXT", nullable: true),
                    Prop2 = table.Column<string>(type: "TEXT", nullable: true),
                    Prop3 = table.Column<string>(type: "TEXT", nullable: true),
                    Prop4 = table.Column<string>(type: "TEXT", nullable: true),
                    Prop5 = table.Column<string>(type: "TEXT", nullable: true),
                    Prop6 = table.Column<string>(type: "TEXT", nullable: true),
                    Prop7 = table.Column<string>(type: "TEXT", nullable: true),
                    Prop8 = table.Column<string>(type: "TEXT", nullable: true),
                    Prop9 = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Telemetries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Telemetries_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Telemetries_UnitId",
                table: "Telemetries",
                column: "UnitId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Telemetries");

            migrationBuilder.DropTable(
                name: "Units");
        }
    }
}
