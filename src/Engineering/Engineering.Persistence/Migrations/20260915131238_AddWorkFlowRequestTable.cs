using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddWorkFlowRequestTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Id",
                schema: "engineer",
                table: "WorkflowOutboxes",
                type: "bigint",
                nullable: false,
                comment: "شناسه",
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("SqlServer:Identity", "2, 1")
                .OldAnnotation("SqlServer:Identity", "2, 1");

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                schema: "engineer",
                table: "WorkflowOutboxes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "تاریخ ایجاد");

            migrationBuilder.AddColumn<long>(
                name: "CreatorId",
                schema: "engineer",
                table: "WorkflowOutboxes",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "ایجاد کننده");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "engineer",
                table: "WorkflowOutboxes",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "حذف شدگی");

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                schema: "engineer",
                table: "WorkflowOutboxes",
                type: "datetime2",
                nullable: true,
                comment: "تاریخ ویرایش");

            migrationBuilder.AddColumn<long>(
                name: "UpdaterId",
                schema: "engineer",
                table: "WorkflowOutboxes",
                type: "bigint",
                nullable: true,
                comment: "ویرایش کننده");

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                schema: "engineer",
                table: "WorkflowInbox",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "تاریخ ایجاد");

            migrationBuilder.AddColumn<long>(
                name: "CreatorId",
                schema: "engineer",
                table: "WorkflowInbox",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "ایجاد کننده");

            migrationBuilder.AddColumn<long>(
                name: "Id",
                schema: "engineer",
                table: "WorkflowInbox",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "شناسه")
                .Annotation("SqlServer:Identity", "2, 1");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "engineer",
                table: "WorkflowInbox",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "حذف شدگی");

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                schema: "engineer",
                table: "WorkflowInbox",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                schema: "engineer",
                table: "WorkflowInbox",
                type: "datetime2",
                nullable: true,
                comment: "تاریخ ویرایش");

            migrationBuilder.AddColumn<long>(
                name: "UpdaterId",
                schema: "engineer",
                table: "WorkflowInbox",
                type: "bigint",
                nullable: true,
                comment: "ویرایش کننده");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                schema: "engineer",
                table: "WorkflowOutboxes");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                schema: "engineer",
                table: "WorkflowOutboxes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "engineer",
                table: "WorkflowOutboxes");

            migrationBuilder.DropColumn(
                name: "Updated",
                schema: "engineer",
                table: "WorkflowOutboxes");

            migrationBuilder.DropColumn(
                name: "UpdaterId",
                schema: "engineer",
                table: "WorkflowOutboxes");

            migrationBuilder.DropColumn(
                name: "Created",
                schema: "engineer",
                table: "WorkflowInbox");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                schema: "engineer",
                table: "WorkflowInbox");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "engineer",
                table: "WorkflowInbox");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "engineer",
                table: "WorkflowInbox");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                schema: "engineer",
                table: "WorkflowInbox");

            migrationBuilder.DropColumn(
                name: "Updated",
                schema: "engineer",
                table: "WorkflowInbox");

            migrationBuilder.DropColumn(
                name: "UpdaterId",
                schema: "engineer",
                table: "WorkflowInbox");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                schema: "engineer",
                table: "WorkflowOutboxes",
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
