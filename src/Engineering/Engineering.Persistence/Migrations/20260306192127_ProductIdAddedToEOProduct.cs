using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ProductIdAddedToEOProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Count",
                schema: "engineer",
                table: "EmployerOperationProducts",
                type: "int",
                nullable: true,
                comment: "تعداد کالا");

            migrationBuilder.AddColumn<long>(
                name: "ProductId",
                schema: "engineer",
                table: "EmployerOperationProducts",
                type: "bigint",
                nullable: true,
                comment: "شناسه کالا");

            migrationBuilder.AddColumn<int>(
                name: "Count",
                schema: "engineer",
                table: "EmployerOperationProductHistories",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ProductId",
                schema: "engineer",
                table: "EmployerOperationProductHistories",
                type: "bigint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                schema: "engineer",
                table: "EmployerOperationProducts");

            migrationBuilder.DropColumn(
                name: "ProductId",
                schema: "engineer",
                table: "EmployerOperationProducts");

            migrationBuilder.DropColumn(
                name: "Count",
                schema: "engineer",
                table: "EmployerOperationProductHistories");

            migrationBuilder.DropColumn(
                name: "ProductId",
                schema: "engineer",
                table: "EmployerOperationProductHistories");
        }
    }
}
