using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddPOAndEOHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployerOperationHistories",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "قیمت واحد"),
                    Workload = table.Column<decimal>(type: "decimal(18,5)", nullable: false, comment: "حجم کار"),
                    Description = table.Column<string>(type: "nvarchar(1500)", nullable: true, comment: "توضیحات"),
                    EmployerOperationId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "تاریخ ایجاد"),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false, comment: "ایجاد کننده"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "تاریخ ویرایش"),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true, comment: "ویرایش کننده"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "حذف شدگی")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployerOperationHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployerOperationHistories_EmployerOperations_EmployerOperationId",
                        column: x => x.EmployerOperationId,
                        principalSchema: "engineer",
                        principalTable: "EmployerOperations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProjectOperationHistories",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "هزینه"),
                    Workload = table.Column<decimal>(type: "decimal(18,5)", nullable: false, comment: "حجم شرح عملیات"),
                    Description = table.Column<string>(type: "nvarchar(1500)", nullable: true, comment: "توضیحات"),
                    ProjectOperationId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "تاریخ ایجاد"),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false, comment: "ایجاد کننده"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "تاریخ ویرایش"),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true, comment: "ویرایش کننده"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "حذف شدگی")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectOperationHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectOperationHistories_ProjectOperations_ProjectOperationId",
                        column: x => x.ProjectOperationId,
                        principalSchema: "engineer",
                        principalTable: "ProjectOperations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployerOperationHistories_EmployerOperationId",
                schema: "engineer",
                table: "EmployerOperationHistories",
                column: "EmployerOperationId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectOperationHistories_ProjectOperationId",
                schema: "engineer",
                table: "ProjectOperationHistories",
                column: "ProjectOperationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployerOperationHistories",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "ProjectOperationHistories",
                schema: "engineer");
        }
    }
}
