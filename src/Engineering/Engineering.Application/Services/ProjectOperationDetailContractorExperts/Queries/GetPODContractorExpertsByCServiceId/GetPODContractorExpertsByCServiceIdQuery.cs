using Engineering.Application.Services.ProjectOperationDetailContractorExperts.Contracts.GetPODContractorExpertsByCServiceId;

namespace Engineering.Application.Services.ProjectOperationDetailContractorExperts.Queries.GetPODContractorExpertsByCServiceId;

public record GetPODContractorExpertsByCServiceIdQuery(
    long ProjectOperationDetailContractorServiceId,
    int PageIndex,
    int PageSize) : IQuery<GetPODContractorExpertsByCServiceIdResponse?>;