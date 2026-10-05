namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetProductsByProjectId;

public record GetProductsByProjectIdRequest(
    long ProjectId,
    string? FilterData,
    long? ContractorId,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;
