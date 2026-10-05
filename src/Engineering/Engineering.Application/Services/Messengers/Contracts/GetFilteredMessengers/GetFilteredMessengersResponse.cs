using Engineering.Domain.Entities.Messengers.Enums;

namespace Engineering.Application.Services.Messengers.Contracts.GetMessengers;

public record GetMessengersResponse(
    List<GetMessengersModel> Data,
    int RowCount);

public record GetMessengersModel : IUserAuditable
{
    public long Id { get; set; }
    public MessengerType MessengerType { get; set; }
    public string MessengerTypeDescription => MessengerType.GetEnumDescription();
    public MessengerTargetType MessengerTargetType { get; set; }
    public string MessengerTargetTypeDescription => MessengerTargetType.GetEnumDescription();
    public long TargetId { get; set; }
    public string? Description { get; set; }

    public bool IsActive { get; set; }

    public long CreatorId { get; set; }
    public string? Creator { get; set; } = string.Empty;
    public DateTime Created { get; set; }
    public string CreatedShamsi => Created.ToShamsi();
    public long? UpdaterId { get; set; }
    public string? Updater { get; set; } = string.Empty;
    public DateTime? Updated { get; set; }
    public string? UpdatedShamsi => Updated.ToShamsi();
}