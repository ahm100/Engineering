using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultManagerSetToPPAddPPHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "DefaultManagerSet",
                schema: "engineer",
                table: "ProjectProducts",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "تایید دیفالت مدیرپروژه هست یا نه");

            migrationBuilder.CreateTable(
                name: "ProjectProductHistories",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    RequestQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    RemainingQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    CompletedQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    InProgressQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    ProductGroupId = table.Column<long>(type: "bigint", nullable: true, comment: "شناسه گروه کالا"),
                    ProductCategoryId = table.Column<long>(type: "bigint", nullable: true, comment: "شناسه دسته بندی کالا"),
                    TolerancePercentage = table.Column<decimal>(type: "decimal(18,5)", nullable: false, defaultValue: 0m, comment: "درصد تلورانس"),
                    DefaultManagerSet = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "تایید دیفالت مدیرپروژه هست یا نه"),
                    ProjectProductId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "تاریخ ایجاد"),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false, comment: "ایجاد کننده"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "تاریخ ویرایش"),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true, comment: "ویرایش کننده"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "حذف شدگی"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true, comment: "وضعیت فعال یا غیر فعال بودن")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectProductHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectProductHistories_ProjectProducts_ProjectProductId",
                        column: x => x.ProjectProductId,
                        principalSchema: "engineer",
                        principalTable: "ProjectProducts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectProductHistories_ProjectProductId",
                schema: "engineer",
                table: "ProjectProductHistories",
                column: "ProjectProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectProductHistories",
                schema: "engineer");

            migrationBuilder.DropColumn(
                name: "DefaultManagerSet",
                schema: "engineer",
                table: "ProjectProducts");
        }
    }
}
