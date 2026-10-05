using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddShippingDetailToPallets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TransportationCargos_PackingShippingId",
                schema: "engineer",
                table: "TransportationCargos");

            migrationBuilder.DropColumn(
                name: "PackingShippingId",
                schema: "engineer",
                table: "TransportationCargos");

            migrationBuilder.AddColumn<int>(
                name: "DeliveryMethod",
                schema: "engineer",
                table: "TransportationCargoPallets",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeliveryType",
                schema: "engineer",
                table: "TransportationCargoPallets",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Driver",
                schema: "engineer",
                table: "TransportationCargoPallets",
                type: "nvarchar(250)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DriverPhoneNumber",
                schema: "engineer",
                table: "TransportationCargoPallets",
                type: "nvarchar(250)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NumberPlate",
                schema: "engineer",
                table: "TransportationCargoPallets",
                type: "nvarchar(250)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PackingShippingType",
                schema: "engineer",
                table: "TransportationCargoPallets",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PostageDate",
                schema: "engineer",
                table: "TransportationCargoPallets",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TransportationContractorId",
                schema: "engineer",
                table: "TransportationCargoPallets",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VehicleName",
                schema: "engineer",
                table: "TransportationCargoPallets",
                type: "nvarchar(250)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TransportationCargoPallets_TransportationContractorId",
                schema: "engineer",
                table: "TransportationCargoPallets",
                column: "TransportationContractorId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransportationCargoPallets_TransportationContractors_TransportationContractorId",
                schema: "engineer",
                table: "TransportationCargoPallets",
                column: "TransportationContractorId",
                principalSchema: "engineer",
                principalTable: "TransportationContractors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransportationCargoPallets_TransportationContractors_TransportationContractorId",
                schema: "engineer",
                table: "TransportationCargoPallets");

            migrationBuilder.DropIndex(
                name: "IX_TransportationCargoPallets_TransportationContractorId",
                schema: "engineer",
                table: "TransportationCargoPallets");

            migrationBuilder.DropColumn(
                name: "DeliveryMethod",
                schema: "engineer",
                table: "TransportationCargoPallets");

            migrationBuilder.DropColumn(
                name: "DeliveryType",
                schema: "engineer",
                table: "TransportationCargoPallets");

            migrationBuilder.DropColumn(
                name: "Driver",
                schema: "engineer",
                table: "TransportationCargoPallets");

            migrationBuilder.DropColumn(
                name: "DriverPhoneNumber",
                schema: "engineer",
                table: "TransportationCargoPallets");

            migrationBuilder.DropColumn(
                name: "NumberPlate",
                schema: "engineer",
                table: "TransportationCargoPallets");

            migrationBuilder.DropColumn(
                name: "PackingShippingType",
                schema: "engineer",
                table: "TransportationCargoPallets");

            migrationBuilder.DropColumn(
                name: "PostageDate",
                schema: "engineer",
                table: "TransportationCargoPallets");

            migrationBuilder.DropColumn(
                name: "TransportationContractorId",
                schema: "engineer",
                table: "TransportationCargoPallets");

            migrationBuilder.DropColumn(
                name: "VehicleName",
                schema: "engineer",
                table: "TransportationCargoPallets");

            migrationBuilder.AddColumn<long>(
                name: "PackingShippingId",
                schema: "engineer",
                table: "TransportationCargos",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_TransportationCargos_PackingShippingId",
                schema: "engineer",
                table: "TransportationCargos",
                column: "PackingShippingId");
        }
    }
}
