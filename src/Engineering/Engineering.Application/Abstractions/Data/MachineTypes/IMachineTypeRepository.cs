using Engineering.Application.Services.ShippingCosts.Contracts.ShippingCostImportExcelHelper;
using Engineering.Domain.Entities.MachineTypes;

namespace Engineering.Application.Abstractions.Data.MachineTypes;

public interface IMachineTypeRepository : IBaseRepository<MachineType>
{
    Task<MachineType?> FindByName(
        string name,
        long? companyId,
        CT ct);

    Task<MachineType?> FindByCode(
        string code,
        long? companyId,
        CT ct);

    Task<MachineType?> GetById(
        long id,
        CT ct);

    Task<MachineType?> FindForDelete(
        long id,
        CT ct);

    Task<bool> FindDuplicateMachineType(
        List<string> names,
        List<string> codes,
        List<int> fromWeights,
        List<int> untilWeights,
        List<int> cabinTypeCodes,
        long? companyId,
        CT ct);

    Task<bool> FindMachineTypeByCodes(
        List<string> codes,
        long? companyId,
        CT ct);

    Task<MachineType?> GetDuplicateMachineType(
        string name,
        int fromWeight,
        int untilWeight,
        int cabinTypeCode,
        long? companyId,
        CT ct);

    Task<string> CodeCreator(
        long? companyId,
        CT ct);

    Task<List<MachineType>> GetsMachineTypeByIds(
        List<long> ids,
        CT ct);

    Task<List<MachineType>> GetsMachineTypeByCodes(
        List<string> codes,
        long companyId,
        CT ct);

    Task<(List<MachineType> Data, int RowCount)> GetMachineTypes(
        List<long>? ids,
        string? filterData,
        bool? isActive,
        long? companyId,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<MachineType> Data, int RowCount)> GetsActiveMachineType(
        string? filterData,
        long? companyId,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<MachineTypeDataModel> Data, int RowCount)> GetsActiveMachineTypeData(
        string? filterData,
        long? companyId,
        int pageIndex,
        int pageSize,
        CT ct);
}