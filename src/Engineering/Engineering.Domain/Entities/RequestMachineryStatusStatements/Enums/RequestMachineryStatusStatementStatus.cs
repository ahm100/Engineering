namespace Engineering.Domain.Entities.RequestMachineryStatusStatements.Enums;

public enum RequestMachineryStatusStatementStatus
{
    [Description("ثبت اولیه")]
    New = 1,
    [Description("رد شده")]
    Rejected = 10,
    [Description("تایید شده")]
    Confirmed = 20,
    [Description("باطل شده")]
    Invalidated = 30,
    [Description("تایید جهت پرداخت")]
    PaymentConfirmation = 40,
    [Description("صدور دستور پرداخت")]
    Paid = 50,
    [Description("بدون پرداختی")]
    NotPaid = 60,
    [Description("پرداخت شده")]
    PaidDone = 70,
    [Description("رد دستور پرداخت")]
    PaidRejected = 80,
    [Description("بخشی پرداخت شده")]
    IncompletelyPaid = 90,
}