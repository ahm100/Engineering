
namespace Engineering.Application.WebServices.MetaDataServices.Cities.Models.GetsCityById;

public record GetsCityByIdRequest(
    int PageIndex,
    int PageSize,
    List<long> Ids,
    string? FilterData,
    bool IgnoreQuery
    );
