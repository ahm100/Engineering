namespace Engineering.Domain.Entities.Contracts.Enums;

public enum PricingMethod
{
    [Description(GlobalCmts.PricingMethodLumpSum)]
    LumpSum = 1,

    [Description(GlobalCmts.PricingMethodUnitPrice)]
    UnitPrice = 2,

    [Description(GlobalCmts.PricingMethodCostPlus)]
    CostPlus = 3,

    [Description(GlobalCmts.PricingMethodTimeAndMaterial)]
    TimeAndMaterial = 4
}