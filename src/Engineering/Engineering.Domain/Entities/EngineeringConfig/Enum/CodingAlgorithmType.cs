namespace Engineering.Domain.Entities.EngineeringConfig.Enum;

public enum CodingAlgorithmType
{
    [Description("درخواست تامین کالایی")]
    ProductRGS = 11,
    [Description("درخواست تامین خدمتی")]
    ServiceRGS = 12,
    [Description("درخواست تامین تبلیغاتی")]
    AdsRGS = 13,
    [Description("درخواست تامین پروژه ای")]
    ProjectRGS = 14,
}