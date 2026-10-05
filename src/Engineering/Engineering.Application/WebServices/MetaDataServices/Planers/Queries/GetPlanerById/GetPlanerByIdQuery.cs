using PlanerModel = Engineering.Application.WebServices.MetaDataServices.Planers.Models.Planer;

namespace Engineering.Application.WebServices.MetaDataServices.Planers.Queries.GetPlanerById;

public record GetPlanerByIdQuery(
    long Id
    ) : IQuery<PlanerModel?>;
