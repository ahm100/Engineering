namespace Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

public enum ServiceReasonType
{
    [Description("تعمیرات")]
    Repair = 1,

    [Description("کالیبراسیون")]
    Calibration = 2,

    [Description("تولید")]
    Production = 3,

    [Description("رفع عیب")]
    Troubleshooting = 4,

    [Description("ارسال نمونه")]
    SampleShipment = 5,

    [Description("مرسولات اداری")]
    AdministrativeShipment = 6,

    [Description("قرارداد خدمات")]
    ServiceContract = 7,

    [Description("خدمات")]
    Services = 8
}