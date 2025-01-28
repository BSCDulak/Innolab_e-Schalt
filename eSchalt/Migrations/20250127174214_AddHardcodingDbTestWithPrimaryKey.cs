using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace eSchalt.Migrations
{
    /// <inheritdoc />
    public partial class AddHardcodingDbTestWithPrimaryKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "eschaltdemo",
                columns: table => new
                {
                    componentid = table.Column<int>(type: "integer", nullable: true),
                    stockwerk = table.Column<string>(type: "text", nullable: true),
                    raum = table.Column<string>(type: "text", nullable: true),
                    bemerkung = table.Column<string>(type: "text", nullable: true),
                    fi = table.Column<string>(type: "text", nullable: true),
                    leiter = table.Column<string>(type: "text", nullable: true),
                    gruppe = table.Column<string>(type: "text", nullable: true),
                    sicherung = table.Column<string>(type: "text", nullable: true),
                    relais = table.Column<string>(type: "text", nullable: true),
                    dimmer = table.Column<string>(type: "text", nullable: true),
                    ausgang = table.Column<string>(type: "text", nullable: true),
                    eingang = table.Column<string>(type: "text", nullable: true),
                    KabelInfo = table.Column<string>(name: "Kabel Info", type: "text", nullable: true),
                    typ = table.Column<string>(type: "text", nullable: true),
                    Info = table.Column<string>(name: "Info ", type: "text", nullable: true),
                    Beschr = table.Column<string>(name: "Beschr. ", type: "text", nullable: true),
                    Stockwerkkurz = table.Column<string>(name: "Stockwerk(kurz)", type: "text", nullable: true),
                    SPSPositionimarray = table.Column<string>(name: "SPS Position im array", type: "text", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "hardcodingdbtest",
                columns: table => new
                {
                    componentid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    stockwerk = table.Column<string>(type: "text", nullable: true),
                    raum = table.Column<string>(type: "text", nullable: true),
                    bemerkung = table.Column<string>(type: "text", nullable: true),
                    fi = table.Column<string>(type: "text", nullable: true),
                    leiter = table.Column<string>(type: "text", nullable: true),
                    gruppe = table.Column<string>(type: "text", nullable: true),
                    sicherung = table.Column<string>(type: "text", nullable: true),
                    relais = table.Column<string>(type: "text", nullable: true),
                    dimmer = table.Column<string>(type: "text", nullable: true),
                    ausgang = table.Column<string>(type: "text", nullable: true),
                    eingang = table.Column<string>(type: "text", nullable: true),
                    KabelInfo = table.Column<string>(name: "Kabel Info", type: "text", nullable: true),
                    typ = table.Column<string>(type: "text", nullable: true),
                    Info = table.Column<string>(name: "Info ", type: "text", nullable: true),
                    Beschr = table.Column<string>(name: "Beschr. ", type: "text", nullable: true),
                    Stockwerkkurz = table.Column<string>(name: "Stockwerk(kurz)", type: "text", nullable: true),
                    SPSPositionimarray = table.Column<string>(name: "SPS Position im array", type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hardcodingdbtest", x => x.componentid);
                });

            migrationBuilder.InsertData(
                table: "hardcodingdbtest",
                columns: new[] { "componentid", "ausgang", "bemerkung", "Beschr. ", "dimmer", "eingang", "fi", "gruppe", "Info ", "Kabel Info", "leiter", "raum", "relais", "sicherung", "SPS Position im array", "stockwerk", "Stockwerk(kurz)", "typ" },
                values: new object[,]
                {
                    { 1, "A1", "Remark 1", "Description 1", "D1", "I1", "FI-A", "G1", "Info 1", "Copper", "L1", "101", "R1", "S1", "1", "First Floor", "FF", "Type A" },
                    { 2, "A2", "Remark 2", "Description 2", "D2", "I2", "FI-B", "G2", "Info 2", "Fiber", "L2", "102", "R2", "S2", "2", "First Floor", "FF", "Type B" },
                    { 3, "A3", "Remark 3", "Description 3", "D3", "I3", "FI-C", "G3", "Info 3", "Copper", "L3", "201", "R3", "S3", "3", "Second Floor", "SF", "Type C" },
                    { 4, "A4", "Remark 4", "Description 4", "D4", "I4", "FI-D", "G4", "Info 4", "Fiber", "L4", "202", "R4", "S4", "4", "Second Floor", "SF", "Type D" },
                    { 5, "A5", "Remark 5", "Description 5", "D5", "I5", "FI-E", "G5", "Info 5", "Copper", "L5", "301", "R5", "S5", "5", "Third Floor", "TF", "Type E" },
                    { 6, "A6", "Remark 6", "Description 6", "D6", "I6", "FI-F", "G6", "Info 6", "Fiber", "L6", "302", "R6", "S6", "6", "Third Floor", "TF", "Type F" },
                    { 7, "A7", "Remark 7", "Description 7", "D7", "I7", "FI-G", "G7", "Info 7", "Copper", "L7", "401", "R7", "S7", "7", "Fourth Floor", "FoF", "Type G" },
                    { 8, "A8", "Remark 8", "Description 8", "D8", "I8", "FI-H", "G8", "Info 8", "Fiber", "L8", "402", "R8", "S8", "8", "Fourth Floor", "FoF", "Type H" },
                    { 9, "A9", "Remark 9", "Description 9", "D9", "I9", "FI-I", "G9", "Info 9", "Copper", "L9", "501", "R9", "S9", "9", "Fifth Floor", "FiF", "Type I" },
                    { 10, "A10", "Remark 10", "Description 10", "D10", "I10", "FI-J", "G10", "Info 10", "Fiber", "L10", "502", "R10", "S10", "10", "Fifth Floor", "FiF", "Type J" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "eschaltdemo");

            migrationBuilder.DropTable(
                name: "hardcodingdbtest");
        }
    }
}
