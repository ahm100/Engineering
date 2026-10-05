using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CreateAdvertisement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Advertisements",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    TitleFa = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false, comment: "عنوان فارسی"),
                    TitleEn = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false, comment: "عنوان انگلیس"),
                    DescriptionFa = table.Column<string>(type: "nvarchar(1500)", nullable: false, comment: "توضیحات فارسی"),
                    DescriptionEn = table.Column<string>(type: "nvarchar(1500)", nullable: false, comment: "توضیحات انگلیسی"),
                    TechnicalCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "کد فنی"),
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
                    table.PrimaryKey("PK_Advertisements", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Advertisements",
                schema: "engineer");
        }
    }
}
