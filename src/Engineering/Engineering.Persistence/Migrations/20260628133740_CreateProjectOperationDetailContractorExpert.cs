using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CreateProjectOperationDetailContractorExpert : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProjectOperationDetailContractorExperts",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    Volume = table.Column<long>(type: "bigint", nullable: false),
                    HaveContract = table.Column<bool>(type: "bit", nullable: false),
                    ConsumableVolumeExpertId = table.Column<long>(type: "bigint", nullable: false),
                    ProjectOperationDetailContractorServiceId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "تاریخ ایجاد"),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false, comment: "ایجاد کننده"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "تاریخ ویرایش"),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true, comment: "ویرایش کننده"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "حذف شدگی"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, comment: "وضعیت فعال یا غیر فعال بودن")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectOperationDetailContractorExperts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectOperationDetailContractorExperts_ProjectOperationDetailConsumableVolumeExperts_ConsumableVolumeExpertId",
                        column: x => x.ConsumableVolumeExpertId,
                        principalSchema: "engineer",
                        principalTable: "ProjectOperationDetailConsumableVolumeExperts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectOperationDetailContractorExperts_ProjectOperationDetailContractorServices_ProjectOperationDetailContractorServiceId",
                        column: x => x.ProjectOperationDetailContractorServiceId,
                        principalSchema: "engineer",
                        principalTable: "ProjectOperationDetailContractorServices",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectOperationDetailContractorExperts_ConsumableVolumeExpertId",
                schema: "engineer",
                table: "ProjectOperationDetailContractorExperts",
                column: "ConsumableVolumeExpertId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectOperationDetailContractorExperts_ProjectOperationDetailContractorServiceId",
                schema: "engineer",
                table: "ProjectOperationDetailContractorExperts",
                column: "ProjectOperationDetailContractorServiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectOperationDetailContractorExperts",
                schema: "engineer");
        }
    }
}
