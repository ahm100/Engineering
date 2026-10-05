using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MapTotalAmount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "FinalAmount",
                schema: "engineer",
                table: "ProjectOperationDetails",
                type: "decimal(18,5)",
                nullable: false,
                defaultValue: 1m);

            migrationBuilder.Sql(@"
                UPDATE engineer.ProjectOperationDetails
                SET FinalAmount = 
                    Length *
                    Width *
                    Height *
                    Weight *
                    Number
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinalAmount",
                schema: "engineer",
                table: "ProjectOperationDetails");
        }
    }
}
