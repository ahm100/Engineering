
namespace Engineering.Application.Services.RequestGoodsSupplies.Models.RequestGoodsSuppliesGroupDelete;

public record RequestGoodsSuppliesGroupDeleteRequest(
    List<long> Ids
    ) : IHttpRequest;
