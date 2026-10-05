using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddCCHVerEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "PreferentialReferenceCode",
                schema: "engineer",
                table: "EngineeringServices",
                type: "uniqueidentifier",
                nullable: true,
                comment: "˜Ï ãÑÌÚ ÊÝÕ?á?",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true,
                oldComment: "کد مرجع تفص?ل?");

            migrationBuilder.CreateTable(
                name: "ContractorContractHeaderVersions",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    Content = table.Column<string>(type: "nvarchar(MAX)", nullable: false, comment: "محتوای ورژن قرارداد پیمانکار"),
                    Version = table.Column<int>(type: "int", nullable: false, comment: "ورژن"),
                    CreatedPaymentDate = table.Column<DateTime>(type: "datetime", nullable: true, comment: "تاریخ صدور پرداخت"),
                    ContractorContractHeaderId = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه قرارداد پیمانکار"),
                    ContractorStatusStatementId = table.Column<long>(type: "bigint", nullable: true, comment: "شناسه صورت وضعیت  پیمانکار"),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "تاریخ ایجاد"),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false, comment: "ایجاد کننده"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "تاریخ ویرایش"),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true, comment: "ویرایش کننده"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "حذف شدگی")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractorContractHeaderVersions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractorContractHeaderVersions_ContractorContractHeaders_ContractorContractHeaderId",
                        column: x => x.ContractorContractHeaderId,
                        principalSchema: "engineer",
                        principalTable: "ContractorContractHeaders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContractorContractHeaderVersions_ContractorStatusStatements_ContractorStatusStatementId",
                        column: x => x.ContractorStatusStatementId,
                        principalSchema: "engineer",
                        principalTable: "ContractorStatusStatements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContractorContractHeaderVersions_ContractorContractHeaderId",
                schema: "engineer",
                table: "ContractorContractHeaderVersions",
                column: "ContractorContractHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractorContractHeaderVersions_ContractorStatusStatementId",
                schema: "engineer",
                table: "ContractorContractHeaderVersions",
                column: "ContractorStatusStatementId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContractorContractHeaderVersions",
                schema: "engineer");

            migrationBuilder.AlterColumn<Guid>(
                name: "PreferentialReferenceCode",
                schema: "engineer",
                table: "EngineeringServices",
                type: "uniqueidentifier",
                nullable: true,
                comment: "کد مرجع تفص?ل?",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true,
                oldComment: "˜Ï ãÑÌÚ ÊÝÕ?á?");
        }
    }
}
