using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangePODependency : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectOperationDependencies_ProjectOperations_ProjectOperationId",
                schema: "engineer",
                table: "ProjectOperationDependencies");

            migrationBuilder.RenameColumn(
                name: "RelationId",
                schema: "engineer",
                table: "ProjectOperationDependencies",
                newName: "SuccessorId");

            migrationBuilder.RenameColumn(
                name: "RelationDays",
                schema: "engineer",
                table: "ProjectOperationDependencies",
                newName: "LagDays");

            migrationBuilder.RenameColumn(
                name: "ProjectOperationId",
                schema: "engineer",
                table: "ProjectOperationDependencies",
                newName: "PredecessorId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectOperationDependencies_ProjectOperationId",
                schema: "engineer",
                table: "ProjectOperationDependencies",
                newName: "IX_ProjectOperationDependencies_PredecessorId");

            migrationBuilder.AlterColumn<long>(
                name: "UpdaterId",
                schema: "engineer",
                table: "ProjectOperationDependencies",
                type: "bigint",
                nullable: true,
                comment: "ویرایش کننده",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                schema: "engineer",
                table: "ProjectOperationDependencies",
                type: "datetime2",
                nullable: true,
                comment: "تاریخ ویرایش",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                schema: "engineer",
                table: "ProjectOperationDependencies",
                type: "bit",
                nullable: false,
                comment: "حذف شدگی",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<long>(
                name: "CreatorId",
                schema: "engineer",
                table: "ProjectOperationDependencies",
                type: "bigint",
                nullable: false,
                comment: "ایجاد کننده",
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                schema: "engineer",
                table: "ProjectOperationDependencies",
                type: "datetime2",
                nullable: false,
                comment: "تاریخ ایجاد",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                schema: "engineer",
                table: "ProjectOperationDependencies",
                type: "bigint",
                nullable: false,
                comment: "شناسه",
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("SqlServer:Identity", "2, 1")
                .OldAnnotation("SqlServer:Identity", "2, 1");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectOperationDependencies_SuccessorId",
                schema: "engineer",
                table: "ProjectOperationDependencies",
                column: "SuccessorId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectOperationDependencies_ProjectOperations_PredecessorId",
                schema: "engineer",
                table: "ProjectOperationDependencies",
                column: "PredecessorId",
                principalSchema: "engineer",
                principalTable: "ProjectOperations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectOperationDependencies_ProjectOperations_SuccessorId",
                schema: "engineer",
                table: "ProjectOperationDependencies",
                column: "SuccessorId",
                principalSchema: "engineer",
                principalTable: "ProjectOperations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectOperationDependencies_ProjectOperations_PredecessorId",
                schema: "engineer",
                table: "ProjectOperationDependencies");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectOperationDependencies_ProjectOperations_SuccessorId",
                schema: "engineer",
                table: "ProjectOperationDependencies");

            migrationBuilder.DropIndex(
                name: "IX_ProjectOperationDependencies_SuccessorId",
                schema: "engineer",
                table: "ProjectOperationDependencies");

            migrationBuilder.RenameColumn(
                name: "SuccessorId",
                schema: "engineer",
                table: "ProjectOperationDependencies",
                newName: "RelationId");

            migrationBuilder.RenameColumn(
                name: "PredecessorId",
                schema: "engineer",
                table: "ProjectOperationDependencies",
                newName: "ProjectOperationId");

            migrationBuilder.RenameColumn(
                name: "LagDays",
                schema: "engineer",
                table: "ProjectOperationDependencies",
                newName: "RelationDays");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectOperationDependencies_PredecessorId",
                schema: "engineer",
                table: "ProjectOperationDependencies",
                newName: "IX_ProjectOperationDependencies_ProjectOperationId");

            migrationBuilder.AlterColumn<long>(
                name: "UpdaterId",
                schema: "engineer",
                table: "ProjectOperationDependencies",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "ویرایش کننده");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                schema: "engineer",
                table: "ProjectOperationDependencies",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldComment: "تاریخ ویرایش");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                schema: "engineer",
                table: "ProjectOperationDependencies",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "حذف شدگی");

            migrationBuilder.AlterColumn<long>(
                name: "CreatorId",
                schema: "engineer",
                table: "ProjectOperationDependencies",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "ایجاد کننده");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                schema: "engineer",
                table: "ProjectOperationDependencies",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "تاریخ ایجاد");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                schema: "engineer",
                table: "ProjectOperationDependencies",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "شناسه")
                .Annotation("SqlServer:Identity", "2, 1")
                .OldAnnotation("SqlServer:Identity", "2, 1");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectOperationDependencies_ProjectOperations_ProjectOperationId",
                schema: "engineer",
                table: "ProjectOperationDependencies",
                column: "ProjectOperationId",
                principalSchema: "engineer",
                principalTable: "ProjectOperations",
                principalColumn: "Id");
        }
    }
}
