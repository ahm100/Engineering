using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CreateCostCenterHistoryEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ProjectTypeId",
                schema: "engineer",
                table: "Projects",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<string>(
                name: "OperationInfoName",
                schema: "engineer",
                table: "OperationInfos",
                type: "nvarchar(max)",
                nullable: false,
                comment: "نام شرح عملیات",
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldComment: "نام شرح عملیات");

            migrationBuilder.CreateTable(
                name: "CostCenterHistories",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    CostCenterCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "کد مرکز هزینه"),
                    CostCenterName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false, comment: "نام مرکز هزینه"),
                    NoOperationDays = table.Column<int>(type: "int", nullable: true, comment: "روزهای بدون پروژه"),
                    CityId = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه شهر"),
                    Address = table.Column<string>(type: "nvarchar(1500)", nullable: false, comment: "آدرس مرکز هزینه"),
                    PostalCode = table.Column<string>(type: "nvarchar(10)", nullable: true, comment: "کد پستی"),
                    Latitude = table.Column<decimal>(type: "decimal(18,9)", nullable: false, comment: "عرض جغرافیایی"),
                    Longitude = table.Column<decimal>(type: "decimal(18,9)", nullable: false, comment: "طول جغرافیایی"),
                    Description = table.Column<string>(type: "nvarchar(1500)", nullable: true, comment: "توضیحات"),
                    WeatherState = table.Column<bool>(type: "bit", nullable: true, defaultValue: false, comment: "وضعیت آب و هوا"),
                    CompanyId = table.Column<long>(type: "bigint", nullable: true, comment: "شناسه کمپانی"),
                    PreferentialReferenceCode = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "کد مرجع تفصیلی"),
                    CostCenterTypesId = table.Column<long>(type: "bigint", nullable: false),
                    CostCenterId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_CostCenterHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CostCenterHistories_CostCenterTypes_CostCenterTypesId",
                        column: x => x.CostCenterTypesId,
                        principalSchema: "engineer",
                        principalTable: "CostCenterTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CostCenterHistories_CostCenters_CostCenterTypesId",
                        column: x => x.CostCenterTypesId,
                        principalSchema: "engineer",
                        principalTable: "CostCenters",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CostCenterHistories_CostCenterTypesId",
                schema: "engineer",
                table: "CostCenterHistories",
                column: "CostCenterTypesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CostCenterHistories",
                schema: "engineer");

            migrationBuilder.DropColumn(
                name: "ProjectTypeId",
                schema: "engineer",
                table: "Projects");

            migrationBuilder.AlterColumn<string>(
                name: "OperationInfoName",
                schema: "engineer",
                table: "OperationInfos",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                comment: "نام شرح عملیات",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "نام شرح عملیات");
        }
    }
}
