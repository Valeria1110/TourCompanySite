using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    public partial class Migr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_orders_schedules_schedule_id",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "id",
                table: "schedules");

            migrationBuilder.DropColumn(
                name: "id_s",
                table: "orders");

            migrationBuilder.AddColumn<string>(
                name: "age",
                table: "orders",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "fk_orders_schedules_schedule_temp_id",
                table: "orders",
                column: "schedule_id_s",
                principalTable: "schedules",
                principalColumn: "id_s",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_orders_schedules_schedule_temp_id",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "age",
                table: "orders");

            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "schedules",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "id_s",
                table: "orders",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "fk_orders_schedules_schedule_id",
                table: "orders",
                column: "schedule_id_s",
                principalTable: "schedules",
                principalColumn: "id_s",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
