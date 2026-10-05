using Engineering.Domain.Entities.Messengers.Enums;

namespace Engineering.Application.Services.MessengerChannelHistories.Contracts.GetMessengerChannelHistories;

public record GetMessengerChannelHistoriesResponse(
    List<GetMessengerChannelHistoriesModel>? Data,
    int RowCount
    );

public record GetMessengerChannelHistoriesModel : IUserAuditable
{
    public long Id { get; set; }
    public string? ChatId { get; set; }
    public string? Message { get; set; }
    public string? ErrorMessage { get; set; }
    public string? FileUrls { get; set; }
    public bool? IsSend { get; set; }

    public long MessengerChannelId { get; set; }
    public MessengerMessageType MessengerMessageType { get; set; }
    public string MessengerMessageTypeDescription => MessengerMessageType.GetEnumDescription();

    public long CreatorId { get; set; }
    public string? Creator { get; set; } = string.Empty;
    public DateTime Created { get; set; }
    public string CreatedShamsi => Created.ToShamsi();
    public long? UpdaterId { get; set; }
    public string? Updater { get; set; } = string.Empty;
    public DateTime? Updated { get; set; }
    public string? UpdatedShamsi => Updated.ToShamsi();
}