using Engineering.Domain.Entities.Messengers.Enums;

namespace Engineering.Application.Services.Messengers.Contracts.GetMessengerById;

public record GetMessengerChannelByIdResponse
{
    public long Id { get; set; }
    public string ChatId { get; set; } = string.Empty;
    public string? ChatUrl { get; set; }
    public string? ChatName { get; set; } = string.Empty;
    public MessengerMessageType MessengerMessageType { get; set; }
    public long MessengerId { get; set; }
}