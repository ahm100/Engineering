using Engineering.Domain.Cmts;
using System.ComponentModel;

namespace Engineering.Api.Controllers.Messengers.Contracts.GetMessengerChannelsToExcel;

public enum MessengerChannelsEnumExcel
{
    [Description(GlobalCmts.Id)]
    Id = 1,

    [Description(MessengerCmts.MessengerType)]
    MessengerTypeDescription,

    [Description(MessengerCmts.MessengerTargetType)]
    MessengerTargetTypeDescription,

    [Description(MessengerCmts.TargetId)]
    TargetId,

    [Description(GlobalCmts.Description)]
    Description,

    [Description(GlobalCmts.IsActive)]
    IsActive,

    [Description(GlobalCmts.Creator)]
    Creator,

    [Description(GlobalCmts.Created)]
    Created,

    [Description(GlobalCmts.CreatedShamsi)]
    CreatedShamsi,

    [Description(GlobalCmts.Updater)]
    Updater,

    [Description(GlobalCmts.Updated)]
    Updated,

    [Description(GlobalCmts.UpdatedShamsi)]
    UpdatedShamsi,
}
