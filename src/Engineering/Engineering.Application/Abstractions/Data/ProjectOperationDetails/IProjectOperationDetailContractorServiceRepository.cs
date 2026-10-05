using Engineering.Application.Services.ContractorContracts.Contracts.GetsPartialProjectOperationDetailService;
using Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.GetInfoByContractorServiceId;
using Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.GetsByProjectOperationDetailId;
using Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.OpAssign.GetByPO;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetProjectContractors;
using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Application.Abstractions.Data.ProjectOperationDetails;

public interface IProjectOperationDetailContractorServiceRepository : IBaseRepository<ProjectOperationDetailContractorService>
{
    Task<ProjectOperationDetailContractorService?> GetById(
        long id,
        CT ct);

    Task<ProjectOperationDetailContractorService?> GetByDetailServiceIds(
        long projectOperationDetailId,
        long serviceInfoId,
        CT ct);

    Task<ProjectOperationDetailContractorService?> FindForDelete(
        long id,
        CT ct);

    Task<(List<GetsContractorServiceByProjectOperationDetailIdModel> Data, int RowCount)> GetsContractorServiceByProjectOperationDetailId(
        long projectOperationDetailId,
        string? serviceInfoName,
        string? serviceInfoCode,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<ProjectOperationDetailContractorService> Data, int RowCount)> GetsContractorServiceByProjectOperationId(
        long projectOperationId,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<ProjectOperationDetailContractorService> Data, int RowCount)> GetsContractorServiceForDaily(
        long projectOperationDetailId,
        CT ct);

    Task<List<ProjectOperationDetailContractorService>> GetsDetailContractorServiceByIds(
        List<long> ids,
        CT ct);

    Task<(List<ProjectOperationDetailContractorService> Data, int RowCount)> GetsByFilter(
        string? filterData,
        long? costCenterId,
        long? projectId,
        List<long>? projectOperationIds,
        List<long>? serviceInfoIds,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<long?> Data, int RowCount)> GetsProjectOperationDetailContractors(
        long? costCenterId,
        long? projectId,
        long? projectOperationId,
        long? projectOperationDetailId,
        CT ct);

    Task<(List<long?> Data, int RowCount)> GetFilteredProjectOperationDetailContractors(
        List<long> costCenterIds,
        List<long>? projectIds,
        List<long>? projectOperationIds,
        List<long>? projectOperationDetailIds,
        CT ct);

    Task<(List<long> Data, int RowCount)> GetsContractorProjectService(
        long? projectId,
        long? serviceId,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<ProjectOperationDetailContractorService> Data, int RowCount)> GetsIntegratedProjectOperationDetailService(
        List<long>? projectOperationDetailServiceIds,
        long? costCenterId,
        long? projectId,
        long? contractorId,
        List<long>? projectOperationIds,
        List<long>? serviceInfoIds,
        string? filterData,
        long? companyId,
        string[]? orderBy,
        int pageIndex,
        int pageSize, CT ct);

    Task<List<GetsPartialProjectOperationDetailServiceModel>> GetsPartialProjectOperationDetailService(
        long? costCenterId,
        long? projectId,
        long? contractorId,
        long serviceInfoId,
        List<long>? projectOperationDetailServiceIds,
        string? filterData,
        long? companyId,
        CT ct);

    Task<(List<ProjectOperationDetailContractorService> Data, int RowCount)> GetsProjectOperationDetailContractorService(
        List<long>? serviceIds,
        List<long>? projectServiceIds,
        List<long>? projectOperationDetailServiceIds,
        long? projectId,
        long? contractorId,
        List<long>? projectOperationIds,
        string? filterData,
        long? companyId,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<ProjectOperationDetailContractorService> Data, int RowCount)> GetsContractorServiceFiltered(
        List<long>? serviceIds,
        List<long>? projectOperationDetailServiceIds,
        long? projectId,
        long? contractorId,
        List<long>? projectOperationIds,
        string? filterData,
        long? companyId,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<ProjectOperationDetailContractorService> Data, int RowCount)> GetsRequestedOperationContract(
        List<long>? projectOperationDetailServiceIds,
        long? companyId,
        CT ct);

    Task<(List<ProjectOperationDetailContractorService> Data, int RowCount)> GetsRequestedServiceContract(
    long projectId,
    List<long> serviceIds,
    long contractorId,
    long? companyId,
    CT ct);

    Task<List<long>> GetFilteredContractors(
        long projectId,
        List<long>? projectOperationIds,
        CT ct);

    Task<List<GetInfoByContractorServiceIdResponse>> GetInfoByContractorServiceId(
        List<long?> ProjectOperationsDetailServiceIds,
        CT ct);

    Task<(List<GetProjectContractorsModel>? Data, int RowCount)> GetProjectContractors(
        long projectId,
        int pageIndex,
        int pageSize, CT ct);

    Task<(List<OpAssignModel> Data, int RowCount)> GetsOperationBasedAssignmentsByProjectOperationId(
        long projectOperationId,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<ProjectOperationDetailContractorService?> GetOperationBasedAssignmentForUpdate(
        long id,
        CT ct);

    Task<decimal> GetContractAllocatedConstructionQuantity(
        long projectOperationDetailId,
        long? excludedContractTypeDetailId,
        CT ct);
}
