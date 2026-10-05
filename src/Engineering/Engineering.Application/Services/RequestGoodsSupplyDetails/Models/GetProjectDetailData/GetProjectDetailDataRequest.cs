using Engineering.Domain.Entities.Projects.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetProjectDetailData;

public record GetProjectDetailDataRequest(
    long ProjectId,
    ProjectProductType Type,
    long ProductGroupId,
    string? FilterData,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;