using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eSchalt.Migrations
{
    /// <inheritdoc />
    public partial class MakeComponentCoordinatesDouble : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "YPosTopLeft",
                table: "Components",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<double>(
                name: "YPosBottomRight",
                table: "Components",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<double>(
                name: "XPosTopLeft",
                table: "Components",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<double>(
                name: "XPosBottomRight",
                table: "Components",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "XPosBottomRight", "XPosTopLeft", "YPosBottomRight", "YPosTopLeft" },
                values: new object[] { 163.0, 101.0, 211.0, 105.0 });

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "XPosBottomRight", "XPosTopLeft", "YPosBottomRight", "YPosTopLeft" },
                values: new object[] { 213.0, 166.0, 215.0, 111.0 });

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "XPosBottomRight", "XPosTopLeft", "YPosBottomRight", "YPosTopLeft" },
                values: new object[] { 252.0, 215.0, 215.0, 105.0 });

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "XPosBottomRight", "XPosTopLeft", "YPosBottomRight", "YPosTopLeft" },
                values: new object[] { 351.0, 329.0, 212.0, 111.0 });

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "XPosBottomRight", "XPosTopLeft", "YPosBottomRight", "YPosTopLeft" },
                values: new object[] { 372.0, 353.0, 211.0, 111.0 });

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "XPosBottomRight", "XPosTopLeft", "YPosBottomRight", "YPosTopLeft" },
                values: new object[] { 393.0, 374.0, 209.0, 111.0 });

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "XPosBottomRight", "XPosTopLeft", "YPosBottomRight", "YPosTopLeft" },
                values: new object[] { 413.0, 394.0, 212.0, 112.0 });

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "XPosBottomRight", "XPosTopLeft", "YPosBottomRight", "YPosTopLeft" },
                values: new object[] { 434.0, 415.0, 210.0, 112.0 });

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "XPosBottomRight", "XPosTopLeft", "YPosBottomRight", "YPosTopLeft" },
                values: new object[] { 455.0, 435.0, 210.0, 104.0 });

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "XPosBottomRight", "XPosTopLeft", "YPosBottomRight", "YPosTopLeft" },
                values: new object[] { 474.0, 456.0, 208.0, 109.0 });

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "XPosBottomRight", "XPosTopLeft", "YPosBottomRight", "YPosTopLeft" },
                values: new object[] { 497.0, 476.0, 211.0, 107.0 });

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "XPosBottomRight", "XPosTopLeft", "YPosBottomRight", "YPosTopLeft" },
                values: new object[] { 583.0, 548.0, 208.0, 109.0 });

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "XPosBottomRight", "XPosTopLeft", "YPosBottomRight", "YPosTopLeft" },
                values: new object[] { 603.0, 584.0, 209.0, 108.0 });

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "XPosBottomRight", "XPosTopLeft", "YPosBottomRight", "YPosTopLeft" },
                values: new object[] { 624.0, 604.0, 209.0, 109.0 });

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "XPosBottomRight", "XPosTopLeft", "YPosBottomRight", "YPosTopLeft" },
                values: new object[] { 644.0, 626.0, 208.0, 110.0 });

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "XPosBottomRight", "XPosTopLeft", "YPosBottomRight", "YPosTopLeft" },
                values: new object[] { 664.0, 645.0, 205.0, 107.0 });

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "XPosBottomRight", "XPosTopLeft", "YPosBottomRight", "YPosTopLeft" },
                values: new object[] { 694.0, 665.0, 207.0, 104.0 });

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "XPosBottomRight", "XPosTopLeft", "YPosBottomRight", "YPosTopLeft" },
                values: new object[] { 168.0, 126.0, 371.0, 278.0 });

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "XPosBottomRight", "XPosTopLeft", "YPosBottomRight", "YPosTopLeft" },
                values: new object[] { 207.0, 169.0, 370.0, 277.0 });

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "XPosBottomRight", "XPosTopLeft", "YPosBottomRight", "YPosTopLeft" },
                values: new object[] { 249.0, 207.0, 370.0, 275.0 });

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "XPosBottomRight", "XPosTopLeft", "YPosBottomRight", "YPosTopLeft" },
                values: new object[] { 300.0, 249.0, 367.0, 269.0 });

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "XPosBottomRight", "XPosTopLeft", "YPosBottomRight", "YPosTopLeft" },
                values: new object[] { 503.0, 425.0, 367.0, 274.0 });

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "XPosBottomRight", "XPosTopLeft", "YPosBottomRight", "YPosTopLeft" },
                values: new object[] { 579.0, 504.0, 363.0, 272.0 });

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "XPosBottomRight", "XPosTopLeft", "YPosBottomRight", "YPosTopLeft" },
                values: new object[] { 655.0, 580.0, 361.0, 272.0 });

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "XPosBottomRight", "XPosTopLeft", "YPosBottomRight", "YPosTopLeft" },
                values: new object[] { 735.0, 656.0, 362.0, 272.0 });

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "XPosBottomRight", "XPosTopLeft", "YPosBottomRight", "YPosTopLeft" },
                values: new object[] { 652.0, 561.0, 508.0, 411.0 });

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "XPosBottomRight", "XPosTopLeft", "YPosBottomRight", "YPosTopLeft" },
                values: new object[] { 729.0, 652.0, 506.0, 412.0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "YPosTopLeft",
                table: "Components",
                type: "integer",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<int>(
                name: "YPosBottomRight",
                table: "Components",
                type: "integer",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<int>(
                name: "XPosTopLeft",
                table: "Components",
                type: "integer",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<int>(
                name: "XPosBottomRight",
                table: "Components",
                type: "integer",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "XPosBottomRight", "XPosTopLeft", "YPosBottomRight", "YPosTopLeft" },
                values: new object[] { 163, 101, 211, 105 });

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "XPosBottomRight", "XPosTopLeft", "YPosBottomRight", "YPosTopLeft" },
                values: new object[] { 213, 166, 215, 111 });

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "XPosBottomRight", "XPosTopLeft", "YPosBottomRight", "YPosTopLeft" },
                values: new object[] { 252, 215, 215, 105 });

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "XPosBottomRight", "XPosTopLeft", "YPosBottomRight", "YPosTopLeft" },
                values: new object[] { 351, 329, 212, 111 });

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "XPosBottomRight", "XPosTopLeft", "YPosBottomRight", "YPosTopLeft" },
                values: new object[] { 372, 353, 211, 111 });

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "XPosBottomRight", "XPosTopLeft", "YPosBottomRight", "YPosTopLeft" },
                values: new object[] { 393, 374, 209, 111 });

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "XPosBottomRight", "XPosTopLeft", "YPosBottomRight", "YPosTopLeft" },
                values: new object[] { 413, 394, 212, 112 });

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "XPosBottomRight", "XPosTopLeft", "YPosBottomRight", "YPosTopLeft" },
                values: new object[] { 434, 415, 210, 112 });

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "XPosBottomRight", "XPosTopLeft", "YPosBottomRight", "YPosTopLeft" },
                values: new object[] { 455, 435, 210, 104 });

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "XPosBottomRight", "XPosTopLeft", "YPosBottomRight", "YPosTopLeft" },
                values: new object[] { 474, 456, 208, 109 });

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "XPosBottomRight", "XPosTopLeft", "YPosBottomRight", "YPosTopLeft" },
                values: new object[] { 497, 476, 211, 107 });

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "XPosBottomRight", "XPosTopLeft", "YPosBottomRight", "YPosTopLeft" },
                values: new object[] { 583, 548, 208, 109 });

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "XPosBottomRight", "XPosTopLeft", "YPosBottomRight", "YPosTopLeft" },
                values: new object[] { 603, 584, 209, 108 });

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "XPosBottomRight", "XPosTopLeft", "YPosBottomRight", "YPosTopLeft" },
                values: new object[] { 624, 604, 209, 109 });

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "XPosBottomRight", "XPosTopLeft", "YPosBottomRight", "YPosTopLeft" },
                values: new object[] { 644, 626, 208, 110 });

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "XPosBottomRight", "XPosTopLeft", "YPosBottomRight", "YPosTopLeft" },
                values: new object[] { 664, 645, 205, 107 });

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "XPosBottomRight", "XPosTopLeft", "YPosBottomRight", "YPosTopLeft" },
                values: new object[] { 694, 665, 207, 104 });

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "XPosBottomRight", "XPosTopLeft", "YPosBottomRight", "YPosTopLeft" },
                values: new object[] { 168, 126, 371, 278 });

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "XPosBottomRight", "XPosTopLeft", "YPosBottomRight", "YPosTopLeft" },
                values: new object[] { 207, 169, 370, 277 });

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "XPosBottomRight", "XPosTopLeft", "YPosBottomRight", "YPosTopLeft" },
                values: new object[] { 249, 207, 370, 275 });

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "XPosBottomRight", "XPosTopLeft", "YPosBottomRight", "YPosTopLeft" },
                values: new object[] { 300, 249, 367, 269 });

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "XPosBottomRight", "XPosTopLeft", "YPosBottomRight", "YPosTopLeft" },
                values: new object[] { 503, 425, 367, 274 });

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "XPosBottomRight", "XPosTopLeft", "YPosBottomRight", "YPosTopLeft" },
                values: new object[] { 579, 504, 363, 272 });

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "XPosBottomRight", "XPosTopLeft", "YPosBottomRight", "YPosTopLeft" },
                values: new object[] { 655, 580, 361, 272 });

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "XPosBottomRight", "XPosTopLeft", "YPosBottomRight", "YPosTopLeft" },
                values: new object[] { 735, 656, 362, 272 });

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "XPosBottomRight", "XPosTopLeft", "YPosBottomRight", "YPosTopLeft" },
                values: new object[] { 652, 561, 508, 411 });

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "XPosBottomRight", "XPosTopLeft", "YPosBottomRight", "YPosTopLeft" },
                values: new object[] { 729, 652, 506, 412 });
        }
    }
}
