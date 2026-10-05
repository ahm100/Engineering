namespace Engineering.Domain.Entities.EmployerContracts;

[Description(EContractCmts.ConsiderationType)]
public enum EContractType
{
    [Description("یک عامله")]
    OneAgent = 1,

    [Description("دو عامله")]
    TowAgent = 2,

    [Description("سه عامله")]
    ThreeAgent = 3,

    [Description("چهار عامله")]
    FourAgent = 4
}