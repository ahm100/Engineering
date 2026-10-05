using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class EOServiceHistoryANdEoProductAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "OtherCost",
                schema: "engineer",
                table: "EmployerOperationServices",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                comment: "هزینه  سایر هزینه ها");

            migrationBuilder.AddColumn<decimal>(
                name: "OtherCostPercent",
                schema: "engineer",
                table: "EmployerOperationServices",
                type: "decimal(5,2)",
                nullable: false,
                defaultValue: 0m,
                comment: "درصد سایر هزینه ها");

            migrationBuilder.AddColumn<decimal>(
                name: "ProfitCost",
                schema: "engineer",
                table: "EmployerOperationServices",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                comment: "هزینه سود");

            migrationBuilder.AddColumn<decimal>(
                name: "ProfitCostPercent",
                schema: "engineer",
                table: "EmployerOperationServices",
                type: "decimal(5,2)",
                nullable: false,
                defaultValue: 0m,
                comment: "درصد سود");

            migrationBuilder.AddColumn<decimal>(
                name: "OtherCost",
                schema: "engineer",
                table: "EmployerOperationProducts",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                comment: "هزینه  سایر هزینه ها");

            migrationBuilder.AddColumn<decimal>(
                name: "OtherCostPercent",
                schema: "engineer",
                table: "EmployerOperationProducts",
                type: "decimal(5,2)",
                nullable: false,
                defaultValue: 0m,
                comment: "درصد سایر هزینه ها");

            migrationBuilder.AddColumn<decimal>(
                name: "ProfitCost",
                schema: "engineer",
                table: "EmployerOperationProducts",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                comment: "هزینه سود");

            migrationBuilder.AddColumn<decimal>(
                name: "ProfitCostPercent",
                schema: "engineer",
                table: "EmployerOperationProducts",
                type: "decimal(5,2)",
                nullable: false,
                defaultValue: 0m,
                comment: "درصد سود");

            migrationBuilder.CreateTable(
                name: "EmployerOperationProductHistories",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    ProductGroupId = table.Column<long>(type: "bigint", nullable: false),
                    MinPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m, comment: "حداقل قیمت"),
                    MaxPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "حداکثر قیمت"),
                    Tax = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "مالیات"),
                    TaxPercent = table.Column<decimal>(type: "decimal(5,2)", nullable: false, comment: "درصد مالیات"),
                    TransportationCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "هزینه حمل و نقل"),
                    TransportationCostPercent = table.Column<decimal>(type: "decimal(5,2)", nullable: false, comment: "درصد هزینه حمل و نقل"),
                    ProfitCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "هزینه سود"),
                    ProfitCostPercent = table.Column<decimal>(type: "decimal(5,2)", nullable: false, comment: "درصد سود"),
                    OtherCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "هزینه  سایر هزینه ها"),
                    OtherCostPercent = table.Column<decimal>(type: "decimal(5,2)", nullable: false, comment: "درصد سایر هزینه ها"),
                    IsStandard = table.Column<bool>(type: "bit", nullable: false, defaultValue: true, comment: "استاندارد بودن کالا"),
                    Description = table.Column<string>(type: "nvarchar(1500)", nullable: true, comment: "توضیحات"),
                    EmployerOperationProductId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "تاریخ ایجاد"),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false, comment: "ایجاد کننده"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "تاریخ ویرایش"),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true, comment: "ویرایش کننده"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "حذف شدگی")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployerOperationProductHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployerOperationProductHistories_EmployerOperationProducts_EmployerOperationProductId",
                        column: x => x.EmployerOperationProductId,
                        principalSchema: "engineer",
                        principalTable: "EmployerOperationProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployerOperationServicesHistories",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    MinPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m, comment: "حداقل قیمت"),
                    MaxPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "حداکثر قیمت"),
                    Tax = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "مالیات"),
                    TaxPercent = table.Column<decimal>(type: "decimal(5,2)", nullable: false, comment: "درصد مالیات"),
                    TransportationCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "هزینه حمل و نقل"),
                    TransportationCostPercent = table.Column<decimal>(type: "decimal(5,2)", nullable: false, comment: "درصد هزینه حمل و نقل"),
                    ProfitCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "هزینه سود"),
                    ProfitCostPercent = table.Column<decimal>(type: "decimal(5,2)", nullable: false, comment: "درصد سود"),
                    OtherCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "هزینه  سایر هزینه ها"),
                    OtherCostPercent = table.Column<decimal>(type: "decimal(5,2)", nullable: false, comment: "درصد سایر هزینه ها"),
                    IsStandard = table.Column<bool>(type: "bit", nullable: false, defaultValue: true, comment: "استاندارد بودن کالا"),
                    Description = table.Column<string>(type: "nvarchar(1500)", nullable: true, comment: "توضیحات"),
                    EmployerOperationServiceId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "تاریخ ایجاد"),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false, comment: "ایجاد کننده"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "تاریخ ویرایش"),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true, comment: "ویرایش کننده"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "حذف شدگی")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployerOperationServicesHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployerOperationServicesHistories_EmployerOperationServices_EmployerOperationServiceId",
                        column: x => x.EmployerOperationServiceId,
                        principalSchema: "engineer",
                        principalTable: "EmployerOperationServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployerOperationProductHistories_EmployerOperationProductId",
                schema: "engineer",
                table: "EmployerOperationProductHistories",
                column: "EmployerOperationProductId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployerOperationServicesHistories_EmployerOperationServiceId",
                schema: "engineer",
                table: "EmployerOperationServicesHistories",
                column: "EmployerOperationServiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployerOperationProductHistories",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "EmployerOperationServicesHistories",
                schema: "engineer");

            migrationBuilder.DropColumn(
                name: "OtherCost",
                schema: "engineer",
                table: "EmployerOperationServices");

            migrationBuilder.DropColumn(
                name: "OtherCostPercent",
                schema: "engineer",
                table: "EmployerOperationServices");

            migrationBuilder.DropColumn(
                name: "ProfitCost",
                schema: "engineer",
                table: "EmployerOperationServices");

            migrationBuilder.DropColumn(
                name: "ProfitCostPercent",
                schema: "engineer",
                table: "EmployerOperationServices");

            migrationBuilder.DropColumn(
                name: "OtherCost",
                schema: "engineer",
                table: "EmployerOperationProducts");

            migrationBuilder.DropColumn(
                name: "OtherCostPercent",
                schema: "engineer",
                table: "EmployerOperationProducts");

            migrationBuilder.DropColumn(
                name: "ProfitCost",
                schema: "engineer",
                table: "EmployerOperationProducts");

            migrationBuilder.DropColumn(
                name: "ProfitCostPercent",
                schema: "engineer",
                table: "EmployerOperationProducts");
        }
    }
}
