using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddIsCreditAndPaymentType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCredit",
                schema: "engineer",
                table: "TransportationRequests",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "TransportationPaymentType",
                schema: "engineer",
                table: "TransportationRequests",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCredit",
                schema: "engineer",
                table: "TransportationRequests");

            migrationBuilder.DropColumn(
                name: "TransportationPaymentType",
                schema: "engineer",
                table: "TransportationRequests");
        }
    }
}
