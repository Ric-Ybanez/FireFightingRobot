using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FireFightingRobot.DAL.Migrations
{
    public partial class FireAndAlertLevel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AlertLevel",
                table: "DeviceHistories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "FireDetected",
                table: "DeviceHistories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AlertLevel",
                table: "DeviceHistories");

            migrationBuilder.DropColumn(
                name: "FireDetected",
                table: "DeviceHistories");
        }
    }
}
