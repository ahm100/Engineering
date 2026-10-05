using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddLogesticNewEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FromDate",
                schema: "engineer",
                table: "ShippingCosts",
                type: "datetime2",
                nullable: true,
                comment: "از تاریخ");

            migrationBuilder.AddColumn<DateTime>(
                name: "ToDate",
                schema: "engineer",
                table: "ShippingCosts",
                type: "datetime2",
                nullable: true,
                comment: "تا تاریخ");

            migrationBuilder.AlterColumn<Guid>(
                name: "PreferentialReferenceCode",
                schema: "engineer",
                table: "EngineeringServices",
                type: "uniqueidentifier",
                nullable: true,
                comment: "˜Ï ãÑÌÚ ÊÝÕ?á?",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true,
                oldComment: "کد مرجع تفص?ل?");

            migrationBuilder.CreateTable(
                name: "TransportationContractorMachines",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    NumberPlate = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false, comment: "پلاک"),
                    Vin = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true, comment: "شماره شاسی"),
                    Color = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "رنگ"),
                    MachineTypeId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_TransportationContractorMachines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransportationContractorMachines_MachineTypes_MachineTypeId",
                        column: x => x.MachineTypeId,
                        principalSchema: "engineer",
                        principalTable: "MachineTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransportationContractorMachines_TransportationContractors_TransportationContractorId",
                        column: x => x.TransportationContractorId,
                        principalSchema: "engineer",
                        principalTable: "TransportationContractors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransportationContractorPersonnelMachines",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    TransportationContractorPersonnelId = table.Column<long>(type: "bigint", nullable: false),
                    TransportationContractorMachineId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_TransportationContractorPersonnelMachines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransportationContractorPersonnelMachines_TransportationContractorMachines_TransportationContractorMachineId",
                        column: x => x.TransportationContractorMachineId,
                        principalSchema: "engineer",
                        principalTable: "TransportationContractorMachines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransportationContractorPersonnelMachines_TransportationContractorPersonnels_TransportationContractorPersonnelId",
                        column: x => x.TransportationContractorPersonnelId,
                        principalSchema: "engineer",
                        principalTable: "TransportationContractorPersonnels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransportationContractorMachines_MachineTypeId",
                schema: "engineer",
                table: "TransportationContractorMachines",
                column: "MachineTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TransportationContractorMachines_TransportationContractorId",
                schema: "engineer",
                table: "TransportationContractorMachines",
                column: "TransportationContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_TransportationContractorPersonnelMachines_TransportationContractorMachineId",
                schema: "engineer",
                table: "TransportationContractorPersonnelMachines",
                column: "TransportationContractorMachineId");

            migrationBuilder.CreateIndex(
                name: "IX_TransportationContractorPersonnelMachines_TransportationContractorPersonnelId",
                schema: "engineer",
                table: "TransportationContractorPersonnelMachines",
                column: "TransportationContractorPersonnelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransportationContractorPersonnelMachines",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "TransportationContractorMachines",
                schema: "engineer");

            migrationBuilder.DropColumn(
                name: "FromDate",
                schema: "engineer",
                table: "ShippingCosts");

            migrationBuilder.DropColumn(
                name: "ToDate",
                schema: "engineer",
                table: "ShippingCosts");

            migrationBuilder.AlterColumn<Guid>(
                name: "PreferentialReferenceCode",
                schema: "engineer",
                table: "EngineeringServices",
                type: "uniqueidentifier",
                nullable: true,
                comment: "کد مرجع تفص?ل?",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true,
                oldComment: "˜Ï ãÑÌÚ ÊÝÕ?á?");
        }
    }
}
