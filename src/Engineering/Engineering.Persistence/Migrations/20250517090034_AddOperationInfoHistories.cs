using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddOperationInfoHistories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OperationInfoHistories",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    OperationInfoName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    OperationInfoCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OperationLatinName = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    Priority = table.Column<int>(type: "int", nullable: true),
                    UnitOfMeasurementId = table.Column<long>(type: "bigint", nullable: false),
                    HaveStandard = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    OperationInfoId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationInfoHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OperationInfoHistories_OperationInfos_OperationInfoId",
                        column: x => x.OperationInfoId,
                        principalSchema: "engineer",
                        principalTable: "OperationInfos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_OperationInfoHistories_OperationInfoId",
                schema: "engineer",
                table: "OperationInfoHistories",
                column: "OperationInfoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OperationInfoHistories",
                schema: "engineer");
        }
    }
}
