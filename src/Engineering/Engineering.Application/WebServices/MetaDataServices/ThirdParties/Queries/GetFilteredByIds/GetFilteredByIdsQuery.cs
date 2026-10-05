using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.GetFilteredByIds;

namespace Engineering.Application.WebServices.MetaDataServices.ThirdParties.Queries.GetFilteredByIds;

public record GetFilteredByIdsQuery(
    List<long> Ids,
    string? FilterData,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<FilteredUserModel?>?>?>;
