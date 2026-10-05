using Engineering.Application.Services.EmployerStatusStatements.Models.GetEmployerStatusStatementLetterheadExcel;
using Engineering.Domain.Entities.EmployerStatusStatements;
using Engineering.Domain.Entities.EmployerStatusStatements.Enums;

namespace Engineering.Application.Abstractions.Data.EmployerStatusStatements;

public interface IEmployerStatusStatementRepository : IBaseRepository<EmployerStatusStatement>
{
    Task<EmployerStatusStatement?> GetEmployerStatusStatementById(
        long Id,
        CT ct);

    Task<EmployerStatusStatement?> GetEmployerStatusStatementByIdForDocs(
        long Id,
        CT ct);

    Task<EmployerStatusStatement?> ChangeEmployerStatusStatementStatus(
        long Id,
        CT ct);

    Task<EmployerStatusStatement?> GetEmployerStatusStatementExcelExporter(
        long Id,
        CT ct);

    Task<GetEmployerStatusStatementLetterheadExcelModel?> GetEmployerStatusStatementLetterheadExcel(
        long Id,
        CT ct);

    Task<string> CodeCreator(long? companyId, CT ct);
    Task<EmployerStatusStatement?> GetLast(
        long? employerId,
        long costCenterId,
        long projectId,
        string? contractCode,
        CT ct);

    Task<(List<EmployerStatusStatement> Data, int RowCount)> GetsFilteredEmployerStatusStatement(
        long employerId,
        long costCenterId,
        long projectId,
        long employerContractId,
        string? statusStatementCode,
        List<long>? projectOperationIds,
        DateTime? startDate,
        DateTime? endDate,
        EmployerStatusStatementStatus? status,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<EmployerStatusStatement> Data, int RowCount)> GetsEmployerStatusStatementExcelExporter(
        List<long>? ids,
        long costCenterId,
        long projectId,
        long? employerId,
        long? employerContractId,
        string? statusStatementCode,
        List<long>? projectOperationIds,
        DateTime? startDate,
        DateTime? endDate,
        EmployerStatusStatementStatus? status,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<long> Data, int RowCount)> GetsFilteredEmployerRequester(CT ct);
}
