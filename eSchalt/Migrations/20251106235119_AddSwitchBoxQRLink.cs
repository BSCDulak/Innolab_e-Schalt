using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace eSchalt.Migrations
{
    /// <inheritdoc />
    public partial class AddSwitchBoxQRLink : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SwitchBoxQRLinks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SwitchBoxId = table.Column<int>(type: "integer", nullable: false),
                    QRLink = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SwitchBoxQRLinks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SwitchBoxQRLinks_SwitchBoxes_SwitchBoxId",
                        column: x => x.SwitchBoxId,
                        principalTable: "SwitchBoxes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "SwitchBoxQRLinks",
                columns: new[] { "Id", "QRLink", "SwitchBoxId" },
                values: new object[] { 1, "http://localhost:5000/detail?fileName=32fe4380-615b-4ed7-8622-a981303264dc.png", 1 });

            migrationBuilder.CreateIndex(
                name: "IX_SwitchBoxQRLinks_SwitchBoxId",
                table: "SwitchBoxQRLinks",
                column: "SwitchBoxId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SwitchBoxQRLinks");
        }
    }
}
