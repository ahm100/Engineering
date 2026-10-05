using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddPriceHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContractorContractDetailPriceHistories",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CurrencyId = table.Column<long>(type: "bigint", nullable: false),
                    ContractorContractDetailPriceId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "تاریخ ایجاد"),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false, comment: "ایجاد کننده"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "تاریخ ویرایش"),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true, comment: "ویرایش کننده"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "حذف شدگی"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, comment: "وضعیت فعال یا غیر فعال بودن")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractorContractDetailPriceHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractorContractDetailPriceHistories_ContractorContractDetailPrices_ContractorContractDetailPriceId",
                        column: x => x.ContractorContractDetailPriceId,
                        principalSchema: "engineer",
                        principalTable: "ContractorContractDetailPrices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContractorContractDetailPriceHistories_ContractorContractDetailPriceId",
                schema: "engineer",
                table: "ContractorContractDetailPriceHistories",
                column: "ContractorContractDetailPriceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContractorContractDetailPriceHistories",
                schema: "engineer");
        }
    }
}
