using Microsoft.EntityFrameworkCore.Migrations;

namespace learn_hr.Data.Migrations
{
    public partial class addedleavedatapointmodified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveHistories_AspNetUsers_EmployeeId",
                table: "LeaveHistories");

            migrationBuilder.DropIndex(
                name: "IX_LeaveHistories_EmployeeId",
                table: "LeaveHistories");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "LeaveHistories");

            migrationBuilder.AlterColumn<string>(
                name: "RequestingEmployeeId",
                table: "LeaveHistories",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LeaveHistories_RequestingEmployeeId",
                table: "LeaveHistories",
                column: "RequestingEmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveHistories_AspNetUsers_RequestingEmployeeId",
                table: "LeaveHistories",
                column: "RequestingEmployeeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveHistories_AspNetUsers_RequestingEmployeeId",
                table: "LeaveHistories");

            migrationBuilder.DropIndex(
                name: "IX_LeaveHistories_RequestingEmployeeId",
                table: "LeaveHistories");

            migrationBuilder.AlterColumn<string>(
                name: "RequestingEmployeeId",
                table: "LeaveHistories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeId",
                table: "LeaveHistories",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LeaveHistories_EmployeeId",
                table: "LeaveHistories",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveHistories_AspNetUsers_EmployeeId",
                table: "LeaveHistories",
                column: "EmployeeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
