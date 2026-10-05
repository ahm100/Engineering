using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddAdvServiceDocuments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CompanyId",
                schema: "engineer",
                table: "Advertisements",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AdvertisementDocument",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdvertisementId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvertisementDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdvertisementDocument_Advertisements_AdvertisementId",
                        column: x => x.AdvertisementId,
                        principalSchema: "engineer",
                        principalTable: "Advertisements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceInfoDocument",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServiceInfoId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceInfoDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceInfoDocument_EngineeringServices_ServiceInfoId",
                        column: x => x.ServiceInfoId,
                        principalSchema: "engineer",
                        principalTable: "EngineeringServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdvertisementDocument_AdvertisementId",
                schema: "engineer",
                table: "AdvertisementDocument",
                column: "AdvertisementId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceInfoDocument_ServiceInfoId",
                schema: "engineer",
                table: "ServiceInfoDocument",
                column: "ServiceInfoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdvertisementDocument",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "ServiceInfoDocument",
                schema: "engineer");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                schema: "engineer",
                table: "Advertisements");
        }
    }
}
