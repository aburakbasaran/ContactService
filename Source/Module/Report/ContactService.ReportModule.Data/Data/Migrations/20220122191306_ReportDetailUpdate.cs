using Microsoft.EntityFrameworkCore.Migrations;

namespace ContactService.ReportModule.Data.Data.Migrations
{
    public partial class ReportDetailUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "phone_number_count",
                schema: "dbo",
                table: "report_details");

            migrationBuilder.DropColumn(
                name: "user_count",
                schema: "dbo",
                table: "report_details");

            migrationBuilder.RenameColumn(
                name: "location",
                schema: "dbo",
                table: "report_details",
                newName: "report_json");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "report_json",
                schema: "dbo",
                table: "report_details",
                newName: "location");

            migrationBuilder.AddColumn<int>(
                name: "phone_number_count",
                schema: "dbo",
                table: "report_details",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "user_count",
                schema: "dbo",
                table: "report_details",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
