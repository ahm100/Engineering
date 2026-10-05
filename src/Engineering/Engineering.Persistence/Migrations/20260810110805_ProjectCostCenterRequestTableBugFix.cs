using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ProjectCostCenterRequestTableBugFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectCostCenterRequest_CostCenters_CostCenterId",
                schema: "engineer",
                table: "ProjectCostCenterRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectCostCenterRequest_Projects_ProjectId",
                schema: "engineer",
                table: "ProjectCostCenterRequest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectCostCenterRequest",
                schema: "engineer",
                table: "ProjectCostCenterRequest");

            migrationBuilder.RenameTable(
                name: "ProjectCostCenterRequest",
                schema: "engineer",
                newName: "ProjectCostCenterRequests",
                newSchema: "engineer");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectCostCenterRequest_ProjectId",
                schema: "engineer",
                table: "ProjectCostCenterRequests",
                newName: "IX_ProjectCostCenterRequests_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectCostCenterRequest_CostCenterId",
                schema: "engineer",
                table: "ProjectCostCenterRequests",
                newName: "IX_ProjectCostCenterRequests_CostCenterId");

            migrationBuilder.AlterColumn<long>(
                name: "UpdaterId",
                schema: "engineer",
                table: "ProjectCostCenterRequests",
                type: "bigint",
                nullable: true,
                comment: "ویرایش کننده",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                schema: "engineer",
                table: "ProjectCostCenterRequests",
                type: "datetime2",
                nullable: true,
                comment: "تاریخ ویرایش",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                schema: "engineer",
                table: "ProjectCostCenterRequests",
                type: "bit",
                nullable: false,
                comment: "حذف شدگی",
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<long>(
                name: "CreatorId",
                schema: "engineer",
                table: "ProjectCostCenterRequests",
                type: "bigint",
                nullable: false,
                comment: "ایجاد کننده",
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                schema: "engineer",
                table: "ProjectCostCenterRequests",
                type: "datetime2",
                nullable: false,
                comment: "تاریخ ایجاد",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                schema: "engineer",
                table: "ProjectCostCenterRequests",
                type: "bigint",
                nullable: false,
                comment: "شناسه",
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("SqlServer:Identity", "2, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectCostCenterRequests",
                schema: "engineer",
                table: "ProjectCostCenterRequests",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectCostCenterRequests_CostCenters_CostCenterId",
                schema: "engineer",
                table: "ProjectCostCenterRequests",
                column: "CostCenterId",
                principalSchema: "engineer",
                principalTable: "CostCenters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectCostCenterRequests_Projects_ProjectId",
                schema: "engineer",
                table: "ProjectCostCenterRequests",
                column: "ProjectId",
                principalSchema: "engineer",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectCostCenterRequests_CostCenters_CostCenterId",
                schema: "engineer",
                table: "ProjectCostCenterRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectCostCenterRequests_Projects_ProjectId",
                schema: "engineer",
                table: "ProjectCostCenterRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectCostCenterRequests",
                schema: "engineer",
                table: "ProjectCostCenterRequests");

            migrationBuilder.RenameTable(
                name: "ProjectCostCenterRequests",
                schema: "engineer",
                newName: "ProjectCostCenterRequest",
                newSchema: "engineer");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectCostCenterRequests_ProjectId",
                schema: "engineer",
                table: "ProjectCostCenterRequest",
                newName: "IX_ProjectCostCenterRequest_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectCostCenterRequests_CostCenterId",
                schema: "engineer",
                table: "ProjectCostCenterRequest",
                newName: "IX_ProjectCostCenterRequest_CostCenterId");

            migrationBuilder.AlterColumn<long>(
                name: "UpdaterId",
                schema: "engineer",
                table: "ProjectCostCenterRequest",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "ویرایش کننده");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                schema: "engineer",
                table: "ProjectCostCenterRequest",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldComment: "تاریخ ویرایش");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                schema: "engineer",
                table: "ProjectCostCenterRequest",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "حذف شدگی");

            migrationBuilder.AlterColumn<long>(
                name: "CreatorId",
                schema: "engineer",
                table: "ProjectCostCenterRequest",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "ایجاد کننده");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                schema: "engineer",
                table: "ProjectCostCenterRequest",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "تاریخ ایجاد");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                schema: "engineer",
                table: "ProjectCostCenterRequest",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "شناسه")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "2, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectCostCenterRequest",
                schema: "engineer",
                table: "ProjectCostCenterRequest",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectCostCenterRequest_CostCenters_CostCenterId",
                schema: "engineer",
                table: "ProjectCostCenterRequest",
                column: "CostCenterId",
                principalSchema: "engineer",
                principalTable: "CostCenters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectCostCenterRequest_Projects_ProjectId",
                schema: "engineer",
                table: "ProjectCostCenterRequest",
                column: "ProjectId",
                principalSchema: "engineer",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
