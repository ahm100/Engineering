
namespace Engineering.Application.WebServices.MetaDataServices.Measureunits.Models.GetsMeasureunitById;

public record GetsMeasureunitByIdRequest(
    int PageIndex,
    int PageSize,
    List<long> Ids,
    bool IgnoreQuery
    );
