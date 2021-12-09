using Microsoft.EntityFrameworkCore.Migrations;

namespace APPZ_new.Migrations
{
    public partial class AddSqlTasks2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SqlAnswerSqlUserTask",
                columns: table => new
                {
                    SqlAnswersId = table.Column<int>(type: "int", nullable: false),
                    UserTasksId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SqlAnswerSqlUserTask", x => new { x.SqlAnswersId, x.UserTasksId });
                    table.ForeignKey(
                        name: "FK_SqlAnswerSqlUserTask_SqlAnswers_SqlAnswersId",
                        column: x => x.SqlAnswersId,
                        principalTable: "SqlAnswers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_SqlAnswerSqlUserTask_SqlUserTasks_UserTasksId",
                        column: x => x.UserTasksId,
                        principalTable: "SqlUserTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SqlAnswerSqlUserTask_UserTasksId",
                table: "SqlAnswerSqlUserTask",
                column: "UserTasksId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SqlAnswerSqlUserTask");
        }
    }
}
