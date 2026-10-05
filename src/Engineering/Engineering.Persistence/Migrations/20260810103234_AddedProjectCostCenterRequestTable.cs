using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedProjectCostCenterRequestTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProjectCostCenterRequest",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<long>(type: "bigint", nullable: false),
                    CostCenterId = table.Column<long>(type: "bigint", nullable: true, comment: "شناسه مرکز هزینه"),
                    RequestedCostCenterName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false, comment: "نام مرکز هزینه درخواستی"),
                    Description = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: true, comment: "توضیحات"),
                    Status = table.Column<int>(type: "int", nullable: false, comment: "وضعیت"),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectCostCenterRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectCostCenterRequest_CostCenters_CostCenterId",
                        column: x => x.CostCenterId,
                        principalSchema: "engineer",
                        principalTable: "CostCenters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectCostCenterRequest_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "engineer",
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectCostCenterRequest_CostCenterId",
                schema: "engineer",
                table: "ProjectCostCenterRequest",
                column: "CostCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectCostCenterRequest_ProjectId",
                schema: "engineer",
                table: "ProjectCostCenterRequest",
                column: "ProjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectCostCenterRequest",
                schema: "engineer");
        }
    }
}
