using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CreateEngineeringDocs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DisciplineDocs",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisciplineDocs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Disciplines",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: false),
                    EnglishName = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disciplines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DisciplineDocTypes",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DisciplineId = table.Column<long>(type: "bigint", nullable: false),
                    DisciplineDocId = table.Column<long>(type: "bigint", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisciplineDocTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DisciplineDocTypes_DisciplineDocs_DisciplineDocId",
                        column: x => x.DisciplineDocId,
                        principalSchema: "engineer",
                        principalTable: "DisciplineDocs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DisciplineDocTypes_Disciplines_DisciplineId",
                        column: x => x.DisciplineId,
                        principalSchema: "engineer",
                        principalTable: "Disciplines",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProjectDocs",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<long>(type: "bigint", nullable: false),
                    DisciplineId = table.Column<long>(type: "bigint", nullable: false),
                    DisciplineDocId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectDocs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectDocs_DisciplineDocs_DisciplineDocId",
                        column: x => x.DisciplineDocId,
                        principalSchema: "engineer",
                        principalTable: "DisciplineDocs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectDocs_Disciplines_DisciplineId",
                        column: x => x.DisciplineId,
                        principalSchema: "engineer",
                        principalTable: "Disciplines",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectDocs_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "engineer",
                        principalTable: "Projects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DisciplineDocTypes_DisciplineDocId",
                schema: "engineer",
                table: "DisciplineDocTypes",
                column: "DisciplineDocId");

            migrationBuilder.CreateIndex(
                name: "IX_DisciplineDocTypes_DisciplineId",
                schema: "engineer",
                table: "DisciplineDocTypes",
                column: "DisciplineId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectDocs_DisciplineDocId",
                schema: "engineer",
                table: "ProjectDocs",
                column: "DisciplineDocId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectDocs_DisciplineId",
                schema: "engineer",
                table: "ProjectDocs",
                column: "DisciplineId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectDocs_ProjectId",
                schema: "engineer",
                table: "ProjectDocs",
                column: "ProjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DisciplineDocTypes",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "ProjectDocs",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "DisciplineDocs",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "Disciplines",
                schema: "engineer");
        }
    }
}
