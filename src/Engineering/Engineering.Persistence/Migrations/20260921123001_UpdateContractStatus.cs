using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateContractStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegistrationTargetStatus",
                schema: "engineer",
                table: "Contracts");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "engineer",
                table: "ContractStatusHistories",
                type: "nvarchar(max)",
                nullable: true,
                comment: "توضیحات");

            migrationBuilder.AddColumn<DateTime>(
                name: "EffectiveDate",
                schema: "engineer",
                table: "ContractStatusHistories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "تاریخ اثرگذاری");

            migrationBuilder.AddColumn<string>(
                name: "Reason",
                schema: "engineer",
                table: "ContractStatusHistories",
                type: "nvarchar(max)",
                nullable: true,
                comment: "علت");

            migrationBuilder.AddColumn<int>(
                name: "SuspensionDurationMonths",
                schema: "engineer",
                table: "ContractStatusHistories",
                type: "int",
                nullable: true,
                comment: "مدت تعلیق به ماه");

            migrationBuilder.AddColumn<int>(
                name: "TransitionType",
                schema: "engineer",
                table: "ContractStatusHistories",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "نوع عملیات تغییر وضعیت");

            migrationBuilder.AddColumn<bool>(
                name: "IsRegistrationPending",
                schema: "engineer",
                table: "Contracts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "ContractStatusHistoryDocuments",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    ContractStatusHistoryId = table.Column<long>(type: "bigint", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: false, comment: "مسیر فایل"),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "تاریخ ایجاد"),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false, comment: "ایجاد کننده"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "تاریخ ویرایش"),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true, comment: "ویرایش کننده"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "حذف شدگی")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractStatusHistoryDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractStatusHistoryDocuments_ContractStatusHistories_ContractStatusHistoryId",
                        column: x => x.ContractStatusHistoryId,
                        principalSchema: "engineer",
                        principalTable: "ContractStatusHistories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContractStatusHistoryDocuments_ContractStatusHistoryId",
                schema: "engineer",
                table: "ContractStatusHistoryDocuments",
                column: "ContractStatusHistoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContractStatusHistoryDocuments",
                schema: "engineer");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "engineer",
                table: "ContractStatusHistories");

            migrationBuilder.DropColumn(
                name: "EffectiveDate",
                schema: "engineer",
                table: "ContractStatusHistories");

            migrationBuilder.DropColumn(
                name: "Reason",
                schema: "engineer",
                table: "ContractStatusHistories");

            migrationBuilder.DropColumn(
                name: "SuspensionDurationMonths",
                schema: "engineer",
                table: "ContractStatusHistories");

            migrationBuilder.DropColumn(
                name: "TransitionType",
                schema: "engineer",
                table: "ContractStatusHistories");

            migrationBuilder.DropColumn(
                name: "IsRegistrationPending",
                schema: "engineer",
                table: "Contracts");

            migrationBuilder.AddColumn<int>(
                name: "RegistrationTargetStatus",
                schema: "engineer",
                table: "Contracts",
                type: "int",
                nullable: true,
                comment: "وضعیت هدف ثبت قرارداد");
        }
    }
}
