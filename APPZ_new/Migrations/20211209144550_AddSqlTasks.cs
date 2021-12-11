using Microsoft.EntityFrameworkCore.Migrations;

namespace APPZ_new.Migrations
{
    public partial class AddSqlTasks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SqlTasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Scope = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Severity = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SqlTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SqlTasks_Categorys_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categorys",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SqlAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SortValue = table.Column<int>(type: "int", nullable: false),
                    IsUnUsed = table.Column<bool>(type: "bit", nullable: false),
                    SqlTaskId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SqlAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SqlAnswers_SqlTasks_SqlTaskId",
                        column: x => x.SqlTaskId,
                        principalTable: "SqlTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SqlUserTasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    TaskId = table.Column<int>(type: "int", nullable: false),
                    Mark = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SqlUserTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SqlUserTasks_SqlTasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "SqlTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SqlUserTasks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SqlAnswers_SqlTaskId",
                table: "SqlAnswers",
                column: "SqlTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_SqlTasks_CategoryId",
                table: "SqlTasks",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SqlUserTasks_TaskId",
                table: "SqlUserTasks",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_SqlUserTasks_UserId",
                table: "SqlUserTasks",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SqlAnswers");

            migrationBuilder.DropTable(
                name: "SqlUserTasks");

            migrationBuilder.DropTable(
                name: "SqlTasks");
        }
    }
}
