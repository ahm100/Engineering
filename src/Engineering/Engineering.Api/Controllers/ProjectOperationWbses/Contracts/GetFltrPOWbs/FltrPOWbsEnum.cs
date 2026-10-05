using Engineering.Domain.Cmts;
using System.ComponentModel;

namespace Engineering.Api.Controllers.ProjectOperationWbses.Contracts.GetFltrPOWbs;

public enum FltrPOWbsEnum
{
    [Description(GlobalCmts.Id)]
    Id = 1,

    [Description(GlobalCmts.Title)]
    Title,

    [Description(GlobalCmts.Code)]
    Code,

    [Description(WbsCmts.ProjectWbsId)]
    ProjectWbsId,

    [Description(WbsCmts.ProjectWbsTitle)]
    ProjectWbsTitle,

    [Description(WbsCmts.ProjectWbsCode)]
    ProjectWbsCode,

    [Description(GlobalCmts.ProjectOperationId)]
    ProjectOperationId,

    [Description(GlobalCmts.Description)]
    ProjectOperationDescription,

    [Description(GlobalCmts.OperationInfoId)]
    OperationInfoId,

    [Description(OperationInfoCmts.OperationInfoName)]
    OperationInfoName,

    [Description(OperationInfoCmts.OperationInfoCode)]
    OperationInfoCode,

    [Description(GlobalCmts.CreatorId)]
    CreatorId,

    [Description(GlobalCmts.Creator)]
    Creator,

    [Description(GlobalCmts.IsActive)]
    IsActive
}