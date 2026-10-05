using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddInsuranceEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Volume",
                schema: "engineer",
                table: "TransportationRequests",
                type: "decimal(18,0)",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "SecondPrefix",
                schema: "engineer",
                table: "TransportationContractors",
                type: "bigint",
                nullable: true,
                comment: "مقدار دوم بارنامه داخلی",
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldNullable: true,
                oldComment: "مقدار دوم بارنامه داخلی");

            migrationBuilder.AddColumn<decimal>(
                name: "ServicePrice",
                schema: "engineer",
                table: "TransportationContractors",
                type: "decimal(18,2)",
                nullable: true,
                comment: "هزینه خدمات");

            migrationBuilder.AddColumn<decimal>(
                name: "TaxPercent",
                schema: "engineer",
                table: "TransportationContractors",
                type: "decimal(18,2)",
                nullable: true,
                comment: "درصد مالیات");

            migrationBuilder.AlterColumn<decimal>(
                name: "Tax",
                schema: "engineer",
                table: "ShippingCosts",
                type: "decimal(18,2)",
                nullable: true,
                comment: "مالیات",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "مالیات");

            migrationBuilder.AlterColumn<decimal>(
                name: "LoadWeight",
                schema: "engineer",
                table: "ShippingCosts",
                type: "decimal(18,2)",
                nullable: true,
                comment: "وزن بار",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "وزن بار");

            migrationBuilder.AlterColumn<int>(
                name: "Count",
                schema: "engineer",
                table: "ShippingCosts",
                type: "int",
                nullable: true,
                comment: "تعداد",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "تعداد");

            migrationBuilder.AddColumn<decimal>(
                name: "Latitude",
                schema: "engineer",
                table: "ShippingCosts",
                type: "decimal(18,9)",
                nullable: true,
                comment: "عرض جغرافیایی");

            migrationBuilder.AddColumn<decimal>(
                name: "Longitude",
                schema: "engineer",
                table: "ShippingCosts",
                type: "decimal(18,9)",
                nullable: true,
                comment: "طول جغرافیایی");

            migrationBuilder.AddColumn<long>(
                name: "ThirdPartyId",
                schema: "engineer",
                table: "ShippingCosts",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TransportationContractorInsurances",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    TransportationContractorId = table.Column<long>(type: "bigint", nullable: false),
                    MinProductPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "کمترین ارزش بار"),
                    MaxProductPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "بیشترین ارزش بار"),
                    FixedPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "مقدار ثابت"),
                    Multiplication = table.Column<decimal>(type: "decimal(18,2)", nullable: true, defaultValue: 1m, comment: "ضرب"),
                    Division = table.Column<decimal>(type: "decimal(18,2)", nullable: true, defaultValue: 1m, comment: "تقسیم"),
                    Subtraction = table.Column<decimal>(type: "decimal(18,2)", nullable: true, defaultValue: 0m, comment: "تفریق"),
                    Addition = table.Column<decimal>(type: "decimal(18,2)", nullable: true, defaultValue: 0m, comment: "جمع"),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "تاریخ ایجاد"),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false, comment: "ایجاد کننده"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "تاریخ ویرایش"),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true, comment: "ویرایش کننده"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "حذف شدگی")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportationContractorInsurances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransportationContractorInsurances_TransportationContractors_TransportationContractorId",
                        column: x => x.TransportationContractorId,
                        principalSchema: "engineer",
                        principalTable: "TransportationContractors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShippingCosts_ThirdPartyId",
                schema: "engineer",
                table: "ShippingCosts",
                column: "ThirdPartyId");

            migrationBuilder.CreateIndex(
                name: "IX_TransportationContractorInsurances_TransportationContractorId",
                schema: "engineer",
                table: "TransportationContractorInsurances",
                column: "TransportationContractorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransportationContractorInsurances",
                schema: "engineer");

            migrationBuilder.DropIndex(
                name: "IX_ShippingCosts_ThirdPartyId",
                schema: "engineer",
                table: "ShippingCosts");

            migrationBuilder.DropColumn(
                name: "Volume",
                schema: "engineer",
                table: "TransportationRequests");

            migrationBuilder.DropColumn(
                name: "ServicePrice",
                schema: "engineer",
                table: "TransportationContractors");

            migrationBuilder.DropColumn(
                name: "TaxPercent",
                schema: "engineer",
                table: "TransportationContractors");

            migrationBuilder.DropColumn(
                name: "Latitude",
                schema: "engineer",
                table: "ShippingCosts");

            migrationBuilder.DropColumn(
                name: "Longitude",
                schema: "engineer",
                table: "ShippingCosts");

            migrationBuilder.DropColumn(
                name: "ThirdPartyId",
                schema: "engineer",
                table: "ShippingCosts");

            migrationBuilder.AlterColumn<string>(
                name: "SecondPrefix",
                schema: "engineer",
                table: "TransportationContractors",
                type: "nvarchar(10)",
                nullable: true,
                comment: "مقدار دوم بارنامه داخلی",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "مقدار دوم بارنامه داخلی");

            migrationBuilder.AlterColumn<decimal>(
                name: "Tax",
                schema: "engineer",
                table: "ShippingCosts",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                comment: "مالیات",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true,
                oldComment: "مالیات");

            migrationBuilder.AlterColumn<decimal>(
                name: "LoadWeight",
                schema: "engineer",
                table: "ShippingCosts",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                comment: "وزن بار",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true,
                oldComment: "وزن بار");

            migrationBuilder.AlterColumn<int>(
                name: "Count",
                schema: "engineer",
                table: "ShippingCosts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "تعداد",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldComment: "تعداد");
        }
    }
}
