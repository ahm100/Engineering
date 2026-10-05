using Engineering.Application.Services.Contracts.Contracts.GetContractById;
using Engineering.Application.Services.Contracts.Contracts.GetContractForProcesVerbal;
using Engineering.Application.Services.Contracts.Contracts.GetContractRegistrationById;
using Engineering.Application.Services.Contracts.Contracts.GetContractRegistrationGrid;
using Engineering.Application.Services.Contracts.Contracts.GetContractsByStatus;
using Engineering.Application.Services.Contracts.Contracts.GetContractStructure;
using Engineering.Application.Services.Contracts.Contracts.GetFilteredContracts;
using Engineering.Application.Services.Contracts.Contracts.UpdateContractRegistration;
using Engineering.Application.Services.Contracts.Contracts.UpdateContractStructure;
using Engineering.Application.Services.Contracts.Models;
using Engineering.Domain.Entities.Contracts;
using Engineering.Domain.Entities.Contracts.Enums;

using ContractEntity = Engineering.Domain.Entities.Contracts.Contract;
using ContractTypeEntity = Engineering.Domain.Entities.Contracts.ContractType;

namespace Engineering.Application.Abstractions.Data.Contracts;

public interface IContractRepository : IBaseRepository<ContractEntity>
{
    Task<ContractEntity?> GetContract(
        long id,
        long companyId,
        CT ct);

    Task<ContractEntity?> GetContractWithDocuments(
        long id,
        long companyId,
        CT ct);

    Task<ContractEntity?> GetContractWithTypes(
        long id,
        long companyId,
        CT ct);

    Task<ContractEntity?> GetContractWithGuarantees(
        long id,
        long companyId,
        CT ct);

    Task<ContractEntity?> GetContractForDelete(
        long id,
        long companyId,
        CT ct);

    Task<ContractEntity?> GetContractById(
        long id,
        CT ct);

    Task<GetContractByIdResponse?> GetContractByIdForResponse(
        long id,
        long companyId,
        CT ct);

    Task<GetContractStructureResponse?> GetContractStructure(
        long id,
        long companyId,
        CT ct);

    Task CreateContract(
        ContractEntity entity,
        CT ct);

    Task<bool> IsContractNumberDuplicate(
        long contractNumber,
        long? excludedContractId,
        CT ct);

    Task EnsureContractNumberSequenceIsAfter(
        long contractNumber,
        CT ct);

    Task<ContractEntity?> GetContractForRegistrationMutation(
        long id,
        long companyId,
        CT ct);

    Task<GetContractRegistrationByIdResponse?> GetContractRegistrationById(
        long id,
        long companyId,
        CT ct);

    Task<(List<GetContractRegistrationGridModel> Data, int RowCount)>
        GetContractRegistrationGrid(
            long? contractNumber,
            long? contractNumberFrom,
            long? contractNumberTo,
            ContractStatus? status,
            string? filterData,
            long companyId,
            string[]? orderBy,
            int pageIndex,
            int pageSize,
            CT ct);

    Task<(List<GetFilteredContractsModel> Data, int RowCount)>
        GetFilteredContracts(
            long? contractNumber,
            string? faTitle,
            string? enTitle,
            long? projectId,
            long? contractPartyId,
            ContractStatus? status,
            ContractDurationUnit? durationUnit,
            DateTime? startDateFrom,
            DateTime? startDateTo,
            DateTime? endDateFrom,
            DateTime? endDateTo,
            string? filterData,
            long companyId,
            string[]? orderBy,
            int pageIndex,
            int pageSize,
            CT ct);

    Task<(List<GetContractsByStatusModel> Data, int RowCount)>
        GetContractsByStatus(
            ContractStatus status,
            long companyId,
            string[]? orderBy,
            int pageIndex,
            int pageSize,
            CT ct);

    Task<ContractEntity?> GetContractWithTypesAndDetails(
        long id,
        long companyId,
        CT ct);

    Task<ContractEntity?> GetContractWithTypesDetailsAndFinancialInformation(
        long id,
        long companyId,
        CT ct);

    Task<ContractEntity?> GetContractWithTypesAndFinancialInformation(
        long id,
        long companyId,
        CT ct);

    Task<ContractEntity?> GetContractWithChangesForMutation(
        long id,
        long companyId,
        CT ct);

    Task<List<GetContractForProcesVerbalResponse>> GetContractForProcesVerbal(
        long? projectId,
        CT ct);

    ContractStructureMutationProjection GetContractStructureMutationProjection(
        ContractEntity contract,
        UpdateContractStructureRequest request);

    ContractRegistrationMutationProjection GetContractRegistrationMutationProjection(
        ContractEntity contract,
        UpdateContractRegistrationRequest request);

    bool HasActiveContractTypes(ContractEntity contract);

    bool HasContractTypeKind(
        ContractEntity contract,
        ContractTypeKind kind,
        long? excludedContractTypeId = null);

    bool RequiresContractCeilingAmount(ContractEntity contract);

    bool HasPricingMethodRequiringCeiling(
        ContractEntity contract,
        PricingMethod pricingMethod,
        long? excludedContractTypeId = null);

    ContractTypeEntity? GetContractTypeForMutation(
        ContractEntity contract,
        long contractTypeId);

    bool HasActiveContractTypeDetails(ContractTypeEntity contractType);

    decimal GetActiveFinancialChangeAmount(ContractEntity contract);

    bool AreRegistrationDocumentsEqual(
        ContractEntity contract,
        IReadOnlyCollection<string> urls);
}