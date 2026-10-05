using Engineering.Domain.Cmts;
using System.ComponentModel;

namespace Engineering.Api.Controllers.ProjectWbses.Contracts.GetFltrProjectWbs;

public enum FltrProjectWbsEnum
{
    [Description(GlobalCmts.Id)]
    Id = 1,

    [Description(GlobalCmts.Title)]
    Title,

    [Description(GlobalCmts.Code)]
    Code,

    [Description(GlobalCmts.ProjectId)]
    ProjectId,

    [Description(ProjectCmts.ProjectName)]
    ProjectName,

    [Description(ProjectCmts.ProjectCode)]
    ProjectCode,

    [Description(WbsCmts.WbsTemplate)]
    WbsTemplateId,

    [Description(WbsCmts.WbsTemplateTitle)]
    WbsTemplateTitle,

    [Description(WbsCmts.WbsTemplateCode)]
    WbsTemplateCode,

    [Description(GlobalCmts.IsActive)]
    IsActive
}