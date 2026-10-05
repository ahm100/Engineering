using Engineering.Domain.Entities.Messengers.Enums;

namespace Engineering.Application.Services.Messengers.Contracts.GetMessengers;

public record GetMessengerChannelsResponse(
    List<GetMessengerChannelsModel> Data,
    int RowCount);

public class GetMessengerChannelsModel : IUserAuditable
{
    public long Id { get; set; }
    public string ChatId { get; set; } = string.Empty;
    public string? ChatUrl { get; set; } = string.Empty;
    public string? ChatName { get; set; } = string.Empty;
    public MessengerMessageType MessengerMessageType { get; set; }
    public string MessengerMessageTypeDesc => MessengerMessageType.GetEnumDescription();

    public long MessengerId { get; set; }
    public MessengerType MessengerType { get; set; }
    public string MessengerTypeDesc => MessengerType.GetEnumDescription();
    public MessengerTargetType MessengerTargetType { get; set; }
    public string MessengerTargetTypeDesc => MessengerTargetType.GetEnumDescription();

    public long CreatorId { get; set; }
    public string? Creator { get; set; } = string.Empty;
    public DateTime Created { get; set; }
    public string CreatedShamsi => Created.ToShamsi();
    public long? UpdaterId { get; set; }
    public string? Updater { get; set; } = string.Empty;
    public DateTime? Updated { get; set; }
    public string? UpdatedShamsi => Updated.ToShamsi();
}