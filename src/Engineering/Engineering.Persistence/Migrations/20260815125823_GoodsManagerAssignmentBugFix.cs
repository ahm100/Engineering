using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class GoodsManagerAssignmentBugFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GoodsManagerAssignments",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    ProductCategoryId = table.Column<long>(type: "bigint", nullable: true, comment: "دسته بندی محصول"),
                    ProductGroupId = table.Column<long>(type: "bigint", nullable: true, comment: "گروه محصول"),
                    ProductId = table.Column<long>(type: "bigint", nullable: true, comment: "محصول"),
                    GoodsManagerThirdPartyId = table.Column<long>(type: "bigint", nullable: false, comment: "مدیر کالا"),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "تاریخ ایجاد"),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false, comment: "ایجاد کننده"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "تاریخ ویرایش"),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true, comment: "ویرایش کننده"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "حذف شدگی")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsManagerAssignments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GoodsManagerAssignmentHistory",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductCategoryId = table.Column<long>(type: "bigint", nullable: true),
                    ProductGroupId = table.Column<long>(type: "bigint", nullable: true),
                    ProductId = table.Column<long>(type: "bigint", nullable: true),
                    GoodsManagerThirdPartyId = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GoodsManagerAssignmentId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsManagerAssignmentHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GoodsManagerAssignmentHistory_GoodsManagerAssignments_GoodsManagerAssignmentId",
                        column: x => x.GoodsManagerAssignmentId,
                        principalSchema: "engineer",
                        principalTable: "GoodsManagerAssignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GoodsManagerAssignmentHistory_GoodsManagerAssignmentId",
                schema: "engineer",
                table: "GoodsManagerAssignmentHistory",
                column: "GoodsManagerAssignmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GoodsManagerAssignmentHistory",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "GoodsManagerAssignments",
                schema: "engineer");
        }
    }
}
