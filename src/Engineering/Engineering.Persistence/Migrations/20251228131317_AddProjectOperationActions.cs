using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddProjectOperationActions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProjectOperationActions",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    OperationInfoActionId = table.Column<long>(type: "bigint", nullable: false),
                    ProjectOperationId = table.Column<long>(type: "bigint", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true, defaultValue: 0m, comment: "هزینه"),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "تاریخ ایجاد"),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false, comment: "ایجاد کننده"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "تاریخ ویرایش"),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true, comment: "ویرایش کننده"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "حذف شدگی")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectOperationActions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectOperationActions_OperationInfoActions_OperationInfoActionId",
                        column: x => x.OperationInfoActionId,
                        principalSchema: "engineer",
                        principalTable: "OperationInfoActions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectOperationActions_ProjectOperations_ProjectOperationId",
                        column: x => x.ProjectOperationId,
                        principalSchema: "engineer",
                        principalTable: "ProjectOperations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectOperationActions_OperationInfoActionId",
                schema: "engineer",
                table: "ProjectOperationActions",
                column: "OperationInfoActionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectOperationActions_ProjectOperationId",
                schema: "engineer",
                table: "ProjectOperationActions",
                column: "ProjectOperationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectOperationActions",
                schema: "engineer");
        }
    }
}
