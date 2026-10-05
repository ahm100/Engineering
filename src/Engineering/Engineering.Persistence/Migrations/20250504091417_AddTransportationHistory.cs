using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddTransportationHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TransportationRequestHistories",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1500)", nullable: true),
                    DriverId = table.Column<long>(type: "bigint", nullable: true),
                    DriverName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountNumber = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    BankId = table.Column<long>(type: "bigint", nullable: true),
                    CardNumber = table.Column<string>(type: "nvarchar(30)", nullable: true),
                    AccountName = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    IBAN = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CurrencyUnitId = table.Column<long>(type: "bigint", nullable: true),
                    AccountDescription = table.Column<string>(type: "nvarchar(1500)", nullable: true),
                    TransportationRequestStatus = table.Column<int>(type: "int", nullable: true),
                    TransportationPaymentType = table.Column<int>(type: "int", nullable: true),
                    ManagerDescription = table.Column<string>(type: "nvarchar(1500)", nullable: true),
                    ConfirmUserId = table.Column<long>(type: "bigint", nullable: true),
                    ConfirmDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PaymentOrderId = table.Column<long>(type: "bigint", nullable: true),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TransportationRequestId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportationRequestHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransportationRequestHistories_TransportationRequests_TransportationRequestId",
                        column: x => x.TransportationRequestId,
                        principalSchema: "engineer",
                        principalTable: "TransportationRequests",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransportationRequestHistories_TransportationRequestId",
                schema: "engineer",
                table: "TransportationRequestHistories",
                column: "TransportationRequestId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransportationRequestHistories",
                schema: "engineer");
        }
    }
}
