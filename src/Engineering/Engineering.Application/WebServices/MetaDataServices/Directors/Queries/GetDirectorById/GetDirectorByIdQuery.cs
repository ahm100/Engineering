using DirectorModel = Engineering.Application.WebServices.MetaDataServices.Directors.Models.Director;

namespace Engineering.Application.WebServices.MetaDataServices.Directors.Queries.GetDirectorById;

public record GetDirectorByIdQuery(
    long Id
    ) : IQuery<DirectorModel?>;
