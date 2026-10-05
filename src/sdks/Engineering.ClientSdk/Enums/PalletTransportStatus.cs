using System.ComponentModel;

namespace Engineering.ClientSdk.Enums;

public enum PalletTransportStatus
{
    [Description("همه دارای ترابری هستند")]
    AllHaveTransport = 1,

    [Description("همه فاقد ترابری هستند")]
    AllDontHaveTransport = 2,

    [Description("حداقل یک پالت فاقد ترابری است")]
    SomeDontHaveTransport = 3,
}
