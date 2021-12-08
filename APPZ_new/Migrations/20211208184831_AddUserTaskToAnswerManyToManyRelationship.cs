using Microsoft.EntityFrameworkCore.Migrations;

namespace APPZ_new.Migrations
{
    public partial class AddUserTaskToAnswerManyToManyRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnswerUserTask",
                columns: table => new
                {
                    UserAnswersId = table.Column<int>(type: "int", nullable: false),
                    UserTasksId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerUserTask", x => new { x.UserAnswersId, x.UserTasksId });
                    table.ForeignKey(
                        name: "FK_AnswerUserTask_Answers_UserAnswersId",
                        column: x => x.UserAnswersId,
                        principalTable: "Answers",
                        principalColumn: "AnswerId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_AnswerUserTask_UserTasks_UserTasksId",
                        column: x => x.UserTasksId,
                        principalTable: "UserTasks",
                        principalColumn: "UserTaskId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnswerUserTask_UserTasksId",
                table: "AnswerUserTask",
                column: "UserTasksId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnswerUserTask");
        }
    }
}
