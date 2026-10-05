using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddCertificateNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "RefrenceId",
                schema: "engineer",
                table: "TransportationRequestWarehouses",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CertificateNumber",
                schema: "engineer",
                table: "TransportationRequests",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CertificateNumber",
                schema: "engineer",
                table: "TransportationContractorPersonnels",
                type: "nvarchar(50)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefrenceId",
                schema: "engineer",
                table: "TransportationRequestWarehouses");

            migrationBuilder.DropColumn(
                name: "CertificateNumber",
                schema: "engineer",
                table: "TransportationRequests");

            migrationBuilder.DropColumn(
                name: "CertificateNumber",
                schema: "engineer",
                table: "TransportationContractorPersonnels");
        }
    }
}
