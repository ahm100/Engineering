using Engineering.Application.Services.ProjectOperationDetailContractorExperts.Contracts.CreatePODContractorExperts;
using Engineering.Application.Services.ProjectOperationDetailContractorExperts.Contracts.DeletePODContractorExperts;
using Engineering.Application.Services.ProjectOperationDetailContractorExperts.Contracts.GetPODContractorExpertsByCServiceId;
using Engineering.Application.Services.ProjectOperationDetailContractorExperts.Contracts.GetPODContractorExpertsByCVEId;
using Engineering.Application.Services.ProjectOperationDetailContractorExperts.Contracts.UpdatePODContractorExperts;

namespace Engineering.Application.Services.ProjectOperationDetailContractorExperts;

public interface IProjectOperationDetailContractorExpertLogic
{
    Task<Result<CreatePODContractorExpertsResponse?>> CreatePODContractorExperts(
        CreatePODContractorExpertsRequest request, CT ct);

    Task<Result<UpdatePODContractorExpertsResponse?>> UpdatePODContractorExperts(
        UpdatePODContractorExpertsRequest request, CT ct);

    Task<Result<DeletePODContractorExpertsResponse?>> DeletePODContractorExperts(
        DeletePODContractorExpertsRequest request, CT ct);

    Task<Result<GetPODContractorExpertsByCServiceIdResponse?>> GetPODContractorExpertsByCServiceId(
        GetPODContractorExpertsByCServiceIdRequest request, CT ct);

    Task<Result<GetPODContractorExpertsByCVEIdResponse?>> GetPODContractorExpertsByCVEId(
        GetPODContractorExpertsByCVEIdRequest request, CT ct);
}
