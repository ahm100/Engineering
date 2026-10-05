using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddProjectDocumentHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                schema: "engineer",
                table: "ProjectDocs",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "engineer",
                table: "ProjectDocs",
                type: "nvarchar(1500)",
                maxLength: 1500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Revision",
                schema: "engineer",
                table: "ProjectDocs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Sequence",
                schema: "engineer",
                table: "ProjectDocs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                schema: "engineer",
                table: "ProjectDocs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "ThirdPartyId",
                schema: "engineer",
                table: "ProjectDocs",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                schema: "engineer",
                table: "ProjectDocs",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ProjectDocHistories",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<long>(type: "bigint", nullable: false),
                    DisciplineId = table.Column<long>(type: "bigint", nullable: false),
                    DisciplineDocId = table.Column<long>(type: "bigint", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: false),
                    ThirdPartyId = table.Column<long>(type: "bigint", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Revision = table.Column<int>(type: "int", nullable: false),
                    Sequence = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ProjectDocId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectDocHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectDocHistories_DisciplineDocs_DisciplineDocId",
                        column: x => x.DisciplineDocId,
                        principalSchema: "engineer",
                        principalTable: "DisciplineDocs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectDocHistories_Disciplines_DisciplineId",
                        column: x => x.DisciplineId,
                        principalSchema: "engineer",
                        principalTable: "Disciplines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectDocHistories_ProjectDocs_ProjectDocId",
                        column: x => x.ProjectDocId,
                        principalSchema: "engineer",
                        principalTable: "ProjectDocs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectDocHistories_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "engineer",
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectDocHistories_DisciplineDocId",
                schema: "engineer",
                table: "ProjectDocHistories",
                column: "DisciplineDocId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectDocHistories_DisciplineId",
                schema: "engineer",
                table: "ProjectDocHistories",
                column: "DisciplineId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectDocHistories_ProjectDocId",
                schema: "engineer",
                table: "ProjectDocHistories",
                column: "ProjectDocId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectDocHistories_ProjectId",
                schema: "engineer",
                table: "ProjectDocHistories",
                column: "ProjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectDocHistories",
                schema: "engineer");

            migrationBuilder.DropColumn(
                name: "Code",
                schema: "engineer",
                table: "ProjectDocs");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "engineer",
                table: "ProjectDocs");

            migrationBuilder.DropColumn(
                name: "Revision",
                schema: "engineer",
                table: "ProjectDocs");

            migrationBuilder.DropColumn(
                name: "Sequence",
                schema: "engineer",
                table: "ProjectDocs");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "engineer",
                table: "ProjectDocs");

            migrationBuilder.DropColumn(
                name: "ThirdPartyId",
                schema: "engineer",
                table: "ProjectDocs");

            migrationBuilder.DropColumn(
                name: "Url",
                schema: "engineer",
                table: "ProjectDocs");
        }
    }
}
