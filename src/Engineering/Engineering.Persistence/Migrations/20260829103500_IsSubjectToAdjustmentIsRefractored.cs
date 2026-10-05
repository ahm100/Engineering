using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class IsSubjectToAdjustmentIsRefractored : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSubjectToAdjustment",
                schema: "engineer",
                table: "ContractTypes");

            migrationBuilder.CreateTable(
                name: "ContractTypeDetailAdjustments",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    ContractTypeDetailId = table.Column<long>(type: "bigint", nullable: false, comment: "جزئیات نوع قرارداد"),
                    Type = table.Column<int>(type: "int", nullable: false, comment: "نوع تعدیل جزئیات نوع قرارداد"),
                    PriceIndexBaseYear = table.Column<int>(type: "int", nullable: true, comment: "سال مبنای شاخص تعدیل"),
                    PriceIndexBasePeriod = table.Column<int>(type: "int", nullable: true, comment: "دوره مبنای شاخص تعدیل"),
                    PriceIndexReference = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true, comment: "مرجع تعدیل"),
                    PriceIndex = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true, comment: "شاخص مربوطه"),
                    CurrencyBaseDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "تاریخ مبنای ارز"),
                    CurrencyBaseRate = table.Column<decimal>(type: "decimal(18,6)", nullable: true, comment: "نرخ ارز در تاریخ مبنا"),
                    CurrencyId = table.Column<long>(type: "bigint", nullable: true, comment: "ارز"),
                    CurrencyReferenceType = table.Column<int>(type: "int", nullable: true, comment: "نوع مرجع نرخ ارز"),
                    CurrencyCustomReference = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true, comment: "مرجع سفارشی نرخ ارز"),
                    OtherBasis = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true, comment: "مبنای تعدیل"),
                    OtherReference = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true, comment: "مرجع تعدیل"),
                    OtherIndex = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true, comment: "شاخص یا معیار تعدیل"),
                    Description = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: true, comment: "توضیحات"),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "تاریخ ایجاد"),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false, comment: "ایجاد کننده"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "تاریخ ویرایش"),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true, comment: "ویرایش کننده"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "حذف شدگی")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractTypeDetailAdjustments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractTypeDetailAdjustments_ContractTypeDetails_ContractTypeDetailId",
                        column: x => x.ContractTypeDetailId,
                        principalSchema: "engineer",
                        principalTable: "ContractTypeDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContractTypeDetailAdjustments_ContractTypeDetailId",
                schema: "engineer",
                table: "ContractTypeDetailAdjustments",
                column: "ContractTypeDetailId",
                unique: true,
                filter: "[IsDeleted] = 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContractTypeDetailAdjustments",
                schema: "engineer");

            migrationBuilder.AddColumn<bool>(
                name: "IsSubjectToAdjustment",
                schema: "engineer",
                table: "ContractTypes",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "مشمول تعدیل");
        }
    }
}
