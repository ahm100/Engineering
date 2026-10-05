namespace Engineering.Domain.Entities.ContractorContracts.Enums;

public enum ContractorContractType
{
    [Description("مقطوع")]
    Fixed = 1,

    [Description("شرح خدمتی")]
    Service = 2,

    [Description("شرح عملیاتی")]
    OperationBased = 3,

    [Description("نفر-روز")]
    ProfessionalWorkday = 4,

    [Description("دستمزدی")]
    SalaryBase = 5,

    [Description("تولیدی")]
    Manufacturing = 6,

    //[Description("هزینه به علاوه کارمزد")]
    //CostPlus = 7,

    //[Description("زمان و مصالح")]
    //TimeAndMaterial = 8,

}
