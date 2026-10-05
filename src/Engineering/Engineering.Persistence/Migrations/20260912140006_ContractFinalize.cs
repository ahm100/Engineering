using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ContractFinalize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "EnTitle",
                schema: "engineer",
                table: "Contracts",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                comment: "عنوان انگلیسی",
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldComment: "عنوان انگلیسی");

            migrationBuilder.AlterColumn<string>(
                name: "EnTitle",
                schema: "engineer",
                table: "ContractLegalSnapshots",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                comment: "عنوان انگلیسی",
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldComment: "عنوان انگلیسی");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "EnTitle",
                schema: "engineer",
                table: "Contracts",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "",
                comment: "عنوان انگلیسی",
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldNullable: true,
                oldComment: "عنوان انگلیسی");

            migrationBuilder.AlterColumn<string>(
                name: "EnTitle",
                schema: "engineer",
                table: "ContractLegalSnapshots",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "",
                comment: "عنوان انگلیسی",
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldNullable: true,
                oldComment: "عنوان انگلیسی");
        }
    }
}
