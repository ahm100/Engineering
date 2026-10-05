using System.ComponentModel;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetProjectOperationDetailsByRequestId;

public enum DividerByProjectOperationDetailsType
{
    [Description("تقسیم برابر")]
    ByEqualDivision = 1,
    [Description("تقسیم بر اساس حجم ریز متره")]
    ByOperationDetailNumber = 2,
    [Description("تقسیم دلخواه")]
    ArbitraryDivision = 3,
}
