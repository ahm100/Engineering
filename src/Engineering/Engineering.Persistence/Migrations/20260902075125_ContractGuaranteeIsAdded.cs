using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ContractGuaranteeIsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContractGuarantees",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    ContractId = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه قرارداد"),
                    Type = table.Column<int>(type: "int", nullable: false, comment: "نوع تضمین"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "مبلغ تضمین"),
                    Percentage = table.Column<decimal>(type: "decimal(5,2)", nullable: true, comment: "درصد تضمین"),
                    Number = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false, comment: "شماره ضمانت‌نامه"),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "تاریخ صدور ضمانت‌نامه"),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "تاریخ انقضای ضمانت‌نامه"),
                    Status = table.Column<int>(type: "int", nullable: false, comment: "وضعیت تضمین"),
                    FileUrl = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: true, comment: "فایل ضمانت‌نامه"),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "تاریخ ایجاد"),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false, comment: "ایجاد کننده"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "تاریخ ویرایش"),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true, comment: "ویرایش کننده"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "حذف شدگی")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractGuarantees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractGuarantees_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalSchema: "engineer",
                        principalTable: "Contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContractGuarantees_ContractId",
                schema: "engineer",
                table: "ContractGuarantees",
                column: "ContractId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContractGuarantees",
                schema: "engineer");
        }
    }
}
