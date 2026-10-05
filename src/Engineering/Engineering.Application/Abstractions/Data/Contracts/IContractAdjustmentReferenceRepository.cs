using Engineering.Application.Services.Contracts.Contracts.GetContractAdjustmentReferenceById;
using Engineering.Application.Services.Contracts.Contracts.GetContractAdjustmentReferences;
using Engineering.Domain.Entities.Contracts;

namespace Engineering.Application.Abstractions.Data.Contracts;

public interface IContractAdjustmentReferenceRepository : IBaseRepository<ContractAdjustmentReference>
{
    Task CreateContractAdjustmentReference(
        ContractAdjustmentReference entity,
        CT ct);

    Task<bool> IsContractAdjustmentReferenceCodeDuplicate(
        string code,
        long? excludedId,
        CT ct);

    Task<ContractAdjustmentReference?> GetContractAdjustmentReference(
         long id,
         CT ct);

    Task<ContractAdjustmentReference?> GetContractAdjustmentReferenceWithIndex(
        long referenceId,
        long indexId,
        CT ct);

    Task<bool> ContractAdjustmentReferenceExists(
        long id,
        CT ct);

    Task<GetContractAdjustmentReferenceByIdResponse?> GetContractAdjustmentReferenceById(
        long id,
        CT ct);

    Task<(List<GetContractAdjustmentReferencesModel> Data, int RowCount)> GetContractAdjustmentReferences(
        bool? isActive,
        string? filterData,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct);
}