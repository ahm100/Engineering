using Engineering.Domain.Cmts;
using System.ComponentModel;

namespace Engineering.Api.Controllers.RequestGoodsSupplies.Contracts.GetDetailByRGSTypeId;

public enum DetailRGSTypeDetailEnum
{
    [Description(GlobalCmts.CostCenter)]
    CostCenterName = 1,

    [Description(RGSCmts.DeliveryDeadLine)]
    DeliveryDeadLineShamsi,

    [Description(RGSCmts.RequestedCount)]
    RequestedCount
}