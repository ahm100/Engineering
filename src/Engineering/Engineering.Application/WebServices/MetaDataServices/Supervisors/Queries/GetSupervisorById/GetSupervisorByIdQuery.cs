using SupervisorModel = Engineering.Application.WebServices.MetaDataServices.Supervisors.Models.Supervisor;

namespace Engineering.Application.WebServices.MetaDataServices.Supervisors.Queries.GetSupervisorById;

public record GetSupervisorByIdQuery(
    long Id
    ) : IQuery<SupervisorModel?>;
