using System.ComponentModel;

namespace Engineering.Application.Services.RequestGoodsSupplies.Models.GetsRequestGoodsSupplyReportsExcelEnums;

public enum RequestGoodsSupplyReportsExcelEnum
{
    [Description("ردیف")]
    Row = 0,

    [Description("شناسه")]
    Id = 1,

    [Description("شماره درخواست")]
    RequestNumber = 2,

    [Description("شناسه مرکزهزینه")]
    CostCenterId = 3,

    [Description("مرکزهزینه")]
    CostCenterName = 4,

    [Description("شناسه پروژه")]
    ProjectId = 5,

    [Description("پروژه")]
    ProjectName = 6,

    [Description("شرح عملیات")]
    OperationInfoName = 7,

    [Description("کد شرح عملیات")]
    OperationInfoCode = 8,

    [Description("واحد سنجش")]
    MeasurementName = 9,

    [Description("حجم")]
    Workload = 10,

    [Description("عنوان خصوصی")]
    PrivateName = 11,

    [Description("کد خصوصی")]
    PrivateCode = 12,

    [Description("عنوان عمومی")]
    PublicName = 13,

    [Description("کد عمومی")]
    PublicCode = 14,

    [Description("حجم نهایی")]
    FinalAmount = 15,

    [Description("نوع درخواست")]
    TypeDescription = 16,

    [Description("وضعیت درخواست")]
    StatusDescription = 17,

    [Description("تعداد رد شده")]
    RejectedNumber = 18,

    [Description("کل خروج مصرفی")]
    AllInStock = 19,

    [Description("تعداد خروج مصرفی")]
    InStockNumber = 20,

    [Description("کل بین انباری")]
    AllBetweenStock = 21,

    [Description("تعداد بین انباری")]
    BetweenStockNumber = 22,

    [Description("کل بازرگانی")]
    AllCommerce = 23,

    [Description("تعداد بازرگانی")]
    CommerceNumber = 24,

    [Description("تاریخ درخواست")]
    CreatedOn = 25,

    [Description("شناسه درخواست دهنده")]
    CreatorId = 26,

    [Description("درخواست دهنده")]
    Creator = 27,

    [Description("الویت")]
    MaxImportanceDescription = 28,

    [Description("توضیحات ریزمتره")]
    ProjectOperationDetailDescription = 29
}