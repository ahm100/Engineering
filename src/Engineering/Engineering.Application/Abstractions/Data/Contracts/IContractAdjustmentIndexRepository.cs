using Engineering.Application.Services.Contracts.Contracts.GetContractAdjustmentIndexById;
using Engineering.Application.Services.Contracts.Contracts.GetContractAdjustmentIndexes;
using Engineering.Domain.Entities.Contracts;

namespace Engineering.Application.Abstractions.Data.Contracts;

public interface IContractAdjustmentIndexRepository : IBaseRepository<ContractAdjustmentIndex>
{
    Task<bool> IsContractAdjustmentIndexCodeDuplicate(
        long referenceId,
        string code,
        long? excludedId,
        CT ct);

    Task<bool> HasActiveIndex(
        long referenceId,
        long indexId,
        CT ct);

    Task<GetContractAdjustmentIndexByIdResponse?> GetContractAdjustmentIndexById(
        long referenceId,
        long id,
        CT ct);

    Task<(List<GetContractAdjustmentIndexesModel> Data, int RowCount)> GetContractAdjustmentIndexes(
        long referenceId,
        bool? isActive,
        string? filterData,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct);
}