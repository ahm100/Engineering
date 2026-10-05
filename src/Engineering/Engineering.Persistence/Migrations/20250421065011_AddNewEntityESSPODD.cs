using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddNewEntityESSPODD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployerStatusStatementProjectOperationDetailDailies",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    ContractorLength = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ContractorWidth = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ContractorHeight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ContractorWeight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ContractorNumber = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ContractorVolume = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ContractorDescription = table.Column<string>(type: "nvarchar(1500)", nullable: true),
                    SupervisorLength = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SupervisorWidth = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SupervisorHeight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SupervisorWeight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SupervisorNumber = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SupervisorVolume = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SupervisorDescription = table.Column<string>(type: "nvarchar(1500)", nullable: true),
                    ConsultantLength = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ConsultantWidth = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ConsultantHeight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ConsultantWeight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ConsultantNumber = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ConsultantVolume = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ConsultantDescription = table.Column<string>(type: "nvarchar(1500)", nullable: true),
                    EmployerRepresentativeLength = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EmployerRepresentativeWidth = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EmployerRepresentativeHeight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EmployerRepresentativeWeight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EmployerRepresentativeNumber = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EmployerRepresentativeVolume = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EmployerRepresentativeDescription = table.Column<string>(type: "nvarchar(1500)", nullable: true),
                    EmployerStatusStatementProjectOperationDetailId = table.Column<long>(type: "bigint", nullable: false),
                    DailyProjectOperationId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployerStatusStatementProjectOperationDetailDailies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployerStatusStatementProjectOperationDetailDailies_DailyProjectOperations_DailyProjectOperationId",
                        column: x => x.DailyProjectOperationId,
                        principalSchema: "engineer",
                        principalTable: "DailyProjectOperations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmployerStatusStatementProjectOperationDetailDailies_EmployerStatusStatementProjectOperationDetails_EmployerStatusStatementP~",
                        column: x => x.EmployerStatusStatementProjectOperationDetailId,
                        principalSchema: "engineer",
                        principalTable: "EmployerStatusStatementProjectOperationDetails",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployerStatusStatementProjectOperationDetailDailies_DailyProjectOperationId",
                schema: "engineer",
                table: "EmployerStatusStatementProjectOperationDetailDailies",
                column: "DailyProjectOperationId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployerStatusStatementProjectOperationDetailDailies_EmployerStatusStatementProjectOperationDetailId",
                schema: "engineer",
                table: "EmployerStatusStatementProjectOperationDetailDailies",
                column: "EmployerStatusStatementProjectOperationDetailId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployerStatusStatementProjectOperationDetailDailies",
                schema: "engineer");
        }
    }
}
