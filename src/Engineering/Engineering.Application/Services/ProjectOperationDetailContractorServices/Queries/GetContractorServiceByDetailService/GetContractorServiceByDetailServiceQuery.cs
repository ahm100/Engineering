using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Queries.GetContractorServiceByDetailService;

public record GetContractorServiceByDetailServiceQuery(
    long ProjectOperationDetailId,
    long ServiceInfoId
    ) : IQuery<ProjectOperationDetailContractorService?>;