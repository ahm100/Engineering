using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemoveContractsUselessEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContractorStatusStatementServices_ContractorContractDetailSkills_ContractorContractDetailSkillId",
                schema: "engineer",
                table: "ContractorStatusStatementServices");

            migrationBuilder.DropTable(
                name: "ContractorContractDetailThirdParties",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "ContractorContractDetailSkills",
                schema: "engineer");

            migrationBuilder.DropIndex(
                name: "IX_ContractorStatusStatementServices_ContractorContractDetailSkillId",
                schema: "engineer",
                table: "ContractorStatusStatementServices");

            migrationBuilder.DropColumn(
                name: "ContractorContractDetailSkillId",
                schema: "engineer",
                table: "ContractorStatusStatementServices");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ContractorContractDetailSkillId",
                schema: "engineer",
                table: "ContractorStatusStatementServices",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ContractorContractDetailSkills",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    ContractorContractId = table.Column<long>(type: "bigint", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "تاریخ ایجاد"),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false, comment: "ایجاد کننده"),
                    CurrencyId = table.Column<long>(type: "bigint", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "حذف شدگی"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    SkillId = table.Column<long>(type: "bigint", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "تاریخ ویرایش"),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true, comment: "ویرایش کننده")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractorContractDetailSkills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractorContractDetailSkills_ContractorContracts_ContractorContractId",
                        column: x => x.ContractorContractId,
                        principalSchema: "engineer",
                        principalTable: "ContractorContracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContractorContractDetailThirdParties",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    ContractorContractDetailSkillId = table.Column<long>(type: "bigint", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "تاریخ ایجاد"),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false, comment: "ایجاد کننده"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "حذف شدگی"),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    ThirdPartyId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "تاریخ ویرایش"),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true, comment: "ویرایش کننده")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractorContractDetailThirdParties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractorContractDetailThirdParties_ContractorContractDetailSkills_ContractorContractDetailSkillId",
                        column: x => x.ContractorContractDetailSkillId,
                        principalSchema: "engineer",
                        principalTable: "ContractorContractDetailSkills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContractorStatusStatementServices_ContractorContractDetailSkillId",
                schema: "engineer",
                table: "ContractorStatusStatementServices",
                column: "ContractorContractDetailSkillId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractorContractDetailSkills_ContractorContractId",
                schema: "engineer",
                table: "ContractorContractDetailSkills",
                column: "ContractorContractId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractorContractDetailThirdParties_ContractorContractDetailSkillId",
                schema: "engineer",
                table: "ContractorContractDetailThirdParties",
                column: "ContractorContractDetailSkillId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractorStatusStatementServices_ContractorContractDetailSkills_ContractorContractDetailSkillId",
                schema: "engineer",
                table: "ContractorStatusStatementServices",
                column: "ContractorContractDetailSkillId",
                principalSchema: "engineer",
                principalTable: "ContractorContractDetailSkills",
                principalColumn: "Id");
        }
    }
}
