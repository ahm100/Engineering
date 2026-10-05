using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddProjectServiceVolume : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "RequestNumber",
                schema: "engineer",
                table: "RequestContractors",
                type: "bigint",
                nullable: true,
                defaultValueSql: "NEXT VALUE FOR engineer.RequestContractor_RequestNumber",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldDefaultValueSql: "NEXT VALUE FOR engineering.RequestContractor_RequestNumber");

            migrationBuilder.AddColumn<decimal>(
                name: "ProjectServiceVolume",
                schema: "engineer",
                table: "DailyProjectOperationServices",
                type: "decimal(18,5)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProjectServiceVolume",
                schema: "engineer",
                table: "DailyProjectOperationServices");

            migrationBuilder.AlterColumn<long>(
                name: "RequestNumber",
                schema: "engineer",
                table: "RequestContractors",
                type: "bigint",
                nullable: true,
                defaultValueSql: "NEXT VALUE FOR engineering.RequestContractor_RequestNumber",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldDefaultValueSql: "NEXT VALUE FOR engineer.RequestContractor_RequestNumber");
        }
    }
}
