using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CreateEngCodingConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HaveCodingAlgorithm",
                schema: "engineer",
                table: "EngineeringConfigs",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "کلید تنظیمات دسترسی کدگذاری پروژه");

            migrationBuilder.CreateTable(
                name: "EngineeringCodingConfigs",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    EngineeringConfigId = table.Column<long>(type: "bigint", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false, comment: "نوع موجودیت الگوریتم کد گذاری"),
                    Prefix = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "پیشوند"),
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
                    table.PrimaryKey("PK_EngineeringCodingConfigs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EngineeringCodingConfigs_EngineeringConfigs_EngineeringConfigId",
                        column: x => x.EngineeringConfigId,
                        principalSchema: "engineer",
                        principalTable: "EngineeringConfigs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EngineeringCodingConfigs_EngineeringConfigId",
                schema: "engineer",
                table: "EngineeringCodingConfigs",
                column: "EngineeringConfigId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EngineeringCodingConfigs",
                schema: "engineer");

            migrationBuilder.DropColumn(
                name: "HaveCodingAlgorithm",
                schema: "engineer",
                table: "EngineeringConfigs");
        }
    }
}
