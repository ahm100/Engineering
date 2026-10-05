using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CreateProjectOperationWbs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectWbs_ProjectWbs_ParentId",
                schema: "engineer",
                table: "ProjectWbs");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectWbs_Projects_ProjectId",
                schema: "engineer",
                table: "ProjectWbs");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectWbs_WbsTemplates_WbsTemplateId",
                schema: "engineer",
                table: "ProjectWbs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectWbs",
                schema: "engineer",
                table: "ProjectWbs");

            migrationBuilder.RenameTable(
                name: "ProjectWbs",
                schema: "engineer",
                newName: "ProjectWbses",
                newSchema: "engineer");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectWbs_WbsTemplateId",
                schema: "engineer",
                table: "ProjectWbses",
                newName: "IX_ProjectWbses_WbsTemplateId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectWbs_ProjectId",
                schema: "engineer",
                table: "ProjectWbses",
                newName: "IX_ProjectWbses_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectWbs_ParentId",
                schema: "engineer",
                table: "ProjectWbses",
                newName: "IX_ProjectWbses_ParentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectWbses",
                schema: "engineer",
                table: "ProjectWbses",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ProjectOperationWbses",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    ProjectOperationId = table.Column<long>(type: "bigint", nullable: false),
                    ProjectWbsId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "تاریخ ایجاد"),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false, comment: "ایجاد کننده"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "تاریخ ویرایش"),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true, comment: "ویرایش کننده"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "حذف شدگی"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectOperationWbses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectOperationWbses_ProjectOperations_ProjectOperationId",
                        column: x => x.ProjectOperationId,
                        principalSchema: "engineer",
                        principalTable: "ProjectOperations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectOperationWbses_ProjectWbses_ProjectWbsId",
                        column: x => x.ProjectWbsId,
                        principalSchema: "engineer",
                        principalTable: "ProjectWbses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectOperationWbses_ProjectOperationId",
                schema: "engineer",
                table: "ProjectOperationWbses",
                column: "ProjectOperationId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectOperationWbses_ProjectWbsId",
                schema: "engineer",
                table: "ProjectOperationWbses",
                column: "ProjectWbsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectWbses_ProjectWbses_ParentId",
                schema: "engineer",
                table: "ProjectWbses",
                column: "ParentId",
                principalSchema: "engineer",
                principalTable: "ProjectWbses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectWbses_Projects_ProjectId",
                schema: "engineer",
                table: "ProjectWbses",
                column: "ProjectId",
                principalSchema: "engineer",
                principalTable: "Projects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectWbses_WbsTemplates_WbsTemplateId",
                schema: "engineer",
                table: "ProjectWbses",
                column: "WbsTemplateId",
                principalSchema: "engineer",
                principalTable: "WbsTemplates",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectWbses_ProjectWbses_ParentId",
                schema: "engineer",
                table: "ProjectWbses");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectWbses_Projects_ProjectId",
                schema: "engineer",
                table: "ProjectWbses");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectWbses_WbsTemplates_WbsTemplateId",
                schema: "engineer",
                table: "ProjectWbses");

            migrationBuilder.DropTable(
                name: "ProjectOperationWbses",
                schema: "engineer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectWbses",
                schema: "engineer",
                table: "ProjectWbses");

            migrationBuilder.RenameTable(
                name: "ProjectWbses",
                schema: "engineer",
                newName: "ProjectWbs",
                newSchema: "engineer");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectWbses_WbsTemplateId",
                schema: "engineer",
                table: "ProjectWbs",
                newName: "IX_ProjectWbs_WbsTemplateId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectWbses_ProjectId",
                schema: "engineer",
                table: "ProjectWbs",
                newName: "IX_ProjectWbs_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectWbses_ParentId",
                schema: "engineer",
                table: "ProjectWbs",
                newName: "IX_ProjectWbs_ParentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectWbs",
                schema: "engineer",
                table: "ProjectWbs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectWbs_ProjectWbs_ParentId",
                schema: "engineer",
                table: "ProjectWbs",
                column: "ParentId",
                principalSchema: "engineer",
                principalTable: "ProjectWbs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectWbs_Projects_ProjectId",
                schema: "engineer",
                table: "ProjectWbs",
                column: "ProjectId",
                principalSchema: "engineer",
                principalTable: "Projects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectWbs_WbsTemplates_WbsTemplateId",
                schema: "engineer",
                table: "ProjectWbs",
                column: "WbsTemplateId",
                principalSchema: "engineer",
                principalTable: "WbsTemplates",
                principalColumn: "Id");
        }
    }
}
