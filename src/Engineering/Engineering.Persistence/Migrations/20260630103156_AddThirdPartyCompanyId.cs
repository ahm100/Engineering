using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddThirdPartyCompanyId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "SourceCityId",
                schema: "engineer",
                table: "ShippingCosts",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "DestinationCityId",
                schema: "engineer",
                table: "ShippingCosts",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "ThirdPartyCompanyId",
                schema: "engineer",
                table: "ShippingCosts",
                type: "bigint",
                nullable: true,
                comment: "کمپانی طرف حساب");

            migrationBuilder.AlterColumn<long>(
                name: "SourceCityId",
                schema: "engineer",
                table: "ShippingCostHistories",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "DestinationCityId",
                schema: "engineer",
                table: "ShippingCostHistories",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "ThirdPartyCompanyId",
                schema: "engineer",
                table: "ShippingCostHistories",
                type: "bigint",
                nullable: true,
                comment: "کمپانی طرف حساب");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThirdPartyCompanyId",
                schema: "engineer",
                table: "ShippingCosts");

            migrationBuilder.DropColumn(
                name: "ThirdPartyCompanyId",
                schema: "engineer",
                table: "ShippingCostHistories");

            migrationBuilder.AlterColumn<long>(
                name: "SourceCityId",
                schema: "engineer",
                table: "ShippingCosts",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "DestinationCityId",
                schema: "engineer",
                table: "ShippingCosts",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "SourceCityId",
                schema: "engineer",
                table: "ShippingCostHistories",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "DestinationCityId",
                schema: "engineer",
                table: "ShippingCostHistories",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);
        }
    }
}
