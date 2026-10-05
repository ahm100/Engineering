using Engineering.Application.Services.RequestGoodsSupplies.Models.GetFltrProducts;

namespace Engineering.Application.Services.RequestGoodsSupplies.Queries.GetFltrProducts;

public record GetFltrProductsQuery(long ProjectId,
    string? FilterData,
    int PageIndex,
    int PageSize) : IQuery<GetFltrProductsResponse?>;