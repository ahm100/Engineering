using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddAdjustmentTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdjustmentReference",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdjustmentReference", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AdjustmentIndexes",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    AdjustmentReferenceId = table.Column<long>(type: "bigint", nullable: false),
                    YearId = table.Column<long>(type: "bigint", nullable: false),
                    SeasonId = table.Column<long>(type: "bigint", nullable: true),
                    BranchId = table.Column<long>(type: "bigint", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "کد"),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false, comment: "عنوان فارسی"),
                    Description = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: true, comment: "توضیحات"),
                    DocumentFile = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "تاریخ ایجاد"),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false, comment: "ایجاد کننده"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "تاریخ ویرایش"),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true, comment: "ویرایش کننده"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "حذف شدگی"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdjustmentIndexes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdjustmentIndexes_AdjustmentReference_AdjustmentReferenceId",
                        column: x => x.AdjustmentReferenceId,
                        principalSchema: "engineer",
                        principalTable: "AdjustmentReference",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdjustmentIndexes_EngineeringBranchs_BranchId",
                        column: x => x.BranchId,
                        principalSchema: "engineer",
                        principalTable: "EngineeringBranchs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdjustmentIndexes_EngineeringSeasons_SeasonId",
                        column: x => x.SeasonId,
                        principalSchema: "engineer",
                        principalTable: "EngineeringSeasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AdjustmentIndexValues",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    AdjustmentIndexId = table.Column<long>(type: "bigint", nullable: false),
                    YearName = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Period = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    Coefficient = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: true),
                    NotificationNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NotificationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "تاریخ ایجاد"),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false, comment: "ایجاد کننده"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "تاریخ ویرایش"),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true, comment: "ویرایش کننده"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "حذف شدگی"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, comment: "وضعیت فعال یا غیر فعال بودن")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdjustmentIndexValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdjustmentIndexValues_AdjustmentIndexes_AdjustmentIndexId",
                        column: x => x.AdjustmentIndexId,
                        principalSchema: "engineer",
                        principalTable: "AdjustmentIndexes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdjustmentIndexes_AdjustmentReferenceId",
                schema: "engineer",
                table: "AdjustmentIndexes",
                column: "AdjustmentReferenceId");

            migrationBuilder.CreateIndex(
                name: "IX_AdjustmentIndexes_BranchId",
                schema: "engineer",
                table: "AdjustmentIndexes",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_AdjustmentIndexes_SeasonId",
                schema: "engineer",
                table: "AdjustmentIndexes",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_AdjustmentIndexValues_AdjustmentIndexId",
                schema: "engineer",
                table: "AdjustmentIndexValues",
                column: "AdjustmentIndexId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdjustmentIndexValues",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "AdjustmentIndexes",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "AdjustmentReference",
                schema: "engineer");
        }
    }
}
