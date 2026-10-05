using ExpertModel = Engineering.Application.WebServices.MetaDataServices.Experts.Models.Expert;

namespace Engineering.Application.WebServices.MetaDataServices.Experts.Queries.GetExpertById;

public record GetExpertByIdQuery(
    long Id
    ) : IQuery<ExpertModel?>;
