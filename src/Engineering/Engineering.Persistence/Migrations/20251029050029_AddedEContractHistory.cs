using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedEContractHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "engineer",
                table: "EmployerContracts",
                type: "nvarchar(1500)",
                nullable: true,
                comment: "توضیحات",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastDescription",
                schema: "engineer",
                table: "EmployerContracts",
                type: "nvarchar(1500)",
                nullable: true,
                comment: "آخرین توضیحات");

            migrationBuilder.CreateTable(
                name: "EmployerContractHistories",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    IsFirst = table.Column<bool>(type: "bit", nullable: false, comment: "اولین قرارداد"),
                    Status = table.Column<int>(type: "int", nullable: false, comment: "وضعیت قرارداد"),
                    Code = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true, comment: "کد"),
                    CurrencyRate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "تاریخ شروع"),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "تاریخ پایان"),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "مجموع مبالغ"),
                    AdvancePayment = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "پیش پرداخت"),
                    Description = table.Column<string>(type: "nvarchar(1500)", nullable: true, comment: "توضیحات"),
                    EmployerContractId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_EmployerContractHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployerContractHistories_EmployerContracts_EmployerContractId",
                        column: x => x.EmployerContractId,
                        principalSchema: "engineer",
                        principalTable: "EmployerContracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployerContractHistories_EmployerContractId",
                schema: "engineer",
                table: "EmployerContractHistories",
                column: "EmployerContractId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployerContractHistories",
                schema: "engineer");

            migrationBuilder.DropColumn(
                name: "LastDescription",
                schema: "engineer",
                table: "EmployerContracts");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "engineer",
                table: "EmployerContracts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1500)",
                oldNullable: true,
                oldComment: "توضیحات");
        }
    }
}
