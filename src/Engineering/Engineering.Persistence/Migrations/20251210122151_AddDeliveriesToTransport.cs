using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddDeliveriesToTransport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeliveryMethod",
                schema: "engineer",
                table: "TransportationRequests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeliveryType",
                schema: "engineer",
                table: "TransportationRequests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeliveryMethod",
                schema: "engineer",
                table: "TransportationContractors",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeliveryType",
                schema: "engineer",
                table: "TransportationContractors",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryMethod",
                schema: "engineer",
                table: "TransportationRequests");

            migrationBuilder.DropColumn(
                name: "DeliveryType",
                schema: "engineer",
                table: "TransportationRequests");

            migrationBuilder.DropColumn(
                name: "DeliveryMethod",
                schema: "engineer",
                table: "TransportationContractors");

            migrationBuilder.DropColumn(
                name: "DeliveryType",
                schema: "engineer",
                table: "TransportationContractors");
        }
    }
}
