using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddDailyProjectOperationHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DailyProjectOperationHistories",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    LegacyId = table.Column<long>(type: "bigint", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Length = table.Column<decimal>(type: "decimal(18,5)", nullable: false, defaultValue: 0m),
                    Width = table.Column<decimal>(type: "decimal(18,5)", nullable: false, defaultValue: 0m),
                    Height = table.Column<decimal>(type: "decimal(18,5)", nullable: false, defaultValue: 0m),
                    Weight = table.Column<decimal>(type: "decimal(18,5)", nullable: false, defaultValue: 0m),
                    Number = table.Column<decimal>(type: "decimal(18,5)", nullable: false, defaultValue: 0m),
                    CompanyId = table.Column<long>(type: "bigint", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1500)", nullable: true),
                    DailyProjectOperationId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "تاریخ ایجاد"),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false, comment: "ایجاد کننده"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "تاریخ ویرایش"),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true, comment: "ویرایش کننده"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "حذف شدگی")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyProjectOperationHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyProjectOperationHistories_DailyProjectOperations_DailyProjectOperationId",
                        column: x => x.DailyProjectOperationId,
                        principalSchema: "engineer",
                        principalTable: "DailyProjectOperations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DailyProjectOperationHistories_DailyProjectOperationId",
                schema: "engineer",
                table: "DailyProjectOperationHistories",
                column: "DailyProjectOperationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailyProjectOperationHistories",
                schema: "engineer");
        }
    }
}
