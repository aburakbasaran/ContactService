using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ContactService.ReportModule.Data.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "reports",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_date_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    is_completed = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_reports", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "report_details",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    report_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_count = table.Column<int>(type: "integer", nullable: false),
                    phone_number_count = table.Column<int>(type: "integer", nullable: false),
                    location = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_report_details", x => x.id);
                    table.ForeignKey(
                        name: "fk_report_details_reports_report_id",
                        column: x => x.report_id,
                        principalSchema: "dbo",
                        principalTable: "reports",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_report_details_report_id",
                schema: "dbo",
                table: "report_details",
                column: "report_id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "report_details",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "reports",
                schema: "dbo");
        }
    }
}
