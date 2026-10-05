using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CreateEmployerEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "ApprovedBudget",
                schema: "engineer",
                table: "Projects",
                type: "decimal(18,2)",
                nullable: true,
                comment: "بودجه ی مصوب",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,5)",
                oldNullable: true,
                oldComment: "بودجه ی مصوب");

            migrationBuilder.CreateTable(
                name: "EmployerEmployees",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    EmployeeId = table.Column<long>(type: "bigint", nullable: false),
                    EmployerId = table.Column<long>(type: "bigint", nullable: false),
                    CompanyId = table.Column<long>(type: "bigint", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployerEmployees", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployerEmployees",
                schema: "engineer");

            migrationBuilder.AlterColumn<decimal>(
                name: "ApprovedBudget",
                schema: "engineer",
                table: "Projects",
                type: "decimal(18,5)",
                nullable: true,
                comment: "بودجه ی مصوب",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true,
                oldComment: "بودجه ی مصوب");
        }
    }
}
