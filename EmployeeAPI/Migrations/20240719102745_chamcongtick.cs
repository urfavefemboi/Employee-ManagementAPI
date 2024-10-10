using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeAPI.Migrations
{
    /// <inheritdoc />
    public partial class chamcongtick : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
       name: "Clockin",
       table: "ChamCong");

            migrationBuilder.AddColumn<DateTime>(
                name: "Clockin",
                table: "ChamCong",
                nullable: false,
                type: "bigint");

            migrationBuilder.DropColumn(
      name: "ClockOut",
      table: "ChamCong");

            migrationBuilder.AddColumn<DateTime>(
                name: "ClockOut",
                table: "ChamCong",
                nullable: false,
                type: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "Clockin",
                table: "ChamCong",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "time",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ClockOut",
                table: "ChamCong",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "time",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
      name: "Clockin",
      table: "ChamCong");

            migrationBuilder.AddColumn<DateTime>(
                name: "Clockin",
                table: "ChamCong",
                nullable: false,
                type: "bigint");

            migrationBuilder.DropColumn(
      name: "ClockOut",
      table: "ChamCong");

            migrationBuilder.AddColumn<DateTime>(
                name: "ClockOut",
                table: "ChamCong",
                nullable: false,
                type: "bigint");
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "Clockin",
                table: "ChamCong",
                type: "time",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "ClockOut",
                table: "ChamCong",
                type: "time",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);
        }
    }
}
