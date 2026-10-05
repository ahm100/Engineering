using ConsultantModel = Engineering.Application.WebServices.MetaDataServices.Consultants.Models.Consultant;

namespace Engineering.Application.WebServices.MetaDataServices.Consultants.Queries.GetConsultantById;

public record GetConsultantByIdQuery(
    long Id
    ) : IQuery<ConsultantModel?>;
