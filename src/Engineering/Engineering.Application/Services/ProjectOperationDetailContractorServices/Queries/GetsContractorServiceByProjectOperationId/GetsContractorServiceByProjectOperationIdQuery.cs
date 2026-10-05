using ProjectOperationDetailContractorService = Engineering.Domain.Entities.ProjectOperationDetails.ProjectOperationDetailContractorService;

namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Queries.GetsContractorServiceByProjectOperationId;

public record GetsContractorServiceByProjectOperationIdQuery(
    long ProjectOperationId
    ) : IQuery<DataResult<List<ProjectOperationDetailContractorService>>>;
