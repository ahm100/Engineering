namespace Engineering.Domain.Entities.Transportations.Enums;

public enum TransportationType
{
    [Description("اسنپ مسافر")] SnappPassenger = 1,

    [Description("اسنپ ترابری")] SnappTransport = 2,

    [Description("اسنپ باربری")] SnappFreight = 3,

    [Description("پیک")] Courier = 4,

    [Description("آژانس")] Agency = 5,

    [Description("هواپیما")] Airplane = 6
}