using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RegistrationTargetStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RegistrationTargetStatus",
                schema: "engineer",
                table: "Contracts",
                type: "int",
                nullable: true,
                comment: "وضعیت هدف ثبت قرارداد");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegistrationTargetStatus",
                schema: "engineer",
                table: "Contracts");
        }
    }
}
