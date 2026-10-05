using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddESSHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployerStatusStatementHistories",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    StatusStatementCode = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PercentageOfWorkDone = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CalculatedAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StatusStatementVolume = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1500)", nullable: true),
                    EmployerStatusStatementId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployerStatusStatementHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployerStatusStatementHistories_EmployerStatusStatements_EmployerStatusStatementId",
                        column: x => x.EmployerStatusStatementId,
                        principalSchema: "engineer",
                        principalTable: "EmployerStatusStatements",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployerStatusStatementHistories_EmployerStatusStatementId",
                schema: "engineer",
                table: "EmployerStatusStatementHistories",
                column: "EmployerStatusStatementId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployerStatusStatementHistories",
                schema: "engineer");
        }
    }
}
