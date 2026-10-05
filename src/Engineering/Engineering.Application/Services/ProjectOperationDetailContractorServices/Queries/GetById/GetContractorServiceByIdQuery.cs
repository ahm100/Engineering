using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Queries.GetById;

public record GetContractorServiceByIdQuery(
    long Id
    ) : IQuery<ProjectOperationDetailContractorService?>;