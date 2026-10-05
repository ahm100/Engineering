using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddEOperationProductAndService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "EmployerOperationId",
                schema: "engineer",
                table: "EngineeringStandardProducts",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EmployerOperationProducts",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    ProductGroupId = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه گروه کالا"),
                    MinPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m, comment: "حداقل قیمت"),
                    MaxPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "حداکثر قیمت"),
                    Tax = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "مالیات"),
                    TaxPercent = table.Column<decimal>(type: "decimal(5,2)", nullable: false, comment: "درصد مالیات"),
                    TransportationCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "هزینه حمل و نقل"),
                    TransportationCostPercent = table.Column<decimal>(type: "decimal(5,2)", nullable: false, comment: "درصد هزینه حمل و نقل"),
                    IncreaseRate = table.Column<decimal>(type: "decimal(5,2)", nullable: false, comment: "ضریب افزایش"),
                    IsStandard = table.Column<bool>(type: "bit", nullable: false, defaultValue: true, comment: "استاندارد بودن کالا"),
                    Description = table.Column<string>(type: "nvarchar(1500)", nullable: true, comment: "توضیحات"),
                    EmployerOperationId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "تاریخ ایجاد"),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false, comment: "ایجاد کننده"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "تاریخ ویرایش"),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true, comment: "ویرایش کننده"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "حذف شدگی")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployerOperationProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployerOperationProducts_EmployerOperations_EmployerOperationId",
                        column: x => x.EmployerOperationId,
                        principalSchema: "engineer",
                        principalTable: "EmployerOperations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmployerOperationServices",
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
                    IncreaseRate = table.Column<decimal>(type: "decimal(5,2)", nullable: false, comment: "ضریب افزایش"),
                    IsStandard = table.Column<bool>(type: "bit", nullable: false, defaultValue: true, comment: "استاندارد بودن کالا"),
                    Description = table.Column<string>(type: "nvarchar(1500)", nullable: true, comment: "توضیحات"),
                    EmployerOperationId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "تاریخ ایجاد"),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false, comment: "ایجاد کننده"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "تاریخ ویرایش"),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true, comment: "ویرایش کننده"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "حذف شدگی")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployerOperationServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployerOperationServices_EmployerOperations_EmployerOperationId",
                        column: x => x.EmployerOperationId,
                        principalSchema: "engineer",
                        principalTable: "EmployerOperations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EngineeringStandardProducts_EmployerOperationId",
                schema: "engineer",
                table: "EngineeringStandardProducts",
                column: "EmployerOperationId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployerOperationProducts_EmployerOperationId",
                schema: "engineer",
                table: "EmployerOperationProducts",
                column: "EmployerOperationId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployerOperationServices_EmployerOperationId",
                schema: "engineer",
                table: "EmployerOperationServices",
                column: "EmployerOperationId");

            migrationBuilder.AddForeignKey(
                name: "FK_EngineeringStandardProducts_EmployerOperations_EmployerOperationId",
                schema: "engineer",
                table: "EngineeringStandardProducts",
                column: "EmployerOperationId",
                principalSchema: "engineer",
                principalTable: "EmployerOperations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EngineeringStandardProducts_EmployerOperations_EmployerOperationId",
                schema: "engineer",
                table: "EngineeringStandardProducts");

            migrationBuilder.DropTable(
                name: "EmployerOperationProducts",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "EmployerOperationServices",
                schema: "engineer");

            migrationBuilder.DropIndex(
                name: "IX_EngineeringStandardProducts_EmployerOperationId",
                schema: "engineer",
                table: "EngineeringStandardProducts");

            migrationBuilder.DropColumn(
                name: "EmployerOperationId",
                schema: "engineer",
                table: "EngineeringStandardProducts");
        }
    }
}
