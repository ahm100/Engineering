using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations;

/// <summary>شناسه داخلی را کلید اصلی می کند و یکتایی رویداد دریافتی را حفظ می کند.</summary>
public partial class WorkflowInboxPrimaryKey : Migration
{
    /// <summary>بدون تغییر شناسه ها یا حذف رسیدها، کلید اصلی و ایندکس رویداد را تنظیم می کند.</summary>
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropPrimaryKey(name: "PK_WorkflowInbox", schema: "engineer", table: "WorkflowInbox");
        migrationBuilder.AddPrimaryKey(name: "PK_WorkflowInbox", schema: "engineer", table: "WorkflowInbox", column: "Id");
        migrationBuilder.CreateIndex(name: "IX_WorkflowInbox_EventId", schema: "engineer", table: "WorkflowInbox", column: "EventId", unique: true);
    }

    /// <summary>چیدمان قبلی کلید را بدون تغییر مقادیر رکوردها بازمی گرداند.</summary>
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropIndex(name: "IX_WorkflowInbox_EventId", schema: "engineer", table: "WorkflowInbox");
        migrationBuilder.DropPrimaryKey(name: "PK_WorkflowInbox", schema: "engineer", table: "WorkflowInbox");
        migrationBuilder.AddPrimaryKey(name: "PK_WorkflowInbox", schema: "engineer", table: "WorkflowInbox", column: "EventId");
    }
}