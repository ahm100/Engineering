
namespace Engineering.Domain.Entities.EmployerStatusStatements.Enums;

public enum EmployerStatusStatementType
{
    [Description("پایان کار")]
    EndOfWork = 1,

    [Description("تحویل موقت")]
    TemporaryDelivery = 2,

    [Description("شروع نشده")]
    NotStarted = 3

}