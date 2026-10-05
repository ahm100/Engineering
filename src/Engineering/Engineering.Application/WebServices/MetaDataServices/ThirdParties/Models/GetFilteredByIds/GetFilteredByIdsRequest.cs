namespace Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.GetFilteredByIds;

public record GetFilteredByIdsRequest(
    List<long> Ids,
    string? FilterData,
    int PageIndex,
    int PageSize
    );

