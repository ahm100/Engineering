using Engineering.Application.Services.ProjectOperationDetailContractorExperts.Contracts.GetPODContractorExpertsByCVEId;

namespace Engineering.Application.Services.ProjectOperationDetailContractorExperts.Queries.GetPODContractorExpertsByCVEId;

public record GetPODContractorExpertsByCVEIdQuery(
    long ConsumableVolumeExpertId,
    int PageIndex,
    int PageSize) : IQuery<GetPODContractorExpertsByCVEIdResponse?>;