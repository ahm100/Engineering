namespace Engineering.Application.Services.Messengers.Contracts.CreateMessenger;

public record RetrySendMessageRequest(
    List<long> ids);
