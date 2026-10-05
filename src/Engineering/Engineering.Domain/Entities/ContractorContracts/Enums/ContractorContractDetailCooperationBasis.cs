namespace Engineering.Domain.Entities.ContractorContracts.Enums;

public enum ContractorContractDetailCooperationBasis
{
    [Description("روزانه")]
    Daily = 1,

    [Description("ساعتی")]
    Hourly = 2,

    [Description("ماهانه")]
    Monthly = 3,

    [Description("تحویل محور")]
    DeliverableBased = 4,
}
