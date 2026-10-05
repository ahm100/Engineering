using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddBaseAndChangePrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Workload",
                schema: "engineer",
                table: "ProjectOperations",
                type: "decimal(18,5)",
                nullable: false,
                comment: "حجم شرح عملیات",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,5)");

            migrationBuilder.AlterColumn<long>(
                name: "UnitOfMeasurementId",
                schema: "engineer",
                table: "ProjectOperations",
                type: "bigint",
                nullable: false,
                comment: "شناسه واحد اندازه گیری",
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<decimal>(
                name: "TolerancePercentage",
                schema: "engineer",
                table: "ProjectOperations",
                type: "decimal(18,5)",
                nullable: false,
                comment: "درصد تحمل",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,5)");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectOperationStatus",
                schema: "engineer",
                table: "ProjectOperations",
                type: "int",
                nullable: false,
                defaultValue: 1,
                comment: "وضعیت شرح عملیات های پروژه",
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 1);

            migrationBuilder.AlterColumn<int>(
                name: "Priority",
                schema: "engineer",
                table: "ProjectOperations",
                type: "int",
                nullable: true,
                comment: "اولویت",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                schema: "engineer",
                table: "ProjectOperations",
                type: "decimal(18,2)",
                nullable: true,
                defaultValue: 0m,
                comment: "هزینه",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true,
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                schema: "engineer",
                table: "ProjectOperations",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "حذف شدگی",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "GoodsInProgress",
                schema: "engineer",
                table: "ProjectOperations",
                type: "bit",
                nullable: false,
                comment: "کالاهای در حال پیشرفت",
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "engineer",
                table: "ProjectOperations",
                type: "nvarchar(1500)",
                nullable: true,
                comment: "توضیحات",
                oldClrType: typeof(string),
                oldType: "nvarchar(1500)",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CreatorId",
                schema: "engineer",
                table: "ProjectOperations",
                type: "bigint",
                nullable: false,
                comment: "ایجاد کننده",
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                schema: "engineer",
                table: "ProjectOperations",
                type: "datetime2",
                nullable: false,
                comment: "تاریخ ایجاد",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<long>(
                name: "CompanyId",
                schema: "engineer",
                table: "ProjectOperations",
                type: "bigint",
                nullable: true,
                comment: "شناسه کمپانی",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "BasePrice",
                schema: "engineer",
                table: "ProjectOperations",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                comment: "هزینه اولیه");

            migrationBuilder.AddColumn<decimal>(
                name: "ChangedPrice",
                schema: "engineer",
                table: "ProjectOperations",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                comment: " آخریم هزینه");

            migrationBuilder.AlterColumn<long>(
                name: "UnitOfMeasurementId",
                schema: "engineer",
                table: "OperationInfos",
                type: "bigint",
                nullable: false,
                comment: "شناسه واحد اندازه‌گیری",
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "Priority",
                schema: "engineer",
                table: "OperationInfos",
                type: "int",
                nullable: true,
                comment: "اولویت",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OperationLatinName",
                schema: "engineer",
                table: "OperationInfos",
                type: "nvarchar(250)",
                nullable: true,
                comment: "نام لاتین شرح عملیات",
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OperationInfoName",
                schema: "engineer",
                table: "OperationInfos",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                comment: "نام شرح عملیات",
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<string>(
                name: "OperationInfoCode",
                schema: "engineer",
                table: "OperationInfos",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                comment: "کد شرح عملیات",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                schema: "engineer",
                table: "OperationInfos",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "حذف شدگی",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                schema: "engineer",
                table: "OperationInfos",
                type: "bit",
                nullable: false,
                comment: "وضعیت فعال یا غیر فعال بودن",
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<long>(
                name: "CreatorId",
                schema: "engineer",
                table: "OperationInfos",
                type: "bigint",
                nullable: false,
                comment: "ایجاد کننده",
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                schema: "engineer",
                table: "OperationInfos",
                type: "datetime2",
                nullable: false,
                comment: "تاریخ ایجاد",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<long>(
                name: "CompanyId",
                schema: "engineer",
                table: "OperationInfos",
                type: "bigint",
                nullable: true,
                comment: "شناسه کمپانی",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "BasePrice",
                schema: "engineer",
                table: "OperationInfos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                comment: "قیمت پایه");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BasePrice",
                schema: "engineer",
                table: "ProjectOperations");

            migrationBuilder.DropColumn(
                name: "ChangedPrice",
                schema: "engineer",
                table: "ProjectOperations");

            migrationBuilder.DropColumn(
                name: "BasePrice",
                schema: "engineer",
                table: "OperationInfos");

            migrationBuilder.AlterColumn<decimal>(
                name: "Workload",
                schema: "engineer",
                table: "ProjectOperations",
                type: "decimal(18,5)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,5)",
                oldComment: "حجم شرح عملیات");

            migrationBuilder.AlterColumn<long>(
                name: "UnitOfMeasurementId",
                schema: "engineer",
                table: "ProjectOperations",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "شناسه واحد اندازه گیری");

            migrationBuilder.AlterColumn<decimal>(
                name: "TolerancePercentage",
                schema: "engineer",
                table: "ProjectOperations",
                type: "decimal(18,5)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,5)",
                oldComment: "درصد تحمل");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectOperationStatus",
                schema: "engineer",
                table: "ProjectOperations",
                type: "int",
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 1,
                oldComment: "وضعیت شرح عملیات های پروژه");

            migrationBuilder.AlterColumn<int>(
                name: "Priority",
                schema: "engineer",
                table: "ProjectOperations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldComment: "اولویت");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                schema: "engineer",
                table: "ProjectOperations",
                type: "decimal(18,2)",
                nullable: true,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true,
                oldDefaultValue: 0m,
                oldComment: "هزینه");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                schema: "engineer",
                table: "ProjectOperations",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false,
                oldComment: "حذف شدگی");

            migrationBuilder.AlterColumn<bool>(
                name: "GoodsInProgress",
                schema: "engineer",
                table: "ProjectOperations",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "کالاهای در حال پیشرفت");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "engineer",
                table: "ProjectOperations",
                type: "nvarchar(1500)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1500)",
                oldNullable: true,
                oldComment: "توضیحات");

            migrationBuilder.AlterColumn<long>(
                name: "CreatorId",
                schema: "engineer",
                table: "ProjectOperations",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "ایجاد کننده");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                schema: "engineer",
                table: "ProjectOperations",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "تاریخ ایجاد");

            migrationBuilder.AlterColumn<long>(
                name: "CompanyId",
                schema: "engineer",
                table: "ProjectOperations",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "شناسه کمپانی");

            migrationBuilder.AlterColumn<long>(
                name: "UnitOfMeasurementId",
                schema: "engineer",
                table: "OperationInfos",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "شناسه واحد اندازه‌گیری");

            migrationBuilder.AlterColumn<int>(
                name: "Priority",
                schema: "engineer",
                table: "OperationInfos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldComment: "اولویت");

            migrationBuilder.AlterColumn<string>(
                name: "OperationLatinName",
                schema: "engineer",
                table: "OperationInfos",
                type: "nvarchar(250)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldNullable: true,
                oldComment: "نام لاتین شرح عملیات");

            migrationBuilder.AlterColumn<string>(
                name: "OperationInfoName",
                schema: "engineer",
                table: "OperationInfos",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldComment: "نام شرح عملیات");

            migrationBuilder.AlterColumn<string>(
                name: "OperationInfoCode",
                schema: "engineer",
                table: "OperationInfos",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldComment: "کد شرح عملیات");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                schema: "engineer",
                table: "OperationInfos",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false,
                oldComment: "حذف شدگی");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                schema: "engineer",
                table: "OperationInfos",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "وضعیت فعال یا غیر فعال بودن");

            migrationBuilder.AlterColumn<long>(
                name: "CreatorId",
                schema: "engineer",
                table: "OperationInfos",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "ایجاد کننده");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                schema: "engineer",
                table: "OperationInfos",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "تاریخ ایجاد");

            migrationBuilder.AlterColumn<long>(
                name: "CompanyId",
                schema: "engineer",
                table: "OperationInfos",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "شناسه کمپانی");
        }
    }
}
