using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CreateProjectCostCenter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProjectCostCenters",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    ProjectId = table.Column<long>(type: "bigint", nullable: false),
                    CostCenterId = table.Column<long>(type: "bigint", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "تاریخ ایجاد"),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false, comment: "ایجاد کننده"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "تاریخ ویرایش"),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true, comment: "ویرایش کننده"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "حذف شدگی")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectCostCenters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectCostCenters_CostCenters_CostCenterId",
                        column: x => x.CostCenterId,
                        principalSchema: "engineer",
                        principalTable: "CostCenters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectCostCenters_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "engineer",
                        principalTable: "Projects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectCostCenters_CostCenterId",
                schema: "engineer",
                table: "ProjectCostCenters",
                column: "CostCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectCostCenters_ProjectId",
                schema: "engineer",
                table: "ProjectCostCenters",
                column: "ProjectId");

            migrationBuilder.Sql(@"
                INSERT INTO engineer.ProjectCostCenters
                (
                    ProjectId,
                    CostCenterId,
                    IsDefault,
                    Created,
                    CreatorId,
                    IsDeleted
                )
                SELECT
                p.Id,
                p.CostCenterId,
                1,
                GETDATE(),
                0,
                0
                FROM engineer.Projects p
                WHERE p.CostCenterId IS NOT NULL;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectCostCenters",
                schema: "engineer");
        }
    }
}