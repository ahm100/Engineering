using Engineering.Application.Services.TelegramChats.Models.ResponseModels;
using Engineering.Domain.Entities.TelegramChats.Enums;

namespace Engineering.Application.Services.TelegramChats.Queries.GetByTransportationRequestId;

public record GetByTransportationRequestIdQuery(
    long TransportationRequestId, bool? GetOther, TelegramMessageType telegramMessageType
    ) : IQuery<DataResult<List<TelegramMessageResponseModel>>>;