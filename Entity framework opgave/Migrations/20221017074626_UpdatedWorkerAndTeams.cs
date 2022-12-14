using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entity_framework_opgave.Migrations
{
    public partial class UpdatedWorkerAndTeams : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    TeamID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    TeamID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    CurrentTaskID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.TeamID);
                    table.ForeignKey(
                        name: "FK_Teams_Tasks_CurrentTaskID",
                        column: x => x.CurrentTaskID,
                        principalTable: "Tasks",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TeamWorkers",
                columns: table => new
                {
                    TeamID = table.Column<int>(type: "INTEGER", nullable: false),
                    WorkerID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamWorkers", x => new { x.TeamID, x.WorkerID });
                    table.ForeignKey(
                        name: "FK_TeamWorkers_Teams_TeamID",
                        column: x => x.TeamID,
                        principalTable: "Teams",
                        principalColumn: "TeamID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Todos",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Complete = table.Column<bool>(type: "INTEGER", nullable: false),
                    TaskID = table.Column<int>(type: "INTEGER", nullable: true),
                    WorkerID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Todos", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Todos_Tasks_TaskID",
                        column: x => x.TaskID,
                        principalTable: "Tasks",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Workers",
                columns: table => new
                {
                    WorkerID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    CurrentTodoID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workers", x => x.WorkerID);
                    table.ForeignKey(
                        name: "FK_Workers_Todos_CurrentTodoID",
                        column: x => x.CurrentTodoID,
                        principalTable: "Todos",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_TeamID",
                table: "Tasks",
                column: "TeamID");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_CurrentTaskID",
                table: "Teams",
                column: "CurrentTaskID");

            migrationBuilder.CreateIndex(
                name: "IX_TeamWorkers_WorkerID",
                table: "TeamWorkers",
                column: "WorkerID");

            migrationBuilder.CreateIndex(
                name: "IX_Todos_TaskID",
                table: "Todos",
                column: "TaskID");

            migrationBuilder.CreateIndex(
                name: "IX_Todos_WorkerID",
                table: "Todos",
                column: "WorkerID");

            migrationBuilder.CreateIndex(
                name: "IX_Workers_CurrentTodoID",
                table: "Workers",
                column: "CurrentTodoID");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Teams_TeamID",
                table: "Tasks",
                column: "TeamID",
                principalTable: "Teams",
                principalColumn: "TeamID");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamWorkers_Workers_WorkerID",
                table: "TeamWorkers",
                column: "WorkerID",
                principalTable: "Workers",
                principalColumn: "WorkerID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Todos_Workers_WorkerID",
                table: "Todos",
                column: "WorkerID",
                principalTable: "Workers",
                principalColumn: "WorkerID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Teams_TeamID",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Todos_Tasks_TaskID",
                table: "Todos");

            migrationBuilder.DropForeignKey(
                name: "FK_Todos_Workers_WorkerID",
                table: "Todos");

            migrationBuilder.DropTable(
                name: "TeamWorkers");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Workers");

            migrationBuilder.DropTable(
                name: "Todos");
        }
    }
}
