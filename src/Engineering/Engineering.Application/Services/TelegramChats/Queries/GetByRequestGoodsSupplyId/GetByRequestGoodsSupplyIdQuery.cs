using Engineering.Domain.Entities.TelegramChats;

namespace Engineering.Application.Services.TelegramChats.Queries.GetByRequestGoodsSupplyId;

public record GetByRequestGoodsSupplyIdQuery(
    long RequestGoodsSupplyId, bool? GetOther
    ) : IQuery<DataResult<List<TelegramChat>>>;