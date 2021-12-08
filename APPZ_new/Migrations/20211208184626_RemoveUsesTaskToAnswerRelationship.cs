using Microsoft.EntityFrameworkCore.Migrations;

namespace APPZ_new.Migrations
{
    public partial class RemoveUsesTaskToAnswerRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_UserTasks_UserTaskId",
                table: "Answers");

            migrationBuilder.DropIndex(
                name: "IX_Answers_UserTaskId",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "UserTaskId",
                table: "Answers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserTaskId",
                table: "Answers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Answers_UserTaskId",
                table: "Answers",
                column: "UserTaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_UserTasks_UserTaskId",
                table: "Answers",
                column: "UserTaskId",
                principalTable: "UserTasks",
                principalColumn: "UserTaskId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
