using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ModifyForPayments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContractorStatusStatementPayments_ContractorStatusStatements_ContractorStatusStatementId",
                schema: "engineer",
                table: "ContractorStatusStatementPayments");

            migrationBuilder.DropColumn(
                name: "FinalManagerConfirmedAmount",
                schema: "engineer",
                table: "ContractorStatusStatementPayments");

            migrationBuilder.DropColumn(
                name: "IsPaid",
                schema: "engineer",
                table: "ContractorStatusStatementPayments");

            migrationBuilder.DropColumn(
                name: "ManagerConfirmedAmount",
                schema: "engineer",
                table: "ContractorStatusStatementPayments");

            migrationBuilder.DropColumn(
                name: "PrimaryManagerConfirmedAmount",
                schema: "engineer",
                table: "ContractorStatusStatementPayments");

            migrationBuilder.AlterColumn<long>(
                name: "UpdaterId",
                schema: "engineer",
                table: "ContractorStatusStatementPayments",
                type: "bigint",
                nullable: true,
                comment: "ویرایش کننده",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                schema: "engineer",
                table: "ContractorStatusStatementPayments",
                type: "datetime2",
                nullable: true,
                comment: "تاریخ ویرایش",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TreasuryPaid",
                schema: "engineer",
                table: "ContractorStatusStatementPayments",
                type: "decimal(18,2)",
                nullable: true,
                comment: "مبلغ پرداخت شده توسط خزانه",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "PaymentOrderId",
                schema: "engineer",
                table: "ContractorStatusStatementPayments",
                type: "bigint",
                nullable: true,
                comment: "شناسه پرداخت",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "PaymentDate",
                schema: "engineer",
                table: "ContractorStatusStatementPayments",
                type: "datetime",
                nullable: true,
                comment: "تاریخ پرداخت",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                schema: "engineer",
                table: "ContractorStatusStatementPayments",
                type: "bit",
                nullable: false,
                comment: "حذف شدگی",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<long>(
                name: "CreatorId",
                schema: "engineer",
                table: "ContractorStatusStatementPayments",
                type: "bigint",
                nullable: false,
                comment: "ایجاد کننده",
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                schema: "engineer",
                table: "ContractorStatusStatementPayments",
                type: "datetime2",
                nullable: false,
                comment: "تاریخ ایجاد",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<long>(
                name: "ContractorStatusStatementId",
                schema: "engineer",
                table: "ContractorStatusStatementPayments",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                schema: "engineer",
                table: "ContractorStatusStatementPayments",
                type: "bigint",
                nullable: false,
                comment: "شناسه",
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("SqlServer:Identity", "2, 1")
                .OldAnnotation("SqlServer:Identity", "2, 1");

            migrationBuilder.AddColumn<decimal>(
                name: "CalculatedAmount",
                schema: "engineer",
                table: "ContractorStatusStatementPayments",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                comment: "مبلغ محاسبه شده");

            migrationBuilder.AddColumn<decimal>(
                name: "FinalManagerAmount",
                schema: "engineer",
                table: "ContractorStatusStatementPayments",
                type: "decimal(18,2)",
                nullable: true,
                comment: "مبلغ تایید مدیر پایانی");

            migrationBuilder.AddColumn<string>(
                name: "FinalManagerDescription",
                schema: "engineer",
                table: "ContractorStatusStatementPayments",
                type: "nvarchar(1500)",
                nullable: true,
                comment: "توضیحات تایید مدیر پایانی");

            migrationBuilder.AddColumn<decimal>(
                name: "ManagementAmount",
                schema: "engineer",
                table: "ContractorStatusStatementPayments",
                type: "decimal(18,2)",
                nullable: true,
                comment: "مبلغ تایید کارشناس ارشد");

            migrationBuilder.AddColumn<string>(
                name: "ManagementDescription",
                schema: "engineer",
                table: "ContractorStatusStatementPayments",
                type: "nvarchar(1500)",
                nullable: true,
                comment: "توضیحات تایید کارشناس ارشد");

            migrationBuilder.AddColumn<decimal>(
                name: "PayableAmount",
                schema: "engineer",
                table: "ContractorStatusStatementPayments",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                comment: "مبلغ قابل پرداخت");

            migrationBuilder.AddColumn<decimal>(
                name: "PaymentAmount",
                schema: "engineer",
                table: "ContractorStatusStatementPayments",
                type: "decimal(18,2)",
                nullable: true,
                comment: "مبلغ پرداخت");

            migrationBuilder.AddColumn<string>(
                name: "PaymentDescription",
                schema: "engineer",
                table: "ContractorStatusStatementPayments",
                type: "nvarchar(1500)",
                nullable: true,
                comment: "توضیح پرداخت");

            migrationBuilder.AddColumn<decimal>(
                name: "PrimaryManagerAmount",
                schema: "engineer",
                table: "ContractorStatusStatementPayments",
                type: "decimal(18,2)",
                nullable: true,
                comment: "مبلغ تایید مدیر اولیه");

            migrationBuilder.AddColumn<string>(
                name: "PrimaryManagerDescription",
                schema: "engineer",
                table: "ContractorStatusStatementPayments",
                type: "nvarchar(1500)",
                nullable: true,
                comment: "توضیحات تایید مدیر اولیه");

            migrationBuilder.AddColumn<decimal>(
                name: "ProjectManagerAmount",
                schema: "engineer",
                table: "ContractorStatusStatementPayments",
                type: "decimal(18,2)",
                nullable: true,
                comment: "مبلغ تایید مدیر پروژه");

            migrationBuilder.AddColumn<string>(
                name: "ProjectManagerDescription",
                schema: "engineer",
                table: "ContractorStatusStatementPayments",
                type: "nvarchar(1500)",
                nullable: true,
                comment: "توضیحات تایید مدیر پروژه");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                schema: "engineer",
                table: "ContractorStatusStatementPayments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "UserAmount",
                schema: "engineer",
                table: "ContractorStatusStatementPayments",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                comment: "مبلغ تایید کاربر");

            migrationBuilder.AddColumn<string>(
                name: "UserDescription",
                schema: "engineer",
                table: "ContractorStatusStatementPayments",
                type: "nvarchar(1500)",
                nullable: true,
                comment: "توضیحات تایید کاربر");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractorStatusStatementPayments_ContractorStatusStatements_ContractorStatusStatementId",
                schema: "engineer",
                table: "ContractorStatusStatementPayments",
                column: "ContractorStatusStatementId",
                principalSchema: "engineer",
                principalTable: "ContractorStatusStatements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContractorStatusStatementPayments_ContractorStatusStatements_ContractorStatusStatementId",
                schema: "engineer",
                table: "ContractorStatusStatementPayments");

            migrationBuilder.DropColumn(
                name: "CalculatedAmount",
                schema: "engineer",
                table: "ContractorStatusStatementPayments");

            migrationBuilder.DropColumn(
                name: "FinalManagerAmount",
                schema: "engineer",
                table: "ContractorStatusStatementPayments");

            migrationBuilder.DropColumn(
                name: "FinalManagerDescription",
                schema: "engineer",
                table: "ContractorStatusStatementPayments");

            migrationBuilder.DropColumn(
                name: "ManagementAmount",
                schema: "engineer",
                table: "ContractorStatusStatementPayments");

            migrationBuilder.DropColumn(
                name: "ManagementDescription",
                schema: "engineer",
                table: "ContractorStatusStatementPayments");

            migrationBuilder.DropColumn(
                name: "PayableAmount",
                schema: "engineer",
                table: "ContractorStatusStatementPayments");

            migrationBuilder.DropColumn(
                name: "PaymentAmount",
                schema: "engineer",
                table: "ContractorStatusStatementPayments");

            migrationBuilder.DropColumn(
                name: "PaymentDescription",
                schema: "engineer",
                table: "ContractorStatusStatementPayments");

            migrationBuilder.DropColumn(
                name: "PrimaryManagerAmount",
                schema: "engineer",
                table: "ContractorStatusStatementPayments");

            migrationBuilder.DropColumn(
                name: "PrimaryManagerDescription",
                schema: "engineer",
                table: "ContractorStatusStatementPayments");

            migrationBuilder.DropColumn(
                name: "ProjectManagerAmount",
                schema: "engineer",
                table: "ContractorStatusStatementPayments");

            migrationBuilder.DropColumn(
                name: "ProjectManagerDescription",
                schema: "engineer",
                table: "ContractorStatusStatementPayments");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "engineer",
                table: "ContractorStatusStatementPayments");

            migrationBuilder.DropColumn(
                name: "UserAmount",
                schema: "engineer",
                table: "ContractorStatusStatementPayments");

            migrationBuilder.DropColumn(
                name: "UserDescription",
                schema: "engineer",
                table: "ContractorStatusStatementPayments");

            migrationBuilder.AlterColumn<long>(
                name: "UpdaterId",
                schema: "engineer",
                table: "ContractorStatusStatementPayments",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "ویرایش کننده");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                schema: "engineer",
                table: "ContractorStatusStatementPayments",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldComment: "تاریخ ویرایش");

            migrationBuilder.AlterColumn<decimal>(
                name: "TreasuryPaid",
                schema: "engineer",
                table: "ContractorStatusStatementPayments",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true,
                oldComment: "مبلغ پرداخت شده توسط خزانه");

            migrationBuilder.AlterColumn<long>(
                name: "PaymentOrderId",
                schema: "engineer",
                table: "ContractorStatusStatementPayments",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "شناسه پرداخت");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PaymentDate",
                schema: "engineer",
                table: "ContractorStatusStatementPayments",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true,
                oldComment: "تاریخ پرداخت");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                schema: "engineer",
                table: "ContractorStatusStatementPayments",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "حذف شدگی");

            migrationBuilder.AlterColumn<long>(
                name: "CreatorId",
                schema: "engineer",
                table: "ContractorStatusStatementPayments",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "ایجاد کننده");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                schema: "engineer",
                table: "ContractorStatusStatementPayments",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "تاریخ ایجاد");

            migrationBuilder.AlterColumn<long>(
                name: "ContractorStatusStatementId",
                schema: "engineer",
                table: "ContractorStatusStatementPayments",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                schema: "engineer",
                table: "ContractorStatusStatementPayments",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "شناسه")
                .Annotation("SqlServer:Identity", "2, 1")
                .OldAnnotation("SqlServer:Identity", "2, 1");

            migrationBuilder.AddColumn<decimal>(
                name: "FinalManagerConfirmedAmount",
                schema: "engineer",
                table: "ContractorStatusStatementPayments",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                schema: "engineer",
                table: "ContractorStatusStatementPayments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "ManagerConfirmedAmount",
                schema: "engineer",
                table: "ContractorStatusStatementPayments",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PrimaryManagerConfirmedAmount",
                schema: "engineer",
                table: "ContractorStatusStatementPayments",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ContractorStatusStatementPayments_ContractorStatusStatements_ContractorStatusStatementId",
                schema: "engineer",
                table: "ContractorStatusStatementPayments",
                column: "ContractorStatusStatementId",
                principalSchema: "engineer",
                principalTable: "ContractorStatusStatements",
                principalColumn: "Id");
        }
    }
}
