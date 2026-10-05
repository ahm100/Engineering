using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddCargoDocs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "SecurityConfirm",
                schema: "engineer",
                table: "TransportationCargos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "SecurityConfirmDate",
                schema: "engineer",
                table: "TransportationCargos",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TransportationCargoDocuments",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransportationCargoId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportationCargoDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransportationCargoDocuments_TransportationCargos_TransportationCargoId",
                        column: x => x.TransportationCargoId,
                        principalSchema: "engineer",
                        principalTable: "TransportationCargos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransportationCargoDocuments_TransportationCargoId",
                schema: "engineer",
                table: "TransportationCargoDocuments",
                column: "TransportationCargoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransportationCargoDocuments",
                schema: "engineer");

            migrationBuilder.DropColumn(
                name: "SecurityConfirm",
                schema: "engineer",
                table: "TransportationCargos");

            migrationBuilder.DropColumn(
                name: "SecurityConfirmDate",
                schema: "engineer",
                table: "TransportationCargos");
        }
    }
}
