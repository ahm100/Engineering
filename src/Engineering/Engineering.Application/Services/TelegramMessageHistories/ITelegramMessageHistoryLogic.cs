using Engineering.Application.Services.TelegramMessageHistorys.Models.Create;
using Engineering.Application.Services.TelegramMessageHistorys.Models.ExecuteSendTelegramMessage;
using Engineering.Application.Services.TelegramMessageHistorys.Models.GetsFiltered;

namespace Engineering.Application.Services.TelegramMessageHistorys;

public interface ITelegramMessageHistoryLogic
{
    Task<Result<GetsFilteredTelegramMessageHistoryResponse?>> GetsFilteredTelegramMessageHistory(
        GetsFilteredTelegramMessageHistoryRequest request, CT ct);

    Task<Result<ExecuteSendTelegramMessageResponse?>> ExecuteSendTelegramMessage(
        ExecuteSendTelegramMessageRequest request, CT ct);

    Task<Result<CreateResponse?>> Create(
        CreateRequest request, CT ct);
}