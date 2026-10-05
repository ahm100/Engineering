using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddNewContractAggregate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "Contract_ContractNumber",
                schema: "engineer");

            migrationBuilder.CreateTable(
                name: "Contracts",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    ContractNumber = table.Column<long>(type: "bigint", nullable: true, defaultValueSql: "NEXT VALUE FOR engineer.Contract_ContractNumber", comment: "شماره قرارداد"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "عنوان"),
                    ProjectId = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه پروژه"),
                    ContractPartyId = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه طرف قرارداد"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "تاریخ شروع"),
                    Duration = table.Column<int>(type: "int", nullable: false, comment: "مدت زمان"),
                    DurationUnit = table.Column<int>(type: "int", nullable: false, comment: "واحد مدت زمان"),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "تاریخ پایان"),
                    Status = table.Column<int>(type: "int", nullable: false, comment: "وضعیت"),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه کمپانی"),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "تاریخ ایجاد"),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false, comment: "ایجاد کننده"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "تاریخ ویرایش"),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true, comment: "ویرایش کننده"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "حذف شدگی")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contracts_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "engineer",
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_ContractNumber",
                schema: "engineer",
                table: "Contracts",
                column: "ContractNumber",
                unique: true,
                filter: "[ContractNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_ProjectId",
                schema: "engineer",
                table: "Contracts",
                column: "ProjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contracts",
                schema: "engineer");

            migrationBuilder.DropSequence(
                name: "Contract_ContractNumber",
                schema: "engineer");
        }
    }
}
