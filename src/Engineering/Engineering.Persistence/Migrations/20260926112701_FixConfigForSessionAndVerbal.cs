using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixConfigForSessionAndVerbal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ContractNum",
                schema: "engineer",
                table: "SessionRecords",
                newName: "ContractNumber");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                schema: "engineer",
                table: "SessionRecords",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                comment: "عنوان",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldComment: "عنوان");

            migrationBuilder.AlterColumn<string>(
                name: "ProjectName",
                schema: "engineer",
                table: "SessionRecords",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                comment: "نام پروژه",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true,
                oldComment: "نام پروژه");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "engineer",
                table: "SessionRecordActions",
                type: "nvarchar(1500)",
                maxLength: 1500,
                nullable: false,
                comment: "توضیحات",
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldComment: "شرح اقدامات و تصمیمات");

            migrationBuilder.AlterColumn<string>(
                name: "Descriotion",
                schema: "engineer",
                table: "SessionItems",
                type: "nvarchar(1500)",
                maxLength: 1500,
                nullable: false,
                comment: "توضیحات",
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldComment: "توضیحات");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                schema: "engineer",
                table: "SessionInvitees",
                type: "int",
                nullable: false,
                comment: "وضعیت",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "وضعیت مدعو جلسه");

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                schema: "engineer",
                table: "SessionInvitees",
                type: "bigint",
                nullable: true,
                comment: "مسیول اقدام");

            migrationBuilder.AlterColumn<string>(
                name: "TitleFa",
                schema: "engineer",
                table: "ProcesVerbals",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                comment: "عنوان",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldComment: "عنوان صورت مجلس");

            migrationBuilder.AlterColumn<string>(
                name: "TitleEn",
                schema: "engineer",
                table: "ProcesVerbals",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                comment: "Title",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true,
                oldComment: "عنوان صورت مجلس");

            migrationBuilder.AlterColumn<decimal>(
                name: "NewFinalAmount",
                schema: "engineer",
                table: "ProcesVerbalPODs",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                comment: "مقدار نهایی جدید",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4,
                oldComment: "مقدار نهایی جدید");

            migrationBuilder.AlterColumn<string>(
                name: "TitleFa",
                schema: "engineer",
                table: "ProcesVerbalItems",
                type: "nvarchar(1500)",
                maxLength: 500,
                nullable: false,
                comment: "شرح مفاد صورت مجلس",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldComment: "عنوان صورت مجلس");

            migrationBuilder.AlterColumn<decimal>(
                name: "NewFinalValue",
                schema: "engineer",
                table: "ProcesVerbalEquipments",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                comment: "مقدار نهایی جدید",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4,
                oldComment: "مقدار نهایی جدید");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "engineer",
                table: "ProcesVerbalEquipments",
                type: "nvarchar(1500)",
                maxLength: 1500,
                nullable: true,
                comment: "توضیحات",
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true,
                oldComment: "توضیحات");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "engineer",
                table: "SessionInvitees");

            migrationBuilder.RenameColumn(
                name: "ContractNumber",
                schema: "engineer",
                table: "SessionRecords",
                newName: "ContractNum");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                schema: "engineer",
                table: "SessionRecords",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                comment: "عنوان",
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldComment: "عنوان");

            migrationBuilder.AlterColumn<string>(
                name: "ProjectName",
                schema: "engineer",
                table: "SessionRecords",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                comment: "نام پروژه",
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldNullable: true,
                oldComment: "نام پروژه");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "engineer",
                table: "SessionRecordActions",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                comment: "شرح اقدامات و تصمیمات",
                oldClrType: typeof(string),
                oldType: "nvarchar(1500)",
                oldMaxLength: 1500,
                oldComment: "توضیحات");

            migrationBuilder.AlterColumn<string>(
                name: "Descriotion",
                schema: "engineer",
                table: "SessionItems",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                comment: "توضیحات",
                oldClrType: typeof(string),
                oldType: "nvarchar(1500)",
                oldMaxLength: 1500,
                oldComment: "توضیحات");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                schema: "engineer",
                table: "SessionInvitees",
                type: "int",
                nullable: false,
                comment: "وضعیت مدعو جلسه",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "وضعیت");

            migrationBuilder.AlterColumn<string>(
                name: "TitleFa",
                schema: "engineer",
                table: "ProcesVerbals",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                comment: "عنوان صورت مجلس",
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldComment: "عنوان");

            migrationBuilder.AlterColumn<string>(
                name: "TitleEn",
                schema: "engineer",
                table: "ProcesVerbals",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                comment: "عنوان صورت مجلس",
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldNullable: true,
                oldComment: "Title");

            migrationBuilder.AlterColumn<decimal>(
                name: "NewFinalAmount",
                schema: "engineer",
                table: "ProcesVerbalPODs",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                comment: "مقدار نهایی جدید",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2,
                oldComment: "مقدار نهایی جدید");

            migrationBuilder.AlterColumn<string>(
                name: "TitleFa",
                schema: "engineer",
                table: "ProcesVerbalItems",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                comment: "عنوان صورت مجلس",
                oldClrType: typeof(string),
                oldType: "nvarchar(1500)",
                oldMaxLength: 500,
                oldComment: "شرح مفاد صورت مجلس");

            migrationBuilder.AlterColumn<decimal>(
                name: "NewFinalValue",
                schema: "engineer",
                table: "ProcesVerbalEquipments",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                comment: "مقدار نهایی جدید",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2,
                oldComment: "مقدار نهایی جدید");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "engineer",
                table: "ProcesVerbalEquipments",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                comment: "توضیحات",
                oldClrType: typeof(string),
                oldType: "nvarchar(1500)",
                oldMaxLength: 1500,
                oldNullable: true,
                oldComment: "توضیحات");
        }
    }
}
