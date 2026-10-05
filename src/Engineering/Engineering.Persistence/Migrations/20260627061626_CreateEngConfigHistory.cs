using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CreateEngConfigHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EngineeringConfigHistories",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    SendTelegramMessage = table.Column<bool>(type: "bit", nullable: false, comment: "کلید تنظیمات برای کنترل ارسال پیام به تلگرام"),
                    ProjectThirdParties = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "کلید تنظیمات دسترسی پروژه"),
                    EngineeringConfigId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_EngineeringConfigHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EngineeringConfigHistories_EngineeringConfigs_EngineeringConfigId",
                        column: x => x.EngineeringConfigId,
                        principalSchema: "engineer",
                        principalTable: "EngineeringConfigs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EngineeringConfigHistories_EngineeringConfigId",
                schema: "engineer",
                table: "EngineeringConfigHistories",
                column: "EngineeringConfigId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EngineeringConfigHistories",
                schema: "engineer");
        }
    }
}
