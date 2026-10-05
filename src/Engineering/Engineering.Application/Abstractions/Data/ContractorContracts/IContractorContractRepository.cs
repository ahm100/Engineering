using Engineering.Application.Services.ContractorContracts.Contracts.GetCCByHeaderId;
using Engineering.Application.Services.ContractorContracts.Contracts.GetCCThirdParties;
using Engineering.Application.Services.ContractorContracts.Contracts.GetContractorContractsDate;
using Engineering.Application.Services.ContractorContracts.Contracts.GetContractsByProjectId;
using Engineering.Application.Services.ContractorContracts.Contracts.GetCostCentersMostPaidCC;
using Engineering.Application.Services.ContractorContracts.Contracts.GetCostCentersMostRecentCC;
using Engineering.Application.Services.ContractorContracts.Contracts.GetDraftedFixCCs;
using Engineering.Application.Services.ContractorContracts.Contracts.GetDraftedServiceCCs;
using Engineering.Application.Services.ContractorContracts.Contracts.GetFltrProjectContractors;
using Engineering.Domain.Entities.ContractorContracts;
using Engineering.Domain.Entities.ContractorContracts.Enums;

namespace Engineering.Application.Abstractions.Data.ContractorContracts;

public interface IContractorContractRepository : IBaseRepository<ContractorContract>
{
    Task<ContractorContract?> GetContractorContractById(
        long id,
        long companyId,
        CT ct);

    Task<List<GetCCByHeaderIdModel>?> GetCCByHeaderId(
        long id,
        ContractorContractType type,
        long companyId,
        CT ct);

    Task<ContractorContract?> GetFixContractorContractByIdIncludeless(
        long id,
        long companyId,
        CT ct);

    Task<ContractorContract?> GetServiceContractorContractByIdIncludeless(
        long id,
        long companyId,
        CT ct);

    Task<ContractorContract?> GetContractorContractForDelete(
        long id,
        long companyId,
        CT ct);

    Task<List<ContractorContractsDateModel>?> GetContractorContractsDate(
        long projectId,
        long contractorId,
        long companyId,
        CT ct);

    Task<List<GetDraftedFixCCsModel>?> GetDraftedFixCCs(
        long projectId,
        long contractorId,
        DateTime? startDate,
        DateTime? endDate,
        long companyId,
        CT ct);

    Task<List<GetDraftedServiceCCsModel>?> GetDraftedServiceCCs(
        long projectId,
        long contractorId,
        DateTime? startDate,
        DateTime? endDate,
        long companyId,
        CT ct);

    Task<ContractorContract?> GetContractorContractByIdIncludeless(
        long id,
        long companyId,
        CT ct);

    Task<long?> GetContractorContractCurrency(
        long projectOperationDetailId,
        long contractorId,
        long companyId,
        CT ct);

    Task<(List<ContractorContract> Data, int RowCount)> GetFilteredAsync(
        long? contractorId,
        long? costCenterId,
        List<long>? projectIds,
        List<long>? projectOperationIds,
        List<long>? employerContracts,
        DateTime? fromDate,
        DateTime? toDate,
        ContractorContractStatus? status,
        ContractorContractType? contractorContractTypeId,
        string? filterData,
        string[]? orderBy,
        long companyId,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<List<ContractorContract>> GetsContractorByContractorContractType(
        ContractorContractType? contractorContractTypeId,
        long companyId,
        CT ct);

    Task<(List<ContractorContract> Data, int RowCount)> GetFilteredByContractorIdAsync(
        long contrctorId,
        long? costCenterId,
        List<long>? projects,
        List<long>? contracts,
        DateTime? fromDate,
        DateTime? toDate,
        string? filterData,
        long companyId,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<ContractorContract> Data, int RowCount)> GetsContractorContractByContractorId(
        long contrctorId,
        long? costCenterId,
        List<long>? projectIds,
        string? filterData,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        long companyId,
        CT ct);

    Task<(List<long> Data, int RowCount)> GetsFilteredContractors(
        List<long>? contractorContractIds,
        long companyId,
        CT ct);

    Task<(List<ContractorContract> Data, int RowCount)> GetsFilteredContractorContractReports(
       List<long>? ids,
       long? contractorId,
       long? costCenterId,
       List<long>? projectIds,
       List<long>? contractorContractIds,
       DateTime? fromDate,
       DateTime? toDate,
       long companyId,
       string? filterData,
       string[]? orderBy,
       int pageIndex,
       int pageSize,
       CT ct);

    Task<(List<GetContractsByProjectIdModel> Data, int RowCount)> GetContractsByProjectId(
        long projectId,
        int pageIndex,
        int pageSize,
        long companyId,
        CT ct);

    Task<List<GetFltrProjectContractorsModel>> GetFltrProjectContractors(
        List<long>? costCenterIds,
        List<long>? projectIds,
        long companyId,
        CT ct);

    Task<(List<GetCCThirdPartiesModel> Data, int RowCount)> GetCCThirdParties(
        long projectId,
        List<long>? contractorIds,
        int pageIndex,
        int pageSize,
        long companyId,
        CT ct);

    Task<List<GetCostCentersMostPaidCCModel>> GetCostCentersMostPaidCC(
    long companyId,
    CT ct);

    Task<List<GetCostCentersMostRecentCCModel>> GetCostCentersMostRecentCC(
    long companyId,
    CT ct);
}
