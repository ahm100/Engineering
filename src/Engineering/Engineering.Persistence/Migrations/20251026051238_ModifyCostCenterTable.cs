using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ModifyCostCenterTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "UpdaterId",
                schema: "engineer",
                table: "InformedUsers",
                type: "bigint",
                nullable: true,
                comment: "ویرایش کننده",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                schema: "engineer",
                table: "InformedUsers",
                type: "datetime2",
                nullable: true,
                comment: "تاریخ ویرایش",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                schema: "engineer",
                table: "InformedUsers",
                type: "bit",
                nullable: false,
                comment: "حذف شدگی",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<long>(
                name: "EmployeeId",
                schema: "engineer",
                table: "InformedUsers",
                type: "bigint",
                nullable: false,
                comment: "شناسه کارمند ",
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "CreatorId",
                schema: "engineer",
                table: "InformedUsers",
                type: "bigint",
                nullable: false,
                comment: "ایجاد کننده",
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                schema: "engineer",
                table: "InformedUsers",
                type: "datetime2",
                nullable: false,
                comment: "تاریخ ایجاد",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                schema: "engineer",
                table: "InformedUsers",
                type: "bigint",
                nullable: false,
                comment: "شناسه",
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("SqlServer:Identity", "2, 1")
                .OldAnnotation("SqlServer:Identity", "2, 1");

            migrationBuilder.AlterColumn<long>(
                name: "WarehouseId",
                schema: "engineer",
                table: "CostCenterWarehouses",
                type: "bigint",
                nullable: false,
                comment: "شناسه انبار مرکز هزینه ",
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "UpdaterId",
                schema: "engineer",
                table: "CostCenterWarehouses",
                type: "bigint",
                nullable: true,
                comment: "ویرایش کننده",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                schema: "engineer",
                table: "CostCenterWarehouses",
                type: "datetime2",
                nullable: true,
                comment: "تاریخ ویرایش",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                schema: "engineer",
                table: "CostCenterWarehouses",
                type: "bit",
                nullable: false,
                comment: "حذف شدگی",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDefault",
                schema: "engineer",
                table: "CostCenterWarehouses",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "پیش‌فرض بودن",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<long>(
                name: "CreatorId",
                schema: "engineer",
                table: "CostCenterWarehouses",
                type: "bigint",
                nullable: false,
                comment: "ایجاد کننده",
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                schema: "engineer",
                table: "CostCenterWarehouses",
                type: "datetime2",
                nullable: false,
                comment: "تاریخ ایجاد",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                schema: "engineer",
                table: "CostCenterWarehouses",
                type: "bigint",
                nullable: false,
                comment: "شناسه",
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("SqlServer:Identity", "2, 1")
                .OldAnnotation("SqlServer:Identity", "2, 1");

            migrationBuilder.AlterColumn<long>(
                name: "UpdaterId",
                schema: "engineer",
                table: "CostCenterVirtualGroups",
                type: "bigint",
                nullable: true,
                comment: "ویرایش کننده",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                schema: "engineer",
                table: "CostCenterVirtualGroups",
                type: "datetime2",
                nullable: true,
                comment: "تاریخ ویرایش",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                schema: "engineer",
                table: "CostCenterVirtualGroups",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                comment: "عنوان گروه/کانال",
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<bool>(
                name: "SendYesterday",
                schema: "engineer",
                table: "CostCenterVirtualGroups",
                type: "bit",
                nullable: false,
                comment: "ارسال نفر روز دیروز",
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "SendToday",
                schema: "engineer",
                table: "CostCenterVirtualGroups",
                type: "bit",
                nullable: false,
                comment: "ارسال نفر روز امروز",
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "Link",
                schema: "engineer",
                table: "CostCenterVirtualGroups",
                type: "nvarchar(1500)",
                maxLength: 1500,
                nullable: false,
                comment: "لینک",
                oldClrType: typeof(string),
                oldType: "nvarchar(1500)",
                oldMaxLength: 1500);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                schema: "engineer",
                table: "CostCenterVirtualGroups",
                type: "bit",
                nullable: false,
                comment: "حذف شدگی",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Identifier",
                schema: "engineer",
                table: "CostCenterVirtualGroups",
                type: "nvarchar(max)",
                nullable: false,
                comment: "شناسه",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<long>(
                name: "CreatorId",
                schema: "engineer",
                table: "CostCenterVirtualGroups",
                type: "bigint",
                nullable: false,
                comment: "ایجاد کننده",
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                schema: "engineer",
                table: "CostCenterVirtualGroups",
                type: "datetime2",
                nullable: false,
                comment: "تاریخ ایجاد",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                schema: "engineer",
                table: "CostCenterVirtualGroups",
                type: "bigint",
                nullable: false,
                comment: "شناسه",
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("SqlServer:Identity", "2, 1")
                .OldAnnotation("SqlServer:Identity", "2, 1");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                schema: "engineer",
                table: "CostCenterVirtualGroupAdmins",
                type: "nvarchar(max)",
                nullable: false,
                comment: "نام کاربری ",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<long>(
                name: "UpdaterId",
                schema: "engineer",
                table: "CostCenterVirtualGroupAdmins",
                type: "bigint",
                nullable: true,
                comment: "ویرایش کننده",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                schema: "engineer",
                table: "CostCenterVirtualGroupAdmins",
                type: "datetime2",
                nullable: true,
                comment: "تاریخ ویرایش",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ThirdPartyId",
                schema: "engineer",
                table: "CostCenterVirtualGroupAdmins",
                type: "bigint",
                nullable: false,
                comment: "شناسه شخص سوم ",
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                schema: "engineer",
                table: "CostCenterVirtualGroupAdmins",
                type: "bit",
                nullable: false,
                comment: "حذف شدگی",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<long>(
                name: "CreatorId",
                schema: "engineer",
                table: "CostCenterVirtualGroupAdmins",
                type: "bigint",
                nullable: false,
                comment: "ایجاد کننده",
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                schema: "engineer",
                table: "CostCenterVirtualGroupAdmins",
                type: "datetime2",
                nullable: false,
                comment: "تاریخ ایجاد",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                schema: "engineer",
                table: "CostCenterVirtualGroupAdmins",
                type: "bigint",
                nullable: false,
                comment: "شناسه",
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("SqlServer:Identity", "2, 1")
                .OldAnnotation("SqlServer:Identity", "2, 1");

            migrationBuilder.AlterColumn<long>(
                name: "UpdaterId",
                schema: "engineer",
                table: "CostCenterTypes",
                type: "bigint",
                nullable: true,
                comment: "ویرایش کننده",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                schema: "engineer",
                table: "CostCenterTypes",
                type: "datetime2",
                nullable: true,
                comment: "تاریخ ویرایش",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                schema: "engineer",
                table: "CostCenterTypes",
                type: "bit",
                nullable: false,
                comment: "حذف شدگی",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                schema: "engineer",
                table: "CostCenterTypes",
                type: "bit",
                nullable: false,
                comment: "وضعیت فعال یا غیر فعال بودن",
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<long>(
                name: "CreatorId",
                schema: "engineer",
                table: "CostCenterTypes",
                type: "bigint",
                nullable: false,
                comment: "ایجاد کننده",
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                schema: "engineer",
                table: "CostCenterTypes",
                type: "datetime2",
                nullable: false,
                comment: "تاریخ ایجاد",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "CostCenterTypeCode",
                schema: "engineer",
                table: "CostCenterTypes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                comment: "شناسه مرکز هزینه",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                schema: "engineer",
                table: "CostCenterTypes",
                type: "bigint",
                nullable: false,
                comment: "شناسه",
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("SqlServer:Identity", "2, 1")
                .OldAnnotation("SqlServer:Identity", "2, 1");

            migrationBuilder.AlterColumn<bool>(
                name: "WeatherState",
                schema: "engineer",
                table: "CostCenters",
                type: "bit",
                nullable: true,
                defaultValue: false,
                comment: "وضعیت آب و هوا",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<long>(
                name: "UpdaterId",
                schema: "engineer",
                table: "CostCenters",
                type: "bigint",
                nullable: true,
                comment: "ویرایش کننده",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                schema: "engineer",
                table: "CostCenters",
                type: "datetime2",
                nullable: true,
                comment: "تاریخ ویرایش",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PostalCode",
                schema: "engineer",
                table: "CostCenters",
                type: "nvarchar(10)",
                nullable: true,
                comment: "کد پستی",
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NoOperationDays",
                schema: "engineer",
                table: "CostCenters",
                type: "int",
                nullable: true,
                comment: "روزهای بدون پروژه",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Longitude",
                schema: "engineer",
                table: "CostCenters",
                type: "decimal(18,9)",
                nullable: false,
                comment: "طول جغرافیایی",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,9)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Latitude",
                schema: "engineer",
                table: "CostCenters",
                type: "decimal(18,9)",
                nullable: false,
                comment: "عرض جغرافیایی",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,9)");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                schema: "engineer",
                table: "CostCenters",
                type: "bit",
                nullable: false,
                comment: "حذف شدگی",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                schema: "engineer",
                table: "CostCenters",
                type: "bit",
                nullable: false,
                comment: "وضعیت فعال یا غیر فعال بودن",
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "engineer",
                table: "CostCenters",
                type: "nvarchar(1500)",
                nullable: true,
                comment: "توضیحات",
                oldClrType: typeof(string),
                oldType: "nvarchar(1500)",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CreatorId",
                schema: "engineer",
                table: "CostCenters",
                type: "bigint",
                nullable: false,
                comment: "ایجاد کننده",
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                schema: "engineer",
                table: "CostCenters",
                type: "datetime2",
                nullable: false,
                comment: "تاریخ ایجاد",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "CostCenterName",
                schema: "engineer",
                table: "CostCenters",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                comment: "نام مرکز هزینه",
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<string>(
                name: "CostCenterCode",
                schema: "engineer",
                table: "CostCenters",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                comment: "کد مرکز هزینه",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<long>(
                name: "CompanyId",
                schema: "engineer",
                table: "CostCenters",
                type: "bigint",
                nullable: true,
                comment: "شناسه کمپانی",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CityId",
                schema: "engineer",
                table: "CostCenters",
                type: "bigint",
                nullable: false,
                comment: "شناسه شهر",
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                schema: "engineer",
                table: "CostCenters",
                type: "nvarchar(1500)",
                nullable: false,
                comment: "آدرس مرکز هزینه",
                oldClrType: typeof(string),
                oldType: "nvarchar(1500)");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                schema: "engineer",
                table: "CostCenters",
                type: "bigint",
                nullable: false,
                comment: "شناسه",
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("SqlServer:Identity", "2, 1")
                .OldAnnotation("SqlServer:Identity", "2, 1");

            migrationBuilder.AddColumn<long>(
                name: "CostCenterTypeId",
                schema: "engineer",
                table: "CostCenters",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<long>(
                name: "UpdaterId",
                schema: "engineer",
                table: "CostCenterAuthorizedUsers",
                type: "bigint",
                nullable: true,
                comment: "ویرایش کننده",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                schema: "engineer",
                table: "CostCenterAuthorizedUsers",
                type: "datetime2",
                nullable: true,
                comment: "تاریخ ویرایش",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                schema: "engineer",
                table: "CostCenterAuthorizedUsers",
                type: "bit",
                nullable: false,
                comment: "حذف شدگی",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<long>(
                name: "CreatorId",
                schema: "engineer",
                table: "CostCenterAuthorizedUsers",
                type: "bigint",
                nullable: false,
                comment: "ایجاد کننده",
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                schema: "engineer",
                table: "CostCenterAuthorizedUsers",
                type: "datetime2",
                nullable: false,
                comment: "تاریخ ایجاد",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<long>(
                name: "AuthorizedUserId",
                schema: "engineer",
                table: "CostCenterAuthorizedUsers",
                type: "bigint",
                nullable: false,
                comment: "شناسه کاربران مجاز برای دیدن مرکز هزینه",
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                schema: "engineer",
                table: "CostCenterAuthorizedUsers",
                type: "bigint",
                nullable: false,
                comment: "شناسه",
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("SqlServer:Identity", "2, 1")
                .OldAnnotation("SqlServer:Identity", "2, 1");

            migrationBuilder.AlterColumn<long>(
                name: "UpdaterId",
                schema: "engineer",
                table: "CostCenterAuthorizedRoles",
                type: "bigint",
                nullable: true,
                comment: "ویرایش کننده",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                schema: "engineer",
                table: "CostCenterAuthorizedRoles",
                type: "datetime2",
                nullable: true,
                comment: "تاریخ ویرایش",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                schema: "engineer",
                table: "CostCenterAuthorizedRoles",
                type: "bit",
                nullable: false,
                comment: "حذف شدگی",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<long>(
                name: "CreatorId",
                schema: "engineer",
                table: "CostCenterAuthorizedRoles",
                type: "bigint",
                nullable: false,
                comment: "ایجاد کننده",
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                schema: "engineer",
                table: "CostCenterAuthorizedRoles",
                type: "datetime2",
                nullable: false,
                comment: "تاریخ ایجاد",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<long>(
                name: "AuthorizedRoleId",
                schema: "engineer",
                table: "CostCenterAuthorizedRoles",
                type: "bigint",
                nullable: false,
                comment: "شناسه نقش های مجاز برای دیدن مرکز هزینه",
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                schema: "engineer",
                table: "CostCenterAuthorizedRoles",
                type: "bigint",
                nullable: false,
                comment: "شناسه",
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("SqlServer:Identity", "2, 1")
                .OldAnnotation("SqlServer:Identity", "2, 1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CostCenterTypeId",
                schema: "engineer",
                table: "CostCenters");

            migrationBuilder.AlterColumn<long>(
                name: "UpdaterId",
                schema: "engineer",
                table: "InformedUsers",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "ویرایش کننده");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                schema: "engineer",
                table: "InformedUsers",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldComment: "تاریخ ویرایش");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                schema: "engineer",
                table: "InformedUsers",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "حذف شدگی");

            migrationBuilder.AlterColumn<long>(
                name: "EmployeeId",
                schema: "engineer",
                table: "InformedUsers",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "شناسه کارمند ");

            migrationBuilder.AlterColumn<long>(
                name: "CreatorId",
                schema: "engineer",
                table: "InformedUsers",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "ایجاد کننده");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                schema: "engineer",
                table: "InformedUsers",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "تاریخ ایجاد");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                schema: "engineer",
                table: "InformedUsers",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "شناسه")
                .Annotation("SqlServer:Identity", "2, 1")
                .OldAnnotation("SqlServer:Identity", "2, 1");

            migrationBuilder.AlterColumn<long>(
                name: "WarehouseId",
                schema: "engineer",
                table: "CostCenterWarehouses",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "شناسه انبار مرکز هزینه ");

            migrationBuilder.AlterColumn<long>(
                name: "UpdaterId",
                schema: "engineer",
                table: "CostCenterWarehouses",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "ویرایش کننده");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                schema: "engineer",
                table: "CostCenterWarehouses",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldComment: "تاریخ ویرایش");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                schema: "engineer",
                table: "CostCenterWarehouses",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "حذف شدگی");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDefault",
                schema: "engineer",
                table: "CostCenterWarehouses",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false,
                oldComment: "پیش‌فرض بودن");

            migrationBuilder.AlterColumn<long>(
                name: "CreatorId",
                schema: "engineer",
                table: "CostCenterWarehouses",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "ایجاد کننده");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                schema: "engineer",
                table: "CostCenterWarehouses",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "تاریخ ایجاد");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                schema: "engineer",
                table: "CostCenterWarehouses",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "شناسه")
                .Annotation("SqlServer:Identity", "2, 1")
                .OldAnnotation("SqlServer:Identity", "2, 1");

            migrationBuilder.AlterColumn<long>(
                name: "UpdaterId",
                schema: "engineer",
                table: "CostCenterVirtualGroups",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "ویرایش کننده");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                schema: "engineer",
                table: "CostCenterVirtualGroups",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldComment: "تاریخ ویرایش");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                schema: "engineer",
                table: "CostCenterVirtualGroups",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldComment: "عنوان گروه/کانال");

            migrationBuilder.AlterColumn<bool>(
                name: "SendYesterday",
                schema: "engineer",
                table: "CostCenterVirtualGroups",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "ارسال نفر روز دیروز");

            migrationBuilder.AlterColumn<bool>(
                name: "SendToday",
                schema: "engineer",
                table: "CostCenterVirtualGroups",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "ارسال نفر روز امروز");

            migrationBuilder.AlterColumn<string>(
                name: "Link",
                schema: "engineer",
                table: "CostCenterVirtualGroups",
                type: "nvarchar(1500)",
                maxLength: 1500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1500)",
                oldMaxLength: 1500,
                oldComment: "لینک");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                schema: "engineer",
                table: "CostCenterVirtualGroups",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "حذف شدگی");

            migrationBuilder.AlterColumn<string>(
                name: "Identifier",
                schema: "engineer",
                table: "CostCenterVirtualGroups",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "شناسه");

            migrationBuilder.AlterColumn<long>(
                name: "CreatorId",
                schema: "engineer",
                table: "CostCenterVirtualGroups",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "ایجاد کننده");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                schema: "engineer",
                table: "CostCenterVirtualGroups",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "تاریخ ایجاد");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                schema: "engineer",
                table: "CostCenterVirtualGroups",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "شناسه")
                .Annotation("SqlServer:Identity", "2, 1")
                .OldAnnotation("SqlServer:Identity", "2, 1");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                schema: "engineer",
                table: "CostCenterVirtualGroupAdmins",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "نام کاربری ");

            migrationBuilder.AlterColumn<long>(
                name: "UpdaterId",
                schema: "engineer",
                table: "CostCenterVirtualGroupAdmins",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "ویرایش کننده");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                schema: "engineer",
                table: "CostCenterVirtualGroupAdmins",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldComment: "تاریخ ویرایش");

            migrationBuilder.AlterColumn<long>(
                name: "ThirdPartyId",
                schema: "engineer",
                table: "CostCenterVirtualGroupAdmins",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "شناسه شخص سوم ");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                schema: "engineer",
                table: "CostCenterVirtualGroupAdmins",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "حذف شدگی");

            migrationBuilder.AlterColumn<long>(
                name: "CreatorId",
                schema: "engineer",
                table: "CostCenterVirtualGroupAdmins",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "ایجاد کننده");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                schema: "engineer",
                table: "CostCenterVirtualGroupAdmins",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "تاریخ ایجاد");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                schema: "engineer",
                table: "CostCenterVirtualGroupAdmins",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "شناسه")
                .Annotation("SqlServer:Identity", "2, 1")
                .OldAnnotation("SqlServer:Identity", "2, 1");

            migrationBuilder.AlterColumn<long>(
                name: "UpdaterId",
                schema: "engineer",
                table: "CostCenterTypes",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "ویرایش کننده");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                schema: "engineer",
                table: "CostCenterTypes",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldComment: "تاریخ ویرایش");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                schema: "engineer",
                table: "CostCenterTypes",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "حذف شدگی");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                schema: "engineer",
                table: "CostCenterTypes",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "وضعیت فعال یا غیر فعال بودن");

            migrationBuilder.AlterColumn<long>(
                name: "CreatorId",
                schema: "engineer",
                table: "CostCenterTypes",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "ایجاد کننده");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                schema: "engineer",
                table: "CostCenterTypes",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "تاریخ ایجاد");

            migrationBuilder.AlterColumn<string>(
                name: "CostCenterTypeCode",
                schema: "engineer",
                table: "CostCenterTypes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldComment: "شناسه مرکز هزینه");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                schema: "engineer",
                table: "CostCenterTypes",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "شناسه")
                .Annotation("SqlServer:Identity", "2, 1")
                .OldAnnotation("SqlServer:Identity", "2, 1");

            migrationBuilder.AlterColumn<bool>(
                name: "WeatherState",
                schema: "engineer",
                table: "CostCenters",
                type: "bit",
                nullable: true,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: false,
                oldComment: "وضعیت آب و هوا");

            migrationBuilder.AlterColumn<long>(
                name: "UpdaterId",
                schema: "engineer",
                table: "CostCenters",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "ویرایش کننده");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                schema: "engineer",
                table: "CostCenters",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldComment: "تاریخ ویرایش");

            migrationBuilder.AlterColumn<string>(
                name: "PostalCode",
                schema: "engineer",
                table: "CostCenters",
                type: "nvarchar(10)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldNullable: true,
                oldComment: "کد پستی");

            migrationBuilder.AlterColumn<int>(
                name: "NoOperationDays",
                schema: "engineer",
                table: "CostCenters",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldComment: "روزهای بدون پروژه");

            migrationBuilder.AlterColumn<decimal>(
                name: "Longitude",
                schema: "engineer",
                table: "CostCenters",
                type: "decimal(18,9)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,9)",
                oldComment: "طول جغرافیایی");

            migrationBuilder.AlterColumn<decimal>(
                name: "Latitude",
                schema: "engineer",
                table: "CostCenters",
                type: "decimal(18,9)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,9)",
                oldComment: "عرض جغرافیایی");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                schema: "engineer",
                table: "CostCenters",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "حذف شدگی");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                schema: "engineer",
                table: "CostCenters",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "وضعیت فعال یا غیر فعال بودن");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "engineer",
                table: "CostCenters",
                type: "nvarchar(1500)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1500)",
                oldNullable: true,
                oldComment: "توضیحات");

            migrationBuilder.AlterColumn<long>(
                name: "CreatorId",
                schema: "engineer",
                table: "CostCenters",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "ایجاد کننده");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                schema: "engineer",
                table: "CostCenters",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "تاریخ ایجاد");

            migrationBuilder.AlterColumn<string>(
                name: "CostCenterName",
                schema: "engineer",
                table: "CostCenters",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldComment: "نام مرکز هزینه");

            migrationBuilder.AlterColumn<string>(
                name: "CostCenterCode",
                schema: "engineer",
                table: "CostCenters",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldComment: "کد مرکز هزینه");

            migrationBuilder.AlterColumn<long>(
                name: "CompanyId",
                schema: "engineer",
                table: "CostCenters",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "شناسه کمپانی");

            migrationBuilder.AlterColumn<long>(
                name: "CityId",
                schema: "engineer",
                table: "CostCenters",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "شناسه شهر");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                schema: "engineer",
                table: "CostCenters",
                type: "nvarchar(1500)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1500)",
                oldComment: "آدرس مرکز هزینه");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                schema: "engineer",
                table: "CostCenters",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "شناسه")
                .Annotation("SqlServer:Identity", "2, 1")
                .OldAnnotation("SqlServer:Identity", "2, 1");

            migrationBuilder.AlterColumn<long>(
                name: "UpdaterId",
                schema: "engineer",
                table: "CostCenterAuthorizedUsers",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "ویرایش کننده");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                schema: "engineer",
                table: "CostCenterAuthorizedUsers",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldComment: "تاریخ ویرایش");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                schema: "engineer",
                table: "CostCenterAuthorizedUsers",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "حذف شدگی");

            migrationBuilder.AlterColumn<long>(
                name: "CreatorId",
                schema: "engineer",
                table: "CostCenterAuthorizedUsers",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "ایجاد کننده");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                schema: "engineer",
                table: "CostCenterAuthorizedUsers",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "تاریخ ایجاد");

            migrationBuilder.AlterColumn<long>(
                name: "AuthorizedUserId",
                schema: "engineer",
                table: "CostCenterAuthorizedUsers",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "شناسه کاربران مجاز برای دیدن مرکز هزینه");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                schema: "engineer",
                table: "CostCenterAuthorizedUsers",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "شناسه")
                .Annotation("SqlServer:Identity", "2, 1")
                .OldAnnotation("SqlServer:Identity", "2, 1");

            migrationBuilder.AlterColumn<long>(
                name: "UpdaterId",
                schema: "engineer",
                table: "CostCenterAuthorizedRoles",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "ویرایش کننده");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                schema: "engineer",
                table: "CostCenterAuthorizedRoles",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldComment: "تاریخ ویرایش");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                schema: "engineer",
                table: "CostCenterAuthorizedRoles",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "حذف شدگی");

            migrationBuilder.AlterColumn<long>(
                name: "CreatorId",
                schema: "engineer",
                table: "CostCenterAuthorizedRoles",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "ایجاد کننده");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                schema: "engineer",
                table: "CostCenterAuthorizedRoles",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "تاریخ ایجاد");

            migrationBuilder.AlterColumn<long>(
                name: "AuthorizedRoleId",
                schema: "engineer",
                table: "CostCenterAuthorizedRoles",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "شناسه نقش های مجاز برای دیدن مرکز هزینه");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                schema: "engineer",
                table: "CostCenterAuthorizedRoles",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "شناسه")
                .Annotation("SqlServer:Identity", "2, 1")
                .OldAnnotation("SqlServer:Identity", "2, 1");
        }
    }
}
