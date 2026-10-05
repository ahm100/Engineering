namespace Engineering.Application.Services.RequestGoodsSupplies.Models.GetsRequestGoodSupplyRequester;

public record GetsRequestGoodSupplyRequesterRequest(
    string? FilterData,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;

