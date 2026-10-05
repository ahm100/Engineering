using Engineering.Domain.Cmts;
using System.ComponentModel;

namespace Engineering.Api.Controllers.RequestGoodsSupplies.Contracts.GetRGSTypeByRGSId;

public enum RGSTypeEnum
{
    [Description(RGSCmts.Importance)]
    ImportanceDescription = 1,

    [Description(RGSCmts.Reference)]
    ReferenceName,

    [Description(RGSCmts.Type)]
    TypeDescription,

    [Description(RGSCmts.RequestedCount)]
    RequestedCount,

    [Description(RGSCmts.DeliveryDeadLine)]
    DelivaryDeadLineShamsi,

    [Description(RGSCmts.UnitPrice)]
    UnitPrice,

    [Description(RGSCmts.TotalPrice)]
    TotalPrice,

    [Description(RGSCmts.PackingPrice)]
    PackingPrice,

    [Description(RGSCmts.FinalPrice)]
    FinalPrice,

    [Description(GlobalCmts.Description)]
    Description,

    [Description(RGSCmts.ManagementDescription)]
    ManagementDescription,

    [Description(GlobalCmts.Contractor)]
    Contractor,

    [Description(RGSCmts.PackageCount)]
    PackageCount,

    [Description(RGSCmts.PackageUnitPrice)]
    PackageUnitPrice,

    [Description(RGSCmts.LastDescription)]
    LastDescription
}