using Engineering.Domain.Cmts;
using System.ComponentModel;

namespace Engineering.Api.Controllers.WbsTemplates.Contracts.GetFltrWbsTemplate;

public enum WbsTemplateEnum
{
    [Description(GlobalCmts.Id)]
    Id = 1,

    [Description(GlobalCmts.Title)]
    Title,

    [Description(GlobalCmts.Code)]
    Code,

    [Description(GlobalCmts.IsActive)]
    IsActive
}