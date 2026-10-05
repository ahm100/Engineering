using System.ComponentModel;

namespace Engineering.ClientSdk.Enums;

[Flags]
public enum DeliveryMethod
{
    [Description("سایر")]
    None = 0,

    [Description("زمینی")]
    GroundTransport = 0x1,

    [Description("دریایی")]
    SeaTransport = 0x2,

    [Description("هوایی")]
    AirTransport = 0x4,

    [Description("ریلی")]
    RailTransport = 0x8,
}