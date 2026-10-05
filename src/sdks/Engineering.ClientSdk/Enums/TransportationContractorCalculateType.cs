using System.ComponentModel;

namespace Engineering.ClientSdk.Enums;

public enum TransportationContractorCalculateType
{
    [Description("مسافتی")]
    Distance = 1,
    [Description("وزن")]
    Weight = 2,
    [Description("سایر")]
    Price = 3
}
