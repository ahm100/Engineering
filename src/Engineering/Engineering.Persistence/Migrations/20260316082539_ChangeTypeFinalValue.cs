using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTypeFinalValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "FinalValue",
                schema: "engineer",
                table: "ProjectOperationDetailConsumableVolumeMachineries",
                type: "decimal(18,3)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldDefaultValue: 36000000000L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "FinalValue",
                schema: "engineer",
                table: "ProjectOperationDetailConsumableVolumeMachineries",
                type: "bigint",
                nullable: false,
                defaultValue: 36000000000L,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,3)");
        }
    }
}
