using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemoveEmployerContract : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployerStatusStatements_EmployerContracts_EmployerContractId",
                schema: "engineer",
                table: "EmployerStatusStatements");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectOperations_EmployerContracts_EmployerContractId",
                schema: "engineer",
                table: "ProjectOperations");

            migrationBuilder.DropTable(
                name: "ConsiderationDependencies",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "ContractCostOverImpacts",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "DocumentDetailUrls",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "EmployerConsiderations",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "ContractCostOvers",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "DocumentDetails",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "EmployerContracts",
                schema: "engineer");

            migrationBuilder.DropIndex(
                name: "IX_ProjectOperations_EmployerContractId",
                schema: "engineer",
                table: "ProjectOperations");

            migrationBuilder.DropIndex(
                name: "IX_EmployerStatusStatements_EmployerContractId",
                schema: "engineer",
                table: "EmployerStatusStatements");

            migrationBuilder.DropColumn(
                name: "EmployerContractId",
                schema: "engineer",
                table: "ProjectOperations");

            migrationBuilder.DropColumn(
                name: "EmployerContractId",
                schema: "engineer",
                table: "EmployerStatusStatements");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "EmployerContractId",
                schema: "engineer",
                table: "ProjectOperations",
                type: "bigint",
                nullable: true);

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

            migrationBuilder.AddColumn<long>(
                name: "EmployerContractId",
                schema: "engineer",
                table: "EmployerStatusStatements",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "EmployerContracts",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    ProjectId = table.Column<long>(type: "bigint", nullable: false),
                    CompanyId = table.Column<long>(type: "bigint", nullable: true),
                    ContractCode = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    CurrencyUnitId = table.Column<long>(type: "bigint", nullable: false),
                    EmployerContractType = table.Column<int>(type: "int", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    PenaltyPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployerContracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployerContracts_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "engineer",
                        principalTable: "Projects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ContractCostOvers",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    CostOverId = table.Column<long>(type: "bigint", nullable: false),
                    EmployerContractId = table.Column<long>(type: "bigint", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Perecent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractCostOvers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractCostOvers_CostOvers_CostOverId",
                        column: x => x.CostOverId,
                        principalSchema: "engineer",
                        principalTable: "CostOvers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ContractCostOvers_EmployerContracts_EmployerContractId",
                        column: x => x.EmployerContractId,
                        principalSchema: "engineer",
                        principalTable: "EmployerContracts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DocumentDetails",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    EmployerContractId = table.Column<long>(type: "bigint", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1500)", nullable: false),
                    DocumentType = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true),
                    Version = table.Column<decimal>(type: "decimal(18,0)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentDetails_EmployerContracts_EmployerContractId",
                        column: x => x.EmployerContractId,
                        principalSchema: "engineer",
                        principalTable: "EmployerContracts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmployerConsiderations",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    EmployerContractId = table.Column<long>(type: "bigint", nullable: false),
                    ConsiderationType = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1500)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployerConsiderations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployerConsiderations_EmployerContracts_EmployerContractId",
                        column: x => x.EmployerContractId,
                        principalSchema: "engineer",
                        principalTable: "EmployerContracts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ContractCostOverImpacts",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    ContractCostOverId = table.Column<long>(type: "bigint", nullable: false),
                    CostOverId = table.Column<long>(type: "bigint", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractCostOverImpacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractCostOverImpacts_ContractCostOvers_ContractCostOverId",
                        column: x => x.ContractCostOverId,
                        principalSchema: "engineer",
                        principalTable: "ContractCostOvers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ContractCostOverImpacts_CostOvers_CostOverId",
                        column: x => x.CostOverId,
                        principalSchema: "engineer",
                        principalTable: "CostOvers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DocumentDetailUrls",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    DocumentDetailId = table.Column<long>(type: "bigint", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    DocumentUrlId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentDetailUrls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentDetailUrls_DocumentDetails_DocumentDetailId",
                        column: x => x.DocumentDetailId,
                        principalSchema: "engineer",
                        principalTable: "DocumentDetails",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ConsiderationDependencies",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    ConsiderationId = table.Column<long>(type: "bigint", nullable: false),
                    OperationInfoId = table.Column<long>(type: "bigint", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsiderationDependencies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConsiderationDependencies_EmployerConsiderations_ConsiderationId",
                        column: x => x.ConsiderationId,
                        principalSchema: "engineer",
                        principalTable: "EmployerConsiderations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConsiderationDependencies_OperationInfos_OperationInfoId",
                        column: x => x.OperationInfoId,
                        principalSchema: "engineer",
                        principalTable: "OperationInfos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectOperations_EmployerContractId",
                schema: "engineer",
                table: "ProjectOperations",
                column: "EmployerContractId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployerStatusStatements_EmployerContractId",
                schema: "engineer",
                table: "EmployerStatusStatements",
                column: "EmployerContractId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsiderationDependencies_ConsiderationId",
                schema: "engineer",
                table: "ConsiderationDependencies",
                column: "ConsiderationId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsiderationDependencies_OperationInfoId",
                schema: "engineer",
                table: "ConsiderationDependencies",
                column: "OperationInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractCostOverImpacts_ContractCostOverId",
                schema: "engineer",
                table: "ContractCostOverImpacts",
                column: "ContractCostOverId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractCostOverImpacts_CostOverId",
                schema: "engineer",
                table: "ContractCostOverImpacts",
                column: "CostOverId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractCostOvers_CostOverId",
                schema: "engineer",
                table: "ContractCostOvers",
                column: "CostOverId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractCostOvers_EmployerContractId",
                schema: "engineer",
                table: "ContractCostOvers",
                column: "EmployerContractId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentDetails_EmployerContractId",
                schema: "engineer",
                table: "DocumentDetails",
                column: "EmployerContractId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentDetailUrls_DocumentDetailId",
                schema: "engineer",
                table: "DocumentDetailUrls",
                column: "DocumentDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployerConsiderations_EmployerContractId",
                schema: "engineer",
                table: "EmployerConsiderations",
                column: "EmployerContractId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployerContracts_ProjectId",
                schema: "engineer",
                table: "EmployerContracts",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployerStatusStatements_EmployerContracts_EmployerContractId",
                schema: "engineer",
                table: "EmployerStatusStatements",
                column: "EmployerContractId",
                principalSchema: "engineer",
                principalTable: "EmployerContracts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectOperations_EmployerContracts_EmployerContractId",
                schema: "engineer",
                table: "ProjectOperations",
                column: "EmployerContractId",
                principalSchema: "engineer",
                principalTable: "EmployerContracts",
                principalColumn: "Id");
        }
    }
}
