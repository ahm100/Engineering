using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class IsSubjectToAdjustment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSubjectToAdjustment",
                schema: "engineer",
                table: "ContractTypeDetails",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "مشمول تعدیل");

            migrationBuilder.AddColumn<bool>(
                name: "IsSubjectToAdjustment",
                schema: "engineer",
                table: "ContractFinancialInformations",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "مشمول تعدیل");

            migrationBuilder.CreateTable(
                name: "ContractAdjustmentConfigurations",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    ContractId = table.Column<long>(type: "bigint", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    PriceIndexBaseYear = table.Column<int>(type: "int", nullable: true),
                    PriceIndexBasePeriod = table.Column<int>(type: "int", nullable: true),
                    PriceIndexId = table.Column<long>(type: "bigint", nullable: true),
                    CurrencyBaseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CurrencyBaseRate = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    CurrencyId = table.Column<long>(type: "bigint", nullable: true),
                    CurrencyReferenceType = table.Column<int>(type: "int", nullable: true),
                    CurrencyCustomReference = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    OtherBasis = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    OtherReference = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    OtherIndex = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "تاریخ ایجاد"),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false, comment: "ایجاد کننده"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "تاریخ ویرایش"),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true, comment: "ویرایش کننده"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "حذف شدگی")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractAdjustmentConfigurations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractAdjustmentConfigurations_ContractAdjustmentIndexes_PriceIndexId",
                        column: x => x.PriceIndexId,
                        principalSchema: "engineer",
                        principalTable: "ContractAdjustmentIndexes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContractAdjustmentConfigurations_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalSchema: "engineer",
                        principalTable: "Contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContractAdjustmentScopes",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    ContractAdjustmentConfigurationId = table.Column<long>(type: "bigint", nullable: false),
                    ScopeType = table.Column<int>(type: "int", nullable: false),
                    ContractTypeKind = table.Column<int>(type: "int", nullable: true),
                    ContractTypeDetailId = table.Column<long>(type: "bigint", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "تاریخ ایجاد"),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false, comment: "ایجاد کننده"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "تاریخ ویرایش"),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true, comment: "ویرایش کننده"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "حذف شدگی")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractAdjustmentScopes", x => x.Id);
                    table.CheckConstraint("CK_ContractAdjustmentScopes_Shape", "([ScopeType] = 1 AND [ContractTypeKind] IS NULL AND [ContractTypeDetailId] IS NULL) OR ([ScopeType] = 2 AND [ContractTypeKind] IS NOT NULL AND [ContractTypeDetailId] IS NULL) OR ([ScopeType] = 3 AND [ContractTypeKind] IS NULL AND [ContractTypeDetailId] IS NOT NULL)");
                    table.ForeignKey(
                        name: "FK_ContractAdjustmentScopes_ContractAdjustmentConfigurations_ContractAdjustmentConfigurationId",
                        column: x => x.ContractAdjustmentConfigurationId,
                        principalSchema: "engineer",
                        principalTable: "ContractAdjustmentConfigurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContractAdjustmentScopes_ContractTypeDetails_ContractTypeDetailId",
                        column: x => x.ContractTypeDetailId,
                        principalSchema: "engineer",
                        principalTable: "ContractTypeDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContractAdjustmentConfigurations_ContractId",
                schema: "engineer",
                table: "ContractAdjustmentConfigurations",
                column: "ContractId",
                unique: true,
                filter: "[IsDeleted] = 0");

            migrationBuilder.CreateIndex(
                name: "IX_ContractAdjustmentConfigurations_PriceIndexId",
                schema: "engineer",
                table: "ContractAdjustmentConfigurations",
                column: "PriceIndexId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractAdjustmentScopes_ContractAdjustmentConfigurationId",
                schema: "engineer",
                table: "ContractAdjustmentScopes",
                column: "ContractAdjustmentConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractAdjustmentScopes_ContractTypeDetailId",
                schema: "engineer",
                table: "ContractAdjustmentScopes",
                column: "ContractTypeDetailId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContractAdjustmentScopes",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "ContractAdjustmentConfigurations",
                schema: "engineer");

            migrationBuilder.DropColumn(
                name: "IsSubjectToAdjustment",
                schema: "engineer",
                table: "ContractTypeDetails");

            migrationBuilder.DropColumn(
                name: "IsSubjectToAdjustment",
                schema: "engineer",
                table: "ContractFinancialInformations");
        }
    }
}
