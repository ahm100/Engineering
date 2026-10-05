namespace Engineering.Application.WebServices.WarehouseServices.Products.Models.GetProductByGroupIds;

public record GetProductByGroupIdsRequest(
    List<long> GroupIds,
    string? FilterData
    );
