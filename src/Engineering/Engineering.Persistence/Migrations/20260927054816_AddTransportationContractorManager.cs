using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddTransportationContractorManager : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TransportationContractorManagers",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    ThirdPartyId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_TransportationContractorManagers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransportationContractorManagers_TransportationContractors_TransportationContractorId",
                        column: x => x.TransportationContractorId,
                        principalSchema: "engineer",
                        principalTable: "TransportationContractors",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransportationContractorManagers_ThirdPartyId",
                schema: "engineer",
                table: "TransportationContractorManagers",
                column: "ThirdPartyId");

            migrationBuilder.CreateIndex(
                name: "IX_TransportationContractorManagers_TransportationContractorId",
                schema: "engineer",
                table: "TransportationContractorManagers",
                column: "TransportationContractorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransportationContractorManagers",
                schema: "engineer");
        }
    }
}
