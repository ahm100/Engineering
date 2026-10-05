using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ModifyEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "EmployerCommercialConfirmeTotalPrice",
                schema: "engineer",
                table: "EmployerStatusStatementProjectOperations",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1500)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "engineer",
                table: "EmployerStatusStatementProjectOperations",
                type: "nvarchar(1500)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "engineer",
                table: "EmployerStatusStatementProjectOperationDetails",
                type: "nvarchar(1500)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1500)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "EmployerCommercialConfirmeTotalPrice",
                schema: "engineer",
                table: "EmployerStatusStatementProjectOperations",
                type: "nvarchar(1500)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "engineer",
                table: "EmployerStatusStatementProjectOperations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1500)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "engineer",
                table: "EmployerStatusStatementProjectOperationDetails",
                type: "nvarchar(1500)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(1500)",
                oldNullable: true);
        }
    }
}
