using System.ComponentModel;

namespace Engineering.Application.Services.RequestGoodsSupplies.Models.GetsRequestGoodsSupplyExcelEnums;

public enum RequestGoodsSupplyExcelEnum
{
    [Description("ردیف")]
    Row = 0,

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

    [Description("عنوان خصوصی")]
    PrivateName = 17,

    [Description("کد خصوصی")]
    PrivateCode = 18,

    [Description("عنوان عمومی")]
    PublicName = 19,

    [Description("کد عمومی")]
    PublicCode = 20,

    [Description("توضیحات ریزمتره")]
    ProjectOperationDetailDescription = 21,

    [Description("تاریخ درخواست")]
    RequestedDate = 22
}