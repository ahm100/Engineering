using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddTransportationContractor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TransportationContractors",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    ThirdPartyId = table.Column<long>(type: "bigint", nullable: false),
                    StartOfContract = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndOfContract = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه کمپانی"),
                    LegacyId = table.Column<long>(type: "bigint", nullable: true),
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
                    table.PrimaryKey("PK_TransportationContractors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransportationContractorDocuments",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    DocumentUrl = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "مسیر فایل"),
                    TransportationContractorId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_TransportationContractorDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransportationContractorDocuments_TransportationContractors_TransportationContractorId",
                        column: x => x.TransportationContractorId,
                        principalSchema: "engineer",
                        principalTable: "TransportationContractors",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TransportationContractorPersonnels",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    ThirdPartyId = table.Column<long>(type: "bigint", nullable: false),
                    LegacyId = table.Column<long>(type: "bigint", nullable: true),
                    TransportationContractorId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_TransportationContractorPersonnels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransportationContractorPersonnels_TransportationContractors_TransportationContractorId",
                        column: x => x.TransportationContractorId,
                        principalSchema: "engineer",
                        principalTable: "TransportationContractors",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransportationContractorDocuments_TransportationContractorId",
                schema: "engineer",
                table: "TransportationContractorDocuments",
                column: "TransportationContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_TransportationContractorPersonnels_ThirdPartyId",
                schema: "engineer",
                table: "TransportationContractorPersonnels",
                column: "ThirdPartyId");

            migrationBuilder.CreateIndex(
                name: "IX_TransportationContractorPersonnels_TransportationContractorId",
                schema: "engineer",
                table: "TransportationContractorPersonnels",
                column: "TransportationContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_TransportationContractors_ThirdPartyId",
                schema: "engineer",
                table: "TransportationContractors",
                column: "ThirdPartyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransportationContractorDocuments",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "TransportationContractorPersonnels",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "TransportationContractors",
                schema: "engineer");
        }
    }
}
