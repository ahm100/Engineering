using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddDocumentsInESS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployerStatusStatementDailyDocuments",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    Url = table.Column<string>(type: "nvarchar(1500)", nullable: false),
                    EmployerStatusStatementProjectOperationDetailDailyId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployerStatusStatementDailyDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployerStatusStatementDailyDocuments_EmployerStatusStatementProjectOperationDetailDailies_EmployerStatusStatementProjectOpe~",
                        column: x => x.EmployerStatusStatementProjectOperationDetailDailyId,
                        principalSchema: "engineer",
                        principalTable: "EmployerStatusStatementProjectOperationDetailDailies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployerStatusStatementDocuments",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    Url = table.Column<string>(type: "nvarchar(1500)", nullable: false),
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
                    table.PrimaryKey("PK_EmployerStatusStatementDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployerStatusStatementDocuments_EmployerStatusStatements_EmployerStatusStatementId",
                        column: x => x.EmployerStatusStatementId,
                        principalSchema: "engineer",
                        principalTable: "EmployerStatusStatements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployerStatusStatementProjectOperationDocuments",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    Url = table.Column<string>(type: "nvarchar(1500)", nullable: false),
                    EmployerStatusStatementProjectOperationId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployerStatusStatementProjectOperationDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployerStatusStatementProjectOperationDocuments_EmployerStatusStatementProjectOperations_EmployerStatusStatementProjectOper~",
                        column: x => x.EmployerStatusStatementProjectOperationId,
                        principalSchema: "engineer",
                        principalTable: "EmployerStatusStatementProjectOperations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployerStatusStatementDailyDocuments_EmployerStatusStatementProjectOperationDetailDailyId",
                schema: "engineer",
                table: "EmployerStatusStatementDailyDocuments",
                column: "EmployerStatusStatementProjectOperationDetailDailyId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployerStatusStatementDocuments_EmployerStatusStatementId",
                schema: "engineer",
                table: "EmployerStatusStatementDocuments",
                column: "EmployerStatusStatementId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployerStatusStatementProjectOperationDocuments_EmployerStatusStatementProjectOperationId",
                schema: "engineer",
                table: "EmployerStatusStatementProjectOperationDocuments",
                column: "EmployerStatusStatementProjectOperationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployerStatusStatementDailyDocuments",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "EmployerStatusStatementDocuments",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "EmployerStatusStatementProjectOperationDocuments",
                schema: "engineer");
        }
    }
}
