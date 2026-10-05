namespace Engineering.Application.Services.RequestGoodsSupplies.Queries.GetProjectNameRequestGoodsSupply;

public record GetProjectNameRequestGoodsSupplyQuery(
    long Id
) : IQuery<string>;