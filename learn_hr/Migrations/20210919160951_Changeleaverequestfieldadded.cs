using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace learn_hr.Migrations
{
    public partial class Changeleaverequestfieldadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Cancelled",
                table: "LeaveRequests",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RequestedComments",
                table: "LeaveRequests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EmployeeVM",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmpNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmpDeptCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmpGradeCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmpQuarterNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateOfJoining = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateLastPromotion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeVM", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LeaveRequestVM",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestingEmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LeaveTypeId = table.Column<int>(type: "int", nullable: false),
                    DateRequested = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RequestedComments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateActioned = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Approved = table.Column<bool>(type: "bit", nullable: true),
                    Cancelled = table.Column<bool>(type: "bit", nullable: true),
                    ApprovedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveRequestVM", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeaveRequestVM_AspNetUsers_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LeaveRequestVM_EmployeeVM_RequestingEmployeeId",
                        column: x => x.RequestingEmployeeId,
                        principalTable: "EmployeeVM",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LeaveRequestVM_LeaveTypes_LeaveTypeId",
                        column: x => x.LeaveTypeId,
                        principalTable: "LeaveTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequestVM_ApprovedById",
                table: "LeaveRequestVM",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequestVM_LeaveTypeId",
                table: "LeaveRequestVM",
                column: "LeaveTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequestVM_RequestingEmployeeId",
                table: "LeaveRequestVM",
                column: "RequestingEmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeaveRequestVM");

            migrationBuilder.DropTable(
                name: "EmployeeVM");

            migrationBuilder.DropColumn(
                name: "Cancelled",
                table: "LeaveRequests");

            migrationBuilder.DropColumn(
                name: "RequestedComments",
                table: "LeaveRequests");
        }
    }
}
