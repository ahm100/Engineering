using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CreateProjectWbs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProjectWbs",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false, comment: "عنوان"),
                    Code = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "کد"),
                    Description = table.Column<string>(type: "nvarchar(1500)", nullable: true, comment: "توضیحات"),
                    ProjectId = table.Column<long>(type: "bigint", nullable: false),
                    WbsTemplateId = table.Column<long>(type: "bigint", nullable: false),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
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
                    table.PrimaryKey("PK_ProjectWbs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectWbs_ProjectWbs_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "engineer",
                        principalTable: "ProjectWbs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectWbs_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "engineer",
                        principalTable: "Projects",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectWbs_WbsTemplates_WbsTemplateId",
                        column: x => x.WbsTemplateId,
                        principalSchema: "engineer",
                        principalTable: "WbsTemplates",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectWbs_ParentId",
                schema: "engineer",
                table: "ProjectWbs",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectWbs_ProjectId",
                schema: "engineer",
                table: "ProjectWbs",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectWbs_WbsTemplateId",
                schema: "engineer",
                table: "ProjectWbs",
                column: "WbsTemplateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectWbs",
                schema: "engineer");
        }
    }
}
