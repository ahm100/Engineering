using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddConfigsForSessionRecordFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                schema: "engineer",
                table: "SessionRecords");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "SessionDate",
                schema: "engineer",
                table: "SessionRecords",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                comment: "تاریخ جلسه",
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true,
                oldComment: "تاریخ جلسه");

            migrationBuilder.AddColumn<long>(
                name: "ContractId",
                schema: "engineer",
                table: "SessionRecords",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ProjctId",
                schema: "engineer",
                table: "SessionRecords",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SessionCategory",
                schema: "engineer",
                table: "SessionRecords",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "دسته جلسه");

            migrationBuilder.AddColumn<int>(
                name: "SessionType",
                schema: "engineer",
                table: "SessionRecords",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "نوع جلسه");

            migrationBuilder.AddColumn<string>(
                name: "TitleEn",
                schema: "engineer",
                table: "SessionRecords",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "",
                comment: "عنوان انگلیسی");

            migrationBuilder.AddColumn<string>(
                name: "TitleFa",
                schema: "engineer",
                table: "SessionRecords",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "",
                comment: "عنوان فارسی");

            migrationBuilder.CreateIndex(
                name: "IX_SessionRecords_ContractId",
                schema: "engineer",
                table: "SessionRecords",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionRecords_ProjctId",
                schema: "engineer",
                table: "SessionRecords",
                column: "ProjctId");

            migrationBuilder.AddForeignKey(
                name: "FK_SessionRecords_Contracts_ContractId",
                schema: "engineer",
                table: "SessionRecords",
                column: "ContractId",
                principalSchema: "engineer",
                principalTable: "Contracts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SessionRecords_Projects_ProjctId",
                schema: "engineer",
                table: "SessionRecords",
                column: "ProjctId",
                principalSchema: "engineer",
                principalTable: "Projects",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SessionRecords_Contracts_ContractId",
                schema: "engineer",
                table: "SessionRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_SessionRecords_Projects_ProjctId",
                schema: "engineer",
                table: "SessionRecords");

            migrationBuilder.DropIndex(
                name: "IX_SessionRecords_ContractId",
                schema: "engineer",
                table: "SessionRecords");

            migrationBuilder.DropIndex(
                name: "IX_SessionRecords_ProjctId",
                schema: "engineer",
                table: "SessionRecords");

            migrationBuilder.DropColumn(
                name: "ContractId",
                schema: "engineer",
                table: "SessionRecords");

            migrationBuilder.DropColumn(
                name: "ProjctId",
                schema: "engineer",
                table: "SessionRecords");

            migrationBuilder.DropColumn(
                name: "SessionCategory",
                schema: "engineer",
                table: "SessionRecords");

            migrationBuilder.DropColumn(
                name: "SessionType",
                schema: "engineer",
                table: "SessionRecords");

            migrationBuilder.DropColumn(
                name: "TitleEn",
                schema: "engineer",
                table: "SessionRecords");

            migrationBuilder.DropColumn(
                name: "TitleFa",
                schema: "engineer",
                table: "SessionRecords");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "SessionDate",
                schema: "engineer",
                table: "SessionRecords",
                type: "date",
                nullable: true,
                comment: "تاریخ جلسه",
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldComment: "تاریخ جلسه");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                schema: "engineer",
                table: "SessionRecords",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "",
                comment: "عنوان");
        }
    }
}
