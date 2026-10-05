using Engineering.Domain.Cmts;
using System.ComponentModel;

namespace Engineering.Api.Controllers.Messengers.Contracts.GetMessengerChannelHistoriesToExcel;

public enum MessengerChannelHistoryExcelColumn
{
    [Description(GlobalCmts.Id)]
    Id = 1,

    [Description(MessengerCmts.ChatId)]
    ChatId,

    [Description(MessengerCmts.Messenger)]
    Message,

    [Description(MessengerCmts.ErrorMessage)]
    ErrorMessage,

    [Description(MessengerCmts.FileUrls)]
    FileUrls,

    [Description(MessengerCmts.IsSend)]
    IsSend,

    [Description(MessengerCmts.MessengerChannelId)]
    MessengerChannelId,

    [Description(MessengerCmts.MessengerMessageType)]
    MessengerMessageTypeDescription,

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
