using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemoveContractorContractType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Add new enum column (int)
            migrationBuilder.AddColumn<int>(
                name: "ContractorContractType",
                schema: "engineer",
                table: "ContractorContracts",
                type: "int",
                nullable: true);

            migrationBuilder.Sql(@"
        UPDATE CC
        SET CC.ContractorContractType = TRY_CAST(CCT.ContractorContractTypeCode AS INT)
        FROM engineer.ContractorContracts CC
        INNER JOIN engineer.ContractorContractTypes CCT
            ON CC.ContractorContractTypeId = CCT.Id
    ");

            // 3. Make new enum column NOT NULL now that data is migrated
            migrationBuilder.AlterColumn<int>(
                name: "ContractorContractType",
                schema: "engineer",
                table: "ContractorContracts",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            // 4. Drop the foreign key
            migrationBuilder.DropForeignKey(
                name: "FK_ContractorContracts_ContractorContractTypes_ContractorContractTypeId",
                schema: "engineer",
                table: "ContractorContracts");

            // 5. Drop index
            migrationBuilder.DropIndex(
                name: "IX_ContractorContracts_ContractorContractTypeId",
                schema: "engineer",
                table: "ContractorContracts");

            // 6. Drop old foreign-key column
            migrationBuilder.DropColumn(
                name: "ContractorContractTypeId",
                schema: "engineer",
                table: "ContractorContracts");

            // 7. DROP TABLE LAST (only after data migration)
            migrationBuilder.DropTable(
                name: "ContractorContractTypes",
                schema: "engineer");
        }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContractorContractType",
                schema: "engineer",
                table: "ContractorContracts");

            migrationBuilder.AddColumn<long>(
                name: "ContractorContractTypeId",
                schema: "engineer",
                table: "ContractorContracts",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "ContractorContractTypes",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    CompanyId = table.Column<long>(type: "bigint", nullable: true),
                    ContractorContractTypeCode = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ContractorContractTypeName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "تاریخ ایجاد"),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false, comment: "ایجاد کننده"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, comment: "وضعیت فعال یا غیر فعال بودن"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "حذف شدگی"),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "تاریخ ویرایش"),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true, comment: "ویرایش کننده")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractorContractTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContractorContracts_ContractorContractTypeId",
                schema: "engineer",
                table: "ContractorContracts",
                column: "ContractorContractTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractorContracts_ContractorContractTypes_ContractorContractTypeId",
                schema: "engineer",
                table: "ContractorContracts",
                column: "ContractorContractTypeId",
                principalSchema: "engineer",
                principalTable: "ContractorContractTypes",
                principalColumn: "Id");
        }
    }
}
