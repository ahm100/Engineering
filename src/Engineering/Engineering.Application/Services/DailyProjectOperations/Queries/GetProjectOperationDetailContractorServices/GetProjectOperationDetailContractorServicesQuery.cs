using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Application.Services.DailyProjectOperations.Queries.GetProjectOperationDetailContractorServices;

public record GetProjectOperationDetailContractorServicesQuery(
    long ProjectOperationDetailId
    ) : IQuery<DataResult<List<ProjectOperationDetailContractorService>>>;
