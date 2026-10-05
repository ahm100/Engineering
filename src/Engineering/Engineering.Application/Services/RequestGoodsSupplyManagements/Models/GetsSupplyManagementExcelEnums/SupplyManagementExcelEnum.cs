using System.ComponentModel;

namespace Engineering.Application.Services.RequestGoodsSupplyManagements.Models.GetsSupplyManagementExcelEnums;

public enum SupplyManagementExcelEnum
{
    [Description("شناسه")]
    Id = 1,
    [Description("شماره درخواست")]
    RequestNumber = 2,
    [Description("نوع")]
    TypeDescription = 3,
    [Description("اهمیت")]
    MaxImportanceDescription = 4,
    [Description("وضعیت")]
    StatusDescription = 5,
    [Description("مرکزهزینه")]
    CostCenterName = 6,
    [Description("پروژه")]
    ProjectName = 7,
    [Description("نام شرح عملیات")]
    OperationInfoName = 8,
    [Description("شناسه واحد")]
    MeasurementId = 9,
    [Description("واحد")]
    MeasurementName = 10,
    [Description("حجم کار")]
    Workload = 11,
    [Description("آدرس")]
    OperationLocationName = 12,
    [Description("مقدار نهایی")]
    FinalAmount = 13,
    [Description("تاریخ ایجاد")]
    CreatedOn = 14,
    [Description("شناسه ایجاد کننده")]
    CreatorId = 15,
    [Description("ایجاد کننده")]
    Creator = 16,
    [Description("درصد")]
    Percent = 17,
    [Description("تعداد رد شده")]
    RejectedNumber = 18,
    [Description("تعداد درخواست های خروج مصرفی")]
    AllInStock = 19,
    [Description("درخواست های تایید شده خروج مصرفی")]
    InStockNumber = 20,
    [Description("تعداد درخواست های بین انباری")]
    AllBetweenStock = 21,
    [Description("درخواست های تایید شده بین انباری")]
    BetweenStockNumber = 22,
    [Description("تعداد درخواست های بازرگانی")]
    AllCommerce = 23,
    [Description("درخواست های تایید شده بازرگانی")]
    CommerceNuber = 24,
    [Description("شناسه موسسه")]
    CompanyId = 25,
    [Description("نام موسسه")]
    CompanyNameFa = 26,
}