using Engineering.Application.Services.BillOfLadings.Contracts.GetBillOfLadingById;
using Engineering.Application.Services.BillOfLadings.Contracts.GetsActiveBillOfLading;
using Engineering.Application.Services.BillOfLadings.Contracts.GetsBillOfLadingExcelExporter;
using Engineering.Application.Services.BillOfLadings.Contracts.GetsFilteredBillOfLading;
using Engineering.Domain.Entities.BillOfLadings;

namespace Engineering.Application.Abstractions.Data.BillOfLadings;

public interface IBillOfLadingRepository : IBaseRepository<BillOfLading>
{
    Task<BillOfLading?> GetBillOfLadingById(
        long id, CT ct);

    Task<GetBillOfLadingByIdResponse?> GetBillOfLadingByIdForResponse(
        long id, CT ct);

    Task<List<BillOfLading>> GetBillOfLadings(
        List<long> ids, CT ct);

    Task<BillOfLading?> GetBillOfLadingByName(
        string name,
        long? companyId,
        CT ct);

    Task<BillOfLading?> GetBillOfLadingByCode(
        string code,
        long? companyId,
        CT ct);

    Task<bool> GetsBillOfLadingByNamesOrCodes(
        List<string> names,
        List<string> codes,
        long? companyId, CT ct);

    Task<string> CodeCreator(
        long? companyId, CT ct);

    Task<(List<GetsFilteredBillOfLadingResponseModel> Data, int RowCount)> GetsFilteredBillOfLading(
            List<long>? ids,
            string? filterData,
            string? code,
            string? name,
            bool? isActive,
            string[]? orderBy,
            long? companyId,
            int pageIndex,
            int pageSize, CT ct);

    Task<(List<GetsBillOfLadingExcelExporterModel> Data, int RowCount)> GetsFilteredBillOfLadingForExcel(
        List<long>? ids,
        string? filterData,
        string? code,
        string? name,
        bool? isActive,
        string[]? orderBy,
        long? companyId,
        int pageIndex,
        int pageSize, CT ct);

    Task<(List<GetsActiveBillOfLadingResponseModel> Data, int RowCount)> GetsActiveBillOfLading(
            string? filterData,
            string? code,
            string? name,
            long? companyId,
            int pageIndex,
            int pageSize, CT ct);
}