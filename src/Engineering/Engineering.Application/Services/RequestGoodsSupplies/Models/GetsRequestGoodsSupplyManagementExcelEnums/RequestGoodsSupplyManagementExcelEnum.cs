using System.ComponentModel;

namespace Engineering.Application.Services.RequestGoodsSupplies.Models.GetsRequestGoodsSupplyManagementExcelEnums;

public enum RequestGoodsSupplyManagementExcelEnum
{
    [Description("ردیف")]
    Row = 0,

    [Description("شناسه")]
    Id = 1,

    [Description("شناسه جزییات درخواست تامین")]
    RequestGoodsSupplyDetailId = 2,

    [Description("شناسه کالا")]
    ProductId = 3,

    [Description("شناسه انبار")]
    WarehouseId = 4,

    [Description("انبار")]
    Warehouse = 5,

    [Description("شناسه انبار مقصد")]
    DestinationWarehouseId = 6,

    [Description("انبار مقصد")]
    DestinationWarehouse = 7,

    [Description("شماره فاکتور")]
    InvoiceId = 8,

    [Description("تعداد درخواست")]
    RequestedCount = 9,

    [Description("تعداد درخواست تایید شده")]
    ConfirmedRequestCount = 10,

    [Description("شناسه آلترناتیو")]
    AlternateId = 11,

    [Description("توضیحات")]
    Description = 12,

    [Description("نوع")]
    TypeDescription = 13,

    [Description("وضعیت")]
    StatusDescription = 14,

    [Description("تاریخ واگذاری")]
    AssignmentDate = 15,

    [Description("اخرین توضیحات")]
    LastDescription = 16
}