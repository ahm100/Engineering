using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddLogesticCargos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [engineer].[TransportationRequestWarehouses]");

            migrationBuilder.DropForeignKey(
                name: "FK_TransportationRequestWarehouses_ShippingCosts_ShippingCostId",
                schema: "engineer",
                table: "TransportationRequestWarehouses");

            migrationBuilder.DropForeignKey(
                name: "FK_TransportationRequestWarehouses_TransportationRequests_TransportationRequestId",
                schema: "engineer",
                table: "TransportationRequestWarehouses");

            migrationBuilder.DropIndex(
                name: "IX_TransportationRequestWarehouses_PackingId",
                schema: "engineer",
                table: "TransportationRequestWarehouses");

            migrationBuilder.DropIndex(
                name: "IX_TransportationRequestWarehouses_ShippingCostId",
                schema: "engineer",
                table: "TransportationRequestWarehouses");

            migrationBuilder.DropColumn(
                name: "PackingId",
                schema: "engineer",
                table: "TransportationRequestWarehouses");

            migrationBuilder.DropColumn(
                name: "PackingNumber",
                schema: "engineer",
                table: "TransportationRequestWarehouses");

            migrationBuilder.DropColumn(
                name: "RefrenceId",
                schema: "engineer",
                table: "TransportationRequestWarehouses");

            migrationBuilder.DropColumn(
                name: "ShippingCostId",
                schema: "engineer",
                table: "TransportationRequestWarehouses");

            migrationBuilder.DropColumn(
                name: "ThirdPartyName",
                schema: "engineer",
                table: "TransportationRequestWarehouses");

            migrationBuilder.DropColumn(
                name: "TransferPrice",
                schema: "engineer",
                table: "TransportationRequestWarehouses");

            migrationBuilder.DropColumn(
                name: "Weight",
                schema: "engineer",
                table: "TransportationRequestWarehouses");

            migrationBuilder.RenameColumn(
                name: "WarehouseId",
                schema: "engineer",
                table: "TransportationRequestWarehouses",
                newName: "PackingSourceAddressId");

            migrationBuilder.RenameColumn(
                name: "TransportationRequestId",
                schema: "engineer",
                table: "TransportationRequestWarehouses",
                newName: "TransportationCargoPalletId");

            migrationBuilder.RenameColumn(
                name: "ThirdPartyId",
                schema: "engineer",
                table: "TransportationRequestWarehouses",
                newName: "PackingDestinationAddressId");

            migrationBuilder.RenameIndex(
                name: "IX_TransportationRequestWarehouses_WarehouseId",
                schema: "engineer",
                table: "TransportationRequestWarehouses",
                newName: "IX_TransportationRequestWarehouses_PackingSourceAddressId");

            migrationBuilder.RenameIndex(
                name: "IX_TransportationRequestWarehouses_TransportationRequestId",
                schema: "engineer",
                table: "TransportationRequestWarehouses",
                newName: "IX_TransportationRequestWarehouses_TransportationCargoPalletId");

            migrationBuilder.RenameIndex(
                name: "IX_TransportationRequestWarehouses_ThirdPartyId",
                schema: "engineer",
                table: "TransportationRequestWarehouses",
                newName: "IX_TransportationRequestWarehouses_PackingDestinationAddressId");

            migrationBuilder.AlterColumn<long>(
                name: "ThirdPartyId",
                schema: "engineer",
                table: "TransportationContractors",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartOfContract",
                schema: "engineer",
                table: "TransportationContractors",
                type: "datetime2",
                nullable: true,
                comment: "شروع قرارداد",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "شروع قرارداد");

            migrationBuilder.AlterColumn<decimal>(
                name: "PercentageValue",
                schema: "engineer",
                table: "TransportationContractors",
                type: "decimal(18,2)",
                nullable: true,
                comment: "درصد محاسبه",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "درصد محاسبه");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndOfContract",
                schema: "engineer",
                table: "TransportationContractors",
                type: "datetime2",
                nullable: true,
                comment: "پایان قرارداد",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "پایان قرارداد");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                schema: "engineer",
                table: "TransportationContractors",
                type: "nvarchar(max)",
                nullable: true,
                comment: "عنوان");

            migrationBuilder.CreateTable(
                name: "TransportationCargos",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    PackingNumber = table.Column<long>(type: "bigint", nullable: false),
                    PackingId = table.Column<long>(type: "bigint", nullable: false),
                    ThirdPartyId = table.Column<long>(type: "bigint", nullable: true),
                    ThirdPartyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PackingShippingId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportationCargos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransportationCargoPallets",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    TransportationCargoId = table.Column<long>(type: "bigint", nullable: false),
                    TransportationRequestId = table.Column<long>(type: "bigint", nullable: true),
                    PalletNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PackingPalletId = table.Column<long>(type: "bigint", nullable: false),
                    PackingSourceAddressId = table.Column<long>(type: "bigint", nullable: true),
                    PackingDestinationAddressId = table.Column<long>(type: "bigint", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TransferPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ShippingCostId = table.Column<long>(type: "bigint", nullable: true),
                    RefrenceId = table.Column<long>(type: "bigint", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportationCargoPallets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransportationCargoPallets_ShippingCosts_ShippingCostId",
                        column: x => x.ShippingCostId,
                        principalSchema: "engineer",
                        principalTable: "ShippingCosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransportationCargoPallets_TransportationCargos_TransportationCargoId",
                        column: x => x.TransportationCargoId,
                        principalSchema: "engineer",
                        principalTable: "TransportationCargos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransportationCargoPallets_TransportationRequests_TransportationRequestId",
                        column: x => x.TransportationRequestId,
                        principalSchema: "engineer",
                        principalTable: "TransportationRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransportationCargoPallets_PackingDestinationAddressId",
                schema: "engineer",
                table: "TransportationCargoPallets",
                column: "PackingDestinationAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_TransportationCargoPallets_PackingPalletId",
                schema: "engineer",
                table: "TransportationCargoPallets",
                column: "PackingPalletId");

            migrationBuilder.CreateIndex(
                name: "IX_TransportationCargoPallets_PackingSourceAddressId",
                schema: "engineer",
                table: "TransportationCargoPallets",
                column: "PackingSourceAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_TransportationCargoPallets_ShippingCostId",
                schema: "engineer",
                table: "TransportationCargoPallets",
                column: "ShippingCostId");

            migrationBuilder.CreateIndex(
                name: "IX_TransportationCargoPallets_TransportationCargoId",
                schema: "engineer",
                table: "TransportationCargoPallets",
                column: "TransportationCargoId");

            migrationBuilder.CreateIndex(
                name: "IX_TransportationCargoPallets_TransportationRequestId",
                schema: "engineer",
                table: "TransportationCargoPallets",
                column: "TransportationRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_TransportationCargos_PackingId",
                schema: "engineer",
                table: "TransportationCargos",
                column: "PackingId");

            migrationBuilder.CreateIndex(
                name: "IX_TransportationCargos_PackingShippingId",
                schema: "engineer",
                table: "TransportationCargos",
                column: "PackingShippingId");

            migrationBuilder.CreateIndex(
                name: "IX_TransportationCargos_ThirdPartyId",
                schema: "engineer",
                table: "TransportationCargos",
                column: "ThirdPartyId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransportationRequestWarehouses_TransportationCargoPallets_TransportationCargoPalletId",
                schema: "engineer",
                table: "TransportationRequestWarehouses",
                column: "TransportationCargoPalletId",
                principalSchema: "engineer",
                principalTable: "TransportationCargoPallets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransportationRequestWarehouses_TransportationCargoPallets_TransportationCargoPalletId",
                schema: "engineer",
                table: "TransportationRequestWarehouses");

            migrationBuilder.DropTable(
                name: "TransportationCargoPallets",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "TransportationCargos",
                schema: "engineer");

            migrationBuilder.DropColumn(
                name: "Title",
                schema: "engineer",
                table: "TransportationContractors");

            migrationBuilder.RenameColumn(
                name: "TransportationCargoPalletId",
                schema: "engineer",
                table: "TransportationRequestWarehouses",
                newName: "TransportationRequestId");

            migrationBuilder.RenameColumn(
                name: "PackingSourceAddressId",
                schema: "engineer",
                table: "TransportationRequestWarehouses",
                newName: "WarehouseId");

            migrationBuilder.RenameColumn(
                name: "PackingDestinationAddressId",
                schema: "engineer",
                table: "TransportationRequestWarehouses",
                newName: "ThirdPartyId");

            migrationBuilder.RenameIndex(
                name: "IX_TransportationRequestWarehouses_TransportationCargoPalletId",
                schema: "engineer",
                table: "TransportationRequestWarehouses",
                newName: "IX_TransportationRequestWarehouses_TransportationRequestId");

            migrationBuilder.RenameIndex(
                name: "IX_TransportationRequestWarehouses_PackingSourceAddressId",
                schema: "engineer",
                table: "TransportationRequestWarehouses",
                newName: "IX_TransportationRequestWarehouses_WarehouseId");

            migrationBuilder.RenameIndex(
                name: "IX_TransportationRequestWarehouses_PackingDestinationAddressId",
                schema: "engineer",
                table: "TransportationRequestWarehouses",
                newName: "IX_TransportationRequestWarehouses_ThirdPartyId");

            migrationBuilder.AddColumn<long>(
                name: "PackingId",
                schema: "engineer",
                table: "TransportationRequestWarehouses",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "PackingNumber",
                schema: "engineer",
                table: "TransportationRequestWarehouses",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "RefrenceId",
                schema: "engineer",
                table: "TransportationRequestWarehouses",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ShippingCostId",
                schema: "engineer",
                table: "TransportationRequestWarehouses",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ThirdPartyName",
                schema: "engineer",
                table: "TransportationRequestWarehouses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TransferPrice",
                schema: "engineer",
                table: "TransportationRequestWarehouses",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Weight",
                schema: "engineer",
                table: "TransportationRequestWarehouses",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ThirdPartyId",
                schema: "engineer",
                table: "TransportationContractors",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartOfContract",
                schema: "engineer",
                table: "TransportationContractors",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "شروع قرارداد",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldComment: "شروع قرارداد");

            migrationBuilder.AlterColumn<decimal>(
                name: "PercentageValue",
                schema: "engineer",
                table: "TransportationContractors",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                comment: "درصد محاسبه",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true,
                oldComment: "درصد محاسبه");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndOfContract",
                schema: "engineer",
                table: "TransportationContractors",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "پایان قرارداد",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldComment: "پایان قرارداد");

            migrationBuilder.CreateIndex(
                name: "IX_TransportationRequestWarehouses_PackingId",
                schema: "engineer",
                table: "TransportationRequestWarehouses",
                column: "PackingId");

            migrationBuilder.CreateIndex(
                name: "IX_TransportationRequestWarehouses_ShippingCostId",
                schema: "engineer",
                table: "TransportationRequestWarehouses",
                column: "ShippingCostId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransportationRequestWarehouses_ShippingCosts_ShippingCostId",
                schema: "engineer",
                table: "TransportationRequestWarehouses",
                column: "ShippingCostId",
                principalSchema: "engineer",
                principalTable: "ShippingCosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransportationRequestWarehouses_TransportationRequests_TransportationRequestId",
                schema: "engineer",
                table: "TransportationRequestWarehouses",
                column: "TransportationRequestId",
                principalSchema: "engineer",
                principalTable: "TransportationRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
