using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemoveRGSProductScale : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequestGoodsSupplyProductScales",
                schema: "engineer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RequestGoodsSupplyProductScales",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    RequestGoodsSupplyProductId = table.Column<long>(type: "bigint", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Destination = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    DriverName = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    GoodsType = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    GrossWeight = table.Column<int>(type: "int", nullable: true),
                    ImageHash = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    ImageName = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    NetWeight = table.Column<int>(type: "int", nullable: true),
                    Origin = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    PlateNumber = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    ReceiptNumber = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    RemainingWeight = table.Column<int>(type: "int", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    ScaleDateTime = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestGoodsSupplyProductScales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestGoodsSupplyProductScales_RequestGoodsSupplyProducts_RequestGoodsSupplyProductId",
                        column: x => x.RequestGoodsSupplyProductId,
                        principalSchema: "engineer",
                        principalTable: "RequestGoodsSupplyProducts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RequestGoodsSupplyProductScales_RequestGoodsSupplyProductId",
                schema: "engineer",
                table: "RequestGoodsSupplyProductScales",
                column: "RequestGoodsSupplyProductId");
        }
    }
}
