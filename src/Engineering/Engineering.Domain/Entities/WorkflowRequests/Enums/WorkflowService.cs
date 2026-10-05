namespace Engineering.Domain.Entities.WorkflowRequests.Enums;

/// <summary>سرویس درخواست دهنده اجرای گردش کار؛ مقادیر عددی بخشی از قرارداد ارتباطی هستند.</summary>
public enum WorkflowService
{
    [Description("مهندسی")]
    Engineering = 1,

    [Description("بازرگانی")]
    Commercial = 2,

    [Description("انبار")]
    Warehouse = 3,

    [Description("مالی")]
    Financial = 4
}
