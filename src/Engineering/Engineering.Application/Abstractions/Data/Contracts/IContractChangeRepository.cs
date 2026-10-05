using Engineering.Application.Services.Contracts;
using Engineering.Application.Services.Contracts.Contracts.GetContractChangeAvailableItems;
using Engineering.Application.Services.Contracts.Contracts.GetContractChangeById;
using Engineering.Application.Services.Contracts.Contracts.GetContractChanges;
using Engineering.Application.Services.Contracts.Contracts.CreateContractChange;
using Engineering.Application.Services.Contracts.Models;
using Engineering.Application.Services.Contracts.Models.ContractChanges;
using Engineering.Domain.Entities.Contracts;
using Engineering.Domain.Entities.Contracts.Enums;
using Engineering.Application.Services.Contracts.Contracts.ContractChanges;

namespace Engineering.Application.Abstractions.Data.Contracts;

public interface IContractChangeRepository : IBaseRepository<ContractChange>
{
    Task<bool> HasActiveContractChanges(
        long contractId,
        long companyId,
        CT ct);

    Task<bool> IsContractChangeNumberDuplicate(
        long contractId,
        string number,
        long? excludedContractChangeId,
        long companyId,
        CT ct);

    Task<List<ContractChangeSourceContextModel>> GetContractChangeSourceContexts(
        long projectId,
        long contractPartyId,
        IReadOnlyCollection<long> consumableVolumeProductIds,
        IReadOnlyCollection<long> projectOperationDetailIds,
        IReadOnlyCollection<long> contractorServiceIds,
        long companyId,
        CT ct);

    Task<GetContractChangeByIdResponse?> GetContractChangeById(
        long contractId,
        long id,
        long companyId,
        CT ct);

    Task<(List<GetContractChangesModel> Data, int RowCount)> GetContractChanges(
        long contractId,
        ContractChangeType? type,
        DateTime? dateFrom,
        DateTime? dateTo,
        string? filterData,
        long companyId,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<GetContractChangeAvailableItemsResponse?> GetContractChangeAvailableItems(
        long contractId,
        long contractTypeId,
        long? projectOperationId,
        string? filterData,
        long companyId,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<ContractChangeMutationContextModel> GetContractChangeMutationContext(
        long contractId,
        long? contractChangeId,
        long companyId,
        CT ct);

    Task<List<ContractChangeSourceHistoryContextModel>> GetContractChangeSourceHistoryContexts(
        long contractId,
        long? excludedContractChangeId,
        IReadOnlyCollection<long> contractTypeIds,
        IReadOnlyCollection<long> projectOperationDetailIds,
        long companyId,
        CT ct);

    Task<List<ContractChangeOmittedItemContextModel>> GetContractChangeOmittedItemContexts(
        long contractId,
        long currentContractChangeId,
        long companyId,
        CT ct);

    Task<List<ContractChangeTypeContextModel>> GetContractChangeTypeContexts(
        long contractId,
        IReadOnlyCollection<long> contractTypeIds,
        long companyId,
        CT ct);

    Task<List<ContractChangeSourceContextModel>> GetContractChangeSourceContexts(
        long projectId,
        long contractPartyId,
        IReadOnlyCollection<(ContractTypeKind Kind, long SourceId)> requestedSources,
        long companyId,
        CT ct);

    ContractChangeRequestProjection GetContractChangeRequestProjection(
        IReadOnlyCollection<ContractChangeItemRequest> requests);

    decimal CalculateContractChangeAmount(
        IReadOnlyCollection<ContractChangeItemTerms> itemTerms);

    ContractChangeCapacityValidationStatus ValidateContractChangeCapacityTransitions(
        IReadOnlyCollection<(ContractTypeKind Kind, long SourceId, decimal Delta)> transitions,
        IReadOnlyCollection<ContractChangeSourceContextModel> sourceContexts);

    ContractChangeDetailResolutionContextModel? FindContractChangeDetailContext(
        IReadOnlyCollection<ContractChangeDetailResolutionContextModel> contexts,
        long detailId);

    ContractChangeTypeContextModel? FindContractChangeTypeContext(
        IReadOnlyCollection<ContractChangeTypeContextModel> contexts,
        long contractTypeId);

    ContractChangeSourceHistoryContextModel? FindContractChangeSourceHistoryContext(
        IReadOnlyCollection<ContractChangeSourceHistoryContextModel> contexts,
        long contractTypeId,
        long sourceId);

    ContractChangeSourceContextModel? FindContractChangeSourceContext(
        IReadOnlyCollection<ContractChangeSourceContextModel> contexts,
        ContractTypeKind kind,
        long sourceId);

    IReadOnlyCollection<(ContractTypeKind Kind, long SourceId, decimal Delta)> BuildOmittedCapacityTransitions(
        IReadOnlyCollection<ContractChangeOmittedItemContextModel> omittedContexts,
        IReadOnlyCollection<long> replacementContractTypeDetailIds,
        IReadOnlyCollection<(long ContractTypeId, long SourceId)> replacementSourceKeys);

    IReadOnlyCollection<(ContractTypeKind Kind, long SourceId)> GetCapacityTransitionSourceKeys(
        IReadOnlyCollection<(ContractTypeKind Kind, long SourceId, decimal Delta)> transitions);
}