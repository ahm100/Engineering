using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class deleteoldprojectdocuments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectDocumentFiles",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "ProjectDocuments",
                schema: "engineer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProjectDocuments",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    ProjectId = table.Column<long>(type: "bigint", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "تاریخ ایجاد"),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false, comment: "ایجاد کننده"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "تاریخ"),
                    Description = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: false, comment: "توضیحات"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "حذف شدگی"),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false, defaultValue: 999, comment: "نوع اسناد پروژه"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "تاریخ ویرایش"),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true, comment: "ویرایش کننده")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectDocuments_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "engineer",
                        principalTable: "Projects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProjectDocumentFiles",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    ProjectDocumentId = table.Column<long>(type: "bigint", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "تاریخ ایجاد"),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false, comment: "ایجاد کننده"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "حذف شدگی"),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "تاریخ ویرایش"),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true, comment: "ویرایش کننده"),
                    Url = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: false, comment: "مسیر فایل")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectDocumentFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectDocumentFiles_ProjectDocuments_ProjectDocumentId",
                        column: x => x.ProjectDocumentId,
                        principalSchema: "engineer",
                        principalTable: "ProjectDocuments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectDocumentFiles_ProjectDocumentId",
                schema: "engineer",
                table: "ProjectDocumentFiles",
                column: "ProjectDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectDocuments_ProjectId",
                schema: "engineer",
                table: "ProjectDocuments",
                column: "ProjectId");
        }
    }
}
