using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class deleteProjectOperationDetailComment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectOperationDetailCommentDocuments",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "ProjectOperationDetailCmts",
                schema: "engineer");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                schema: "engineer",
                table: "ProjectScheduleColumns",
                type: "bit",
                nullable: false,
                comment: "وضعیت فعال یا غیر فعال بودن",
                oldClrType: typeof(bool),
                oldType: "bit");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                schema: "engineer",
                table: "ProjectScheduleColumns",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "وضعیت فعال یا غیر فعال بودن");

            migrationBuilder.CreateTable(
                name: "ProjectOperationDetailCmts",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    ProjectOperationDetailId = table.Column<long>(type: "bigint", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(1500)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectOperationDetailCmts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectOperationDetailCmts_ProjectOperationDetailCmts_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "engineer",
                        principalTable: "ProjectOperationDetailCmts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectOperationDetailCmts_ProjectOperationDetails_ProjectOperationDetailId",
                        column: x => x.ProjectOperationDetailId,
                        principalSchema: "engineer",
                        principalTable: "ProjectOperationDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectOperationDetailCommentDocuments",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    ProjectOperationDetailCommentId = table.Column<long>(type: "bigint", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(1500)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectOperationDetailCommentDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectOperationDetailCommentDocuments_ProjectOperationDetailCmts_ProjectOperationDetailCommentId",
                        column: x => x.ProjectOperationDetailCommentId,
                        principalSchema: "engineer",
                        principalTable: "ProjectOperationDetailCmts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectOperationDetailCmts_ParentId",
                schema: "engineer",
                table: "ProjectOperationDetailCmts",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectOperationDetailCmts_ProjectOperationDetailId",
                schema: "engineer",
                table: "ProjectOperationDetailCmts",
                column: "ProjectOperationDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectOperationDetailCommentDocuments_ProjectOperationDetailCommentId",
                schema: "engineer",
                table: "ProjectOperationDetailCommentDocuments",
                column: "ProjectOperationDetailCommentId");
        }
    }
}
