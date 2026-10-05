namespace Engineering.Domain.Entities.Transportations.Enums;

public enum TransportationPaymentType
{
    [Description("پرداخت نشده")] NotPaid = 1,

    [Description("حواله")] StatementPaid = 2,

    [Description("تنخواه گردان")] PettyCash = 3,

    [Description("۴۸۱۸")] Panel4818 = 4,
}