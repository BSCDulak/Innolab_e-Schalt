using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace eSchalt.Migrations
{
    /// <inheritdoc />
    public partial class AddSwitchBoxAndComponentTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SwitchBoxes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Floor = table.Column<string>(type: "text", nullable: false),
                    Room = table.Column<string>(type: "text", nullable: false),
                    Group = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SwitchBoxes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Components",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    XPosTopLeft = table.Column<int>(type: "integer", nullable: false),
                    YPosTopLeft = table.Column<int>(type: "integer", nullable: false),
                    XPosBottomRight = table.Column<int>(type: "integer", nullable: false),
                    YPosBottomRight = table.Column<int>(type: "integer", nullable: false),
                    SwitchBoxId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Components", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Components_SwitchBoxes_SwitchBoxId",
                        column: x => x.SwitchBoxId,
                        principalTable: "SwitchBoxes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComponentConnections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FromComponentId = table.Column<int>(type: "integer", nullable: false),
                    ToComponentId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentConnections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComponentConnections_Components_FromComponentId",
                        column: x => x.FromComponentId,
                        principalTable: "Components",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ComponentConnections_Components_ToComponentId",
                        column: x => x.ToComponentId,
                        principalTable: "Components",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "SwitchBoxes",
                columns: new[] { "Id", "Floor", "Group", "Room", "Type" },
                values: new object[] { 1, "EG", "E-DO15", "", "" });

            migrationBuilder.InsertData(
                table: "Components",
                columns: new[] { "Id", "Name", "SwitchBoxId", "XPosBottomRight", "XPosTopLeft", "YPosBottomRight", "YPosTopLeft" },
                values: new object[,]
                {
                    { 1, "S1", 1, 163, 101, 211, 105 },
                    { 2, "S2", 1, 213, 166, 215, 111 },
                    { 3, "S3", 1, 252, 215, 215, 105 },
                    { 4, "S4", 1, 351, 329, 212, 111 },
                    { 5, "S5", 1, 372, 353, 211, 111 },
                    { 6, "S6", 1, 393, 374, 209, 111 },
                    { 7, "S7", 1, 413, 394, 212, 112 },
                    { 8, "S8", 1, 434, 415, 210, 112 },
                    { 9, "S9", 1, 455, 435, 210, 104 },
                    { 10, "S10", 1, 474, 456, 208, 109 },
                    { 11, "S11", 1, 497, 476, 211, 107 },
                    { 12, "S12", 1, 583, 548, 208, 109 },
                    { 13, "S13", 1, 603, 584, 209, 108 },
                    { 14, "S14", 1, 624, 604, 209, 109 },
                    { 15, "S15", 1, 644, 626, 208, 110 },
                    { 16, "S16", 1, 664, 645, 205, 107 },
                    { 17, "S17", 1, 694, 665, 207, 104 },
                    { 18, "R1", 1, 168, 126, 371, 278 },
                    { 19, "R2", 1, 207, 169, 370, 277 },
                    { 20, "R3", 1, 249, 207, 370, 275 },
                    { 21, "R4", 1, 300, 249, 367, 269 },
                    { 22, "R5", 1, 503, 425, 367, 274 },
                    { 23, "R6", 1, 579, 504, 363, 272 },
                    { 24, "R7", 1, 655, 580, 361, 272 },
                    { 25, "R8", 1, 735, 656, 362, 272 },
                    { 26, "R9", 1, 652, 561, 508, 411 },
                    { 27, "R0", 1, 729, 652, 506, 412 }
                });

            migrationBuilder.InsertData(
                table: "ComponentConnections",
                columns: new[] { "Id", "FromComponentId", "ToComponentId" },
                values: new object[,]
                {
                    { 1, 1, 18 },
                    { 2, 1, 19 },
                    { 3, 2, 19 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComponentConnections_FromComponentId",
                table: "ComponentConnections",
                column: "FromComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentConnections_ToComponentId",
                table: "ComponentConnections",
                column: "ToComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_Components_SwitchBoxId",
                table: "Components",
                column: "SwitchBoxId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComponentConnections");

            migrationBuilder.DropTable(
                name: "Components");

            migrationBuilder.DropTable(
                name: "SwitchBoxes");
        }
    }
}
