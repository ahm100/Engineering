using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ContractTypeDetailAdjustment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceIndex",
                schema: "engineer",
                table: "ContractTypeDetailAdjustments");

            migrationBuilder.DropColumn(
                name: "PriceIndexReference",
                schema: "engineer",
                table: "ContractTypeDetailAdjustments");

            migrationBuilder.AddColumn<long>(
                name: "PriceIndexId",
                schema: "engineer",
                table: "ContractTypeDetailAdjustments",
                type: "bigint",
                nullable: true,
                comment: "شناسه شاخص تعدیل");

            migrationBuilder.CreateTable(
                name: "ContractAdjustmentReferences",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    FaTitle = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false, comment: "عنوان فارسی"),
                    EnTitle = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false, comment: "عنوان انگلیسی"),
                    Description = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: true, comment: "توضیحات"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, comment: "وضعیت فعال یا غیر فعال بودن"),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "تاریخ ایجاد"),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false, comment: "ایجاد کننده"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "تاریخ ویرایش"),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true, comment: "ویرایش کننده"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "حذف شدگی")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractAdjustmentReferences", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContractAdjustmentIndexes",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    ContractAdjustmentReferenceId = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه مرجع شاخص تعدیل"),
                    FaTitle = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false, comment: "عنوان فارسی"),
                    EnTitle = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false, comment: "عنوان انگلیسی"),
                    Description = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: true, comment: "توضیحات"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, comment: "وضعیت فعال یا غیر فعال بودن"),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "تاریخ ایجاد"),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false, comment: "ایجاد کننده"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "تاریخ ویرایش"),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true, comment: "ویرایش کننده"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "حذف شدگی")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractAdjustmentIndexes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractAdjustmentIndexes_ContractAdjustmentReferences_ContractAdjustmentReferenceId",
                        column: x => x.ContractAdjustmentReferenceId,
                        principalSchema: "engineer",
                        principalTable: "ContractAdjustmentReferences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContractTypeDetailAdjustments_PriceIndexId",
                schema: "engineer",
                table: "ContractTypeDetailAdjustments",
                column: "PriceIndexId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractAdjustmentIndexes_ContractAdjustmentReferenceId",
                schema: "engineer",
                table: "ContractAdjustmentIndexes",
                column: "ContractAdjustmentReferenceId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractTypeDetailAdjustments_ContractAdjustmentIndexes_PriceIndexId",
                schema: "engineer",
                table: "ContractTypeDetailAdjustments",
                column: "PriceIndexId",
                principalSchema: "engineer",
                principalTable: "ContractAdjustmentIndexes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContractTypeDetailAdjustments_ContractAdjustmentIndexes_PriceIndexId",
                schema: "engineer",
                table: "ContractTypeDetailAdjustments");

            migrationBuilder.DropTable(
                name: "ContractAdjustmentIndexes",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "ContractAdjustmentReferences",
                schema: "engineer");

            migrationBuilder.DropIndex(
                name: "IX_ContractTypeDetailAdjustments_PriceIndexId",
                schema: "engineer",
                table: "ContractTypeDetailAdjustments");

            migrationBuilder.DropColumn(
                name: "PriceIndexId",
                schema: "engineer",
                table: "ContractTypeDetailAdjustments");

            migrationBuilder.AddColumn<string>(
                name: "PriceIndex",
                schema: "engineer",
                table: "ContractTypeDetailAdjustments",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                comment: "شاخص مربوطه");

            migrationBuilder.AddColumn<string>(
                name: "PriceIndexReference",
                schema: "engineer",
                table: "ContractTypeDetailAdjustments",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                comment: "مرجع تعدیل");
        }
    }
}
