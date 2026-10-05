using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemovingPayementBasis : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentBasis",
                schema: "engineer",
                table: "ContractTypes");

            migrationBuilder.RenameColumn(
                name: "IsSubjectToAmendment",
                schema: "engineer",
                table: "ContractTypes",
                newName: "IsSubjectToAdjustment");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsSubjectToAdjustment",
                schema: "engineer",
                table: "ContractTypes",
                newName: "IsSubjectToAmendment");

            migrationBuilder.AddColumn<int>(
                name: "PaymentBasis",
                schema: "engineer",
                table: "ContractTypes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "مبنای پرداخت");
        }
    }
}
