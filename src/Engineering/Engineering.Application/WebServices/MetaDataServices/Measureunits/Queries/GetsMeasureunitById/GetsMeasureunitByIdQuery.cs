using Engineering.Domain.Entities.Synonyms.MetaData.MeasureUnits;

namespace Engineering.Application.WebServices.MetaDataServices.Measureunits.Queries.GetsMeasureunitById;

public record GetsMeasureunitByIdQuery(
    int PageIndex,
    int PageSize,
    List<long> Ids,
    bool IgnoreQuery
    ) : IQuery<DataResult<List<MeasureUnit>>>;
