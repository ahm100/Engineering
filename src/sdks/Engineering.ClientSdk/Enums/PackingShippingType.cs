using System.ComponentModel;

namespace Engineering.ClientSdk.Enums;

public enum PackingShippingType
{
    [Description("پیمانکاری")]
    Contracting = 1,

    [Description("آژانسی")]
    Agency = 2,

    [Description("تحویل حضوری(کارگزار)")]
    InPersonDelivery = 3,
}
