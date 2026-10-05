using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddContractType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContractTypes",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    Kind = table.Column<int>(type: "int", nullable: false, comment: "نوع قرارداد"),
                    PricingMethod = table.Column<int>(type: "int", nullable: false, comment: "روش قیمت‌گذاری"),
                    PaymentBasis = table.Column<int>(type: "int", nullable: false, comment: "مبنای پرداخت"),
                    IsSubjectToAmendment = table.Column<bool>(type: "bit", nullable: false, comment: "مشمول تعدیل"),
                    ContractId = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه قرارداد"),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "تاریخ ایجاد"),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false, comment: "ایجاد کننده"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "تاریخ ویرایش"),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true, comment: "ویرایش کننده"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "حذف شدگی")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractTypes_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalSchema: "engineer",
                        principalTable: "Contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContractTypes_ContractId",
                schema: "engineer",
                table: "ContractTypes",
                column: "ContractId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContractTypes",
                schema: "engineer");
        }
    }
}
