using Engineering.Application.Services.ProjectOperationDetailContractorExperts.Contracts.GetPODContractorExpertsByCServiceId;
using Engineering.Application.Services.ProjectOperationDetailContractorExperts.Contracts.GetPODContractorExpertsByCVEId;
using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Application.Abstractions.Data.ProjectOperationDetails;

public interface IProjectOperationDetailContractorExpertRepository : IBaseRepository<ProjectOperationDetailContractorExpert>
{
    Task<ProjectOperationDetailContractorExpert?> GetById(
        long id, CT ct);

    Task<List<ProjectOperationDetailContractorExpert>?> GetByIds(
        List<long> ids, CT ct);

    Task<List<ProjectOperationDetailContractorExpert>?> GetByProjectOperationDetailContractorServiceId(
        long id, CT ct);

    Task<List<ProjectOperationDetailContractorExpert>?> GetByProjectOperationDetailContractorServiceIds(
        List<long> ids, CT ct);

    Task<(List<GetPODContractorExpertsByCServiceIdModel>? Data, int RowCount)> GetPODContractorExpertsByCServiceId(
        long contractorServiceId,
        int pageIndex,
        int pageSize, CT ct);

    Task<(List<GetPODContractorExpertsByCVEIdModel>? Data, int RowCount)> GetPODContractorExpertsByCVEId(
        long consumableVolumeExpertId,
        int pageIndex,
        int pageSize, CT ct);
}