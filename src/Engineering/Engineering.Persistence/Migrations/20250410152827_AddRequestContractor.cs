using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddRequestContractor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RequestContractors",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    RequestNumber = table.Column<long>(type: "bigint", nullable: true, defaultValueSql: "NEXT VALUE FOR engineer.RequestContractor_RequestNumber"),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Volume = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    StatusDescription = table.Column<string>(type: "nvarchar(1500)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1500)", nullable: true),
                    ProjectOperationDetailId = table.Column<long>(type: "bigint", nullable: false),
                    ServiceInfoId = table.Column<long>(type: "bigint", nullable: false),
                    CompanyId = table.Column<long>(type: "bigint", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestContractors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestContractors_EngineeringServices_ServiceInfoId",
                        column: x => x.ServiceInfoId,
                        principalSchema: "engineer",
                        principalTable: "EngineeringServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RequestContractors_ProjectOperationDetails_ProjectOperationDetailId",
                        column: x => x.ProjectOperationDetailId,
                        principalSchema: "engineer",
                        principalTable: "ProjectOperationDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RequestContractorHistories",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Volume = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    StatusDescription = table.Column<string>(type: "nvarchar(1500)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1500)", nullable: true),
                    RequestContractorId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestContractorHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestContractorHistories_RequestContractors_RequestContractorId",
                        column: x => x.RequestContractorId,
                        principalSchema: "engineer",
                        principalTable: "RequestContractors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RequestContractorInquiries",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    ContractorId = table.Column<long>(type: "bigint", nullable: false),
                    CurrencyId = table.Column<long>(type: "bigint", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Tax = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    FromDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ToDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ConfirmedUser = table.Column<long>(type: "bigint", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1500)", nullable: true),
                    RequestContractorId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestContractorInquiries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestContractorInquiries_RequestContractors_RequestContractorId",
                        column: x => x.RequestContractorId,
                        principalSchema: "engineer",
                        principalTable: "RequestContractors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RequestContractorInquiryDocuments",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequestContractorInquiryId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestContractorInquiryDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestContractorInquiryDocuments_RequestContractorInquiries_RequestContractorInquiryId",
                        column: x => x.RequestContractorInquiryId,
                        principalSchema: "engineer",
                        principalTable: "RequestContractorInquiries",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RequestContractorHistories_RequestContractorId",
                schema: "engineer",
                table: "RequestContractorHistories",
                column: "RequestContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestContractorInquiries_RequestContractorId",
                schema: "engineer",
                table: "RequestContractorInquiries",
                column: "RequestContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestContractorInquiryDocuments_RequestContractorInquiryId",
                schema: "engineer",
                table: "RequestContractorInquiryDocuments",
                column: "RequestContractorInquiryId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestContractors_ProjectOperationDetailId",
                schema: "engineer",
                table: "RequestContractors",
                column: "ProjectOperationDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestContractors_ServiceInfoId",
                schema: "engineer",
                table: "RequestContractors",
                column: "ServiceInfoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequestContractorHistories",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "RequestContractorInquiryDocuments",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "RequestContractorInquiries",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "RequestContractors",
                schema: "engineer");
        }
    }
}
