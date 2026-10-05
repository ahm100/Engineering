using MeasureunitModel = Engineering.Application.WebServices.MetaDataServices.Measureunits.Models.Measureunit;

namespace Engineering.Application.WebServices.MetaDataServices.Measureunits.Queries.GetMeasureunitById;

public record GetMeasureunitByIdQuery(
    long Id
    ) : IQuery<MeasureunitModel?>;
