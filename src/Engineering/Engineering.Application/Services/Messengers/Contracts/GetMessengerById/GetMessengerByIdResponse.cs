using Engineering.Domain.Entities.Messengers.Enums;

namespace Engineering.Application.Services.Messengers.Contracts.GetMessengerById;

public record GetMessengerByIdResponse
{
    public long Id { get; set; }
    public MessengerType MessengerType { get; set; }
    public string MessengerTypeDescription => MessengerType.GetEnumDescription();
    public MessengerTargetType MessengerTargetType { get; set; }
    public string MessengerTargetTypeDescription => MessengerTargetType.GetEnumDescription();
    public long TargetId { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; }
}