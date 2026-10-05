using System.ComponentModel;

namespace Engineering.ClientSdk.Enums;

[Flags]
public enum DeliveryType
{
    [Description("سایر")]
    None = 0,

    [Description("عادی")]
    Normal = 0x1,

    [Description("فوری")]
    Quick = 0x2,
}
