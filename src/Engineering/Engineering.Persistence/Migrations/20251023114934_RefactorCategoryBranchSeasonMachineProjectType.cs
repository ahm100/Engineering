using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RefactorCategoryBranchSeasonMachineProjectType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "UpdaterId",
                schema: "engineer",
                table: "MachineriesGroup",
                type: "bigint",
                nullable: true,
                comment: "ویرایش کننده",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                schema: "engineer",
                table: "MachineriesGroup",
                type: "datetime2",
                nullable: true,
                comment: "تاریخ ویرایش",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                schema: "engineer",
                table: "MachineriesGroup",
                type: "bit",
                nullable: false,
                comment: "حذف شدگی",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                schema: "engineer",
                table: "MachineriesGroup",
                type: "bit",
                nullable: false,
                comment: "وضعیت فعال یا غیر فعال بودن",
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<long>(
                name: "CreatorId",
                schema: "engineer",
                table: "MachineriesGroup",
                type: "bigint",
                nullable: false,
                comment: "ایجاد کننده",
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                schema: "engineer",
                table: "MachineriesGroup",
                type: "datetime2",
                nullable: false,
                comment: "تاریخ ایجاد",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                schema: "engineer",
                table: "MachineriesGroup",
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
                table: "Machineries",
                type: "bigint",
                nullable: true,
                comment: "ویرایش کننده",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                schema: "engineer",
                table: "Machineries",
                type: "datetime2",
                nullable: true,
                comment: "تاریخ ویرایش",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                schema: "engineer",
                table: "Machineries",
                type: "bit",
                nullable: false,
                comment: "حذف شدگی",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                schema: "engineer",
                table: "Machineries",
                type: "bit",
                nullable: false,
                comment: "وضعیت فعال یا غیر فعال بودن",
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<long>(
                name: "GroupId",
                schema: "engineer",
                table: "Machineries",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "CreatorId",
                schema: "engineer",
                table: "Machineries",
                type: "bigint",
                nullable: false,
                comment: "ایجاد کننده",
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                schema: "engineer",
                table: "Machineries",
                type: "datetime2",
                nullable: false,
                comment: "تاریخ ایجاد",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                schema: "engineer",
                table: "Machineries",
                type: "bigint",
                nullable: false,
                comment: "شناسه",
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("SqlServer:Identity", "2, 1")
                .OldAnnotation("SqlServer:Identity", "2, 1");

            migrationBuilder.AddColumn<long>(
                name: "MachineriesGroupId",
                schema: "engineer",
                table: "Machineries",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "UpdaterId",
                schema: "engineer",
                table: "EngineeringSeasons",
                type: "bigint",
                nullable: true,
                comment: "ویرایش کننده",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                schema: "engineer",
                table: "EngineeringSeasons",
                type: "datetime2",
                nullable: true,
                comment: "تاریخ ویرایش",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                schema: "engineer",
                table: "EngineeringSeasons",
                type: "bit",
                nullable: false,
                comment: "حذف شدگی",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                schema: "engineer",
                table: "EngineeringSeasons",
                type: "bit",
                nullable: false,
                comment: "وضعیت فعال یا غیر فعال بودن",
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<long>(
                name: "CreatorId",
                schema: "engineer",
                table: "EngineeringSeasons",
                type: "bigint",
                nullable: false,
                comment: "ایجاد کننده",
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                schema: "engineer",
                table: "EngineeringSeasons",
                type: "datetime2",
                nullable: false,
                comment: "تاریخ ایجاد",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<long>(
                name: "BranchId",
                schema: "engineer",
                table: "EngineeringSeasons",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                schema: "engineer",
                table: "EngineeringSeasons",
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
                table: "EngineeringProjectTypes",
                type: "bigint",
                nullable: true,
                comment: "ویرایش کننده",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                schema: "engineer",
                table: "EngineeringProjectTypes",
                type: "datetime2",
                nullable: true,
                comment: "تاریخ ویرایش",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                schema: "engineer",
                table: "EngineeringProjectTypes",
                type: "bit",
                nullable: false,
                comment: "حذف شدگی",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                schema: "engineer",
                table: "EngineeringProjectTypes",
                type: "bit",
                nullable: false,
                comment: "وضعیت فعال یا غیر فعال بودن",
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<long>(
                name: "CreatorId",
                schema: "engineer",
                table: "EngineeringProjectTypes",
                type: "bigint",
                nullable: false,
                comment: "ایجاد کننده",
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                schema: "engineer",
                table: "EngineeringProjectTypes",
                type: "datetime2",
                nullable: false,
                comment: "تاریخ ایجاد",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                schema: "engineer",
                table: "EngineeringProjectTypes",
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
                table: "EngineeringCategories",
                type: "bigint",
                nullable: true,
                comment: "ویرایش کننده",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                schema: "engineer",
                table: "EngineeringCategories",
                type: "datetime2",
                nullable: true,
                comment: "تاریخ ویرایش",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                schema: "engineer",
                table: "EngineeringCategories",
                type: "bit",
                nullable: false,
                comment: "حذف شدگی",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                schema: "engineer",
                table: "EngineeringCategories",
                type: "bit",
                nullable: false,
                comment: "وضعیت فعال یا غیر فعال بودن",
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<long>(
                name: "CreatorId",
                schema: "engineer",
                table: "EngineeringCategories",
                type: "bigint",
                nullable: false,
                comment: "ایجاد کننده",
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                schema: "engineer",
                table: "EngineeringCategories",
                type: "datetime2",
                nullable: false,
                comment: "تاریخ ایجاد",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                schema: "engineer",
                table: "EngineeringCategories",
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
                table: "EngineeringBranchs",
                type: "bigint",
                nullable: true,
                comment: "ویرایش کننده",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                schema: "engineer",
                table: "EngineeringBranchs",
                type: "datetime2",
                nullable: true,
                comment: "تاریخ ویرایش",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                schema: "engineer",
                table: "EngineeringBranchs",
                type: "bit",
                nullable: false,
                comment: "حذف شدگی",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                schema: "engineer",
                table: "EngineeringBranchs",
                type: "bit",
                nullable: false,
                comment: "وضعیت فعال یا غیر فعال بودن",
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<long>(
                name: "CreatorId",
                schema: "engineer",
                table: "EngineeringBranchs",
                type: "bigint",
                nullable: false,
                comment: "ایجاد کننده",
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                schema: "engineer",
                table: "EngineeringBranchs",
                type: "datetime2",
                nullable: false,
                comment: "تاریخ ایجاد",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<long>(
                name: "CategoryId",
                schema: "engineer",
                table: "EngineeringBranchs",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                schema: "engineer",
                table: "EngineeringBranchs",
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
                name: "MachineriesGroupId",
                schema: "engineer",
                table: "Machineries");

            migrationBuilder.AlterColumn<long>(
                name: "UpdaterId",
                schema: "engineer",
                table: "MachineriesGroup",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "ویرایش کننده");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                schema: "engineer",
                table: "MachineriesGroup",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldComment: "تاریخ ویرایش");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                schema: "engineer",
                table: "MachineriesGroup",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "حذف شدگی");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                schema: "engineer",
                table: "MachineriesGroup",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "وضعیت فعال یا غیر فعال بودن");

            migrationBuilder.AlterColumn<long>(
                name: "CreatorId",
                schema: "engineer",
                table: "MachineriesGroup",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "ایجاد کننده");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                schema: "engineer",
                table: "MachineriesGroup",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "تاریخ ایجاد");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                schema: "engineer",
                table: "MachineriesGroup",
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
                table: "Machineries",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "ویرایش کننده");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                schema: "engineer",
                table: "Machineries",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldComment: "تاریخ ویرایش");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                schema: "engineer",
                table: "Machineries",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "حذف شدگی");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                schema: "engineer",
                table: "Machineries",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "وضعیت فعال یا غیر فعال بودن");

            migrationBuilder.AlterColumn<long>(
                name: "GroupId",
                schema: "engineer",
                table: "Machineries",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CreatorId",
                schema: "engineer",
                table: "Machineries",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "ایجاد کننده");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                schema: "engineer",
                table: "Machineries",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "تاریخ ایجاد");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                schema: "engineer",
                table: "Machineries",
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
                table: "EngineeringSeasons",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "ویرایش کننده");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                schema: "engineer",
                table: "EngineeringSeasons",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldComment: "تاریخ ویرایش");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                schema: "engineer",
                table: "EngineeringSeasons",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "حذف شدگی");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                schema: "engineer",
                table: "EngineeringSeasons",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "وضعیت فعال یا غیر فعال بودن");

            migrationBuilder.AlterColumn<long>(
                name: "CreatorId",
                schema: "engineer",
                table: "EngineeringSeasons",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "ایجاد کننده");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                schema: "engineer",
                table: "EngineeringSeasons",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "تاریخ ایجاد");

            migrationBuilder.AlterColumn<long>(
                name: "BranchId",
                schema: "engineer",
                table: "EngineeringSeasons",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                schema: "engineer",
                table: "EngineeringSeasons",
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
                table: "EngineeringProjectTypes",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "ویرایش کننده");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                schema: "engineer",
                table: "EngineeringProjectTypes",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldComment: "تاریخ ویرایش");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                schema: "engineer",
                table: "EngineeringProjectTypes",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "حذف شدگی");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                schema: "engineer",
                table: "EngineeringProjectTypes",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "وضعیت فعال یا غیر فعال بودن");

            migrationBuilder.AlterColumn<long>(
                name: "CreatorId",
                schema: "engineer",
                table: "EngineeringProjectTypes",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "ایجاد کننده");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                schema: "engineer",
                table: "EngineeringProjectTypes",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "تاریخ ایجاد");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                schema: "engineer",
                table: "EngineeringProjectTypes",
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
                table: "EngineeringCategories",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "ویرایش کننده");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                schema: "engineer",
                table: "EngineeringCategories",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldComment: "تاریخ ویرایش");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                schema: "engineer",
                table: "EngineeringCategories",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "حذف شدگی");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                schema: "engineer",
                table: "EngineeringCategories",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "وضعیت فعال یا غیر فعال بودن");

            migrationBuilder.AlterColumn<long>(
                name: "CreatorId",
                schema: "engineer",
                table: "EngineeringCategories",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "ایجاد کننده");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                schema: "engineer",
                table: "EngineeringCategories",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "تاریخ ایجاد");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                schema: "engineer",
                table: "EngineeringCategories",
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
                table: "EngineeringBranchs",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "ویرایش کننده");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                schema: "engineer",
                table: "EngineeringBranchs",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldComment: "تاریخ ویرایش");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                schema: "engineer",
                table: "EngineeringBranchs",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "حذف شدگی");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                schema: "engineer",
                table: "EngineeringBranchs",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "وضعیت فعال یا غیر فعال بودن");

            migrationBuilder.AlterColumn<long>(
                name: "CreatorId",
                schema: "engineer",
                table: "EngineeringBranchs",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "ایجاد کننده");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                schema: "engineer",
                table: "EngineeringBranchs",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "تاریخ ایجاد");

            migrationBuilder.AlterColumn<long>(
                name: "CategoryId",
                schema: "engineer",
                table: "EngineeringBranchs",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                schema: "engineer",
                table: "EngineeringBranchs",
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
