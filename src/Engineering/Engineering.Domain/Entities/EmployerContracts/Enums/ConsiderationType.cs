namespace Engineering.Domain.Entities.EmployerContracts;

[Description(EContractCmts.ConsiderationType)]
public enum ConsiderationType
{
    [Description("اجرایی")]
    Executive = 1,

    [Description("مهندسی")]
    Engineering = 2,

    [Description("بازرگانی")]
    Commerce = 3
}