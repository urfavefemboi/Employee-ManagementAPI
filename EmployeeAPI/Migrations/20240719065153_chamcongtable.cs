using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeAPI.Migrations
{
    /// <inheritdoc />
    public partial class chamcongtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ThanhToan_ChamCong_ChamCongId",
                table: "ThanhToan");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ThanhToan",
                table: "ThanhToan");

            migrationBuilder.DropIndex(
                name: "IX_ThanhToan_ChamCongId",
                table: "ThanhToan");

            migrationBuilder.DropColumn(
                name: "NgayCong",
                table: "ChamCong");

            migrationBuilder.DropColumn(
                name: "NgayNghiHL",
                table: "ChamCong");

            migrationBuilder.RenameColumn(
                name: "ChamCongId",
                table: "ThanhToan",
                newName: "Id");

            migrationBuilder.AddColumn<DateTime>(
                name: "ClockOut",
                table: "ChamCong",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Clockin",
                table: "ChamCong",
                type: "date",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ThanhToan",
                table: "ThanhToan",
                column: "NhanVienId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ThanhToan",
                table: "ThanhToan");

            migrationBuilder.DropColumn(
                name: "ClockOut",
                table: "ChamCong");

            migrationBuilder.DropColumn(
                name: "Clockin",
                table: "ChamCong");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ThanhToan",
                newName: "ChamCongId");

            migrationBuilder.AddColumn<int>(
                name: "NgayCong",
                table: "ChamCong",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NgayNghiHL",
                table: "ChamCong",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ThanhToan",
                table: "ThanhToan",
                columns: new[] { "NhanVienId", "ChamCongId" });

            migrationBuilder.CreateIndex(
                name: "IX_ThanhToan_ChamCongId",
                table: "ThanhToan",
                column: "ChamCongId");

            migrationBuilder.AddForeignKey(
                name: "FK_ThanhToan_ChamCong_ChamCongId",
                table: "ThanhToan",
                column: "ChamCongId",
                principalTable: "ChamCong",
                principalColumn: "Id");
        }
    }
}
