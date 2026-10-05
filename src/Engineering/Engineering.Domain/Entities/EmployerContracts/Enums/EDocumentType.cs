namespace Engineering.Domain.Entities.EmployerContracts;

[Description(EContractCmts.DocumentType)]
public enum EDocumentType
{
    [Description("تحویل زمین")]
    DeliveringLand = 1,

    [Description("نسخه قرارداد")]
    ContractVersion = 2,

    [Description("پیش پرداخت")]
    PrePayment = 3,

    [Description("نقشه ها")]
    Plot = 4,

    [Description("ابلاغیه")]
    Announcement = 5,

    [Description("سایر")]
    Other = 6,
}