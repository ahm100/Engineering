using Engineering.Application.Services.ContractorStatusStatements.Contracts;
using Engineering.Application.Services.ContractorStatusStatements.Contracts.GetIntegratedCSS.Service;
using Engineering.Application.Services.ContractorStatusStatements.Contracts.GetIntegratedCSSByProjectId;
using Engineering.Application.Services.ContractorStatusStatements.Models.GetContractorStatusStatementById;
using Engineering.Application.Services.ContractorStatusStatements.Models.GetFilteredContractorStatusStatement;
using Engineering.Domain.Entities.ContractorStatusStatements;
using Engineering.Domain.Entities.ContractorStatusStatements.Enums;

namespace Engineering.Application.Abstractions.Data.ContractorStatusStatements;

public interface IContractorStatusStatementRepository : IBaseRepository<ContractorStatusStatement>
{
    Task<ContractorStatusStatement?> GetContractorStatusStatementById(
        long id,
        CT ct);
    Task<ContractorStatusStatement?> GetCSSForDelete(
        long id,
        CT ct);

    Task<ContractorStatusStatement?> GetCSSForDiscount(
        long id,
        CT ct);

    Task<ContractorStatusStatement?> GetContractorStatusStatementHeaderById(
        long id,
        CT ct);

    Task<ContractorStatusStatement?> GetContractorStatusStatementByIdNoInclude(
        long id,
        CT ct);

    Task<ContractorStatusStatement?> GetContractorStatusStatementByIdIncludeLess(
        long id,
        CT ct);

    Task<ContractorStatusStatement?> GetContractorStatusStatementByIdFullInclude(
        long id,
        CT ct);

    Task<GetContractorStatusStatementByIdResponse?> GetModeledContractorStatusStatementById(
        long id,
        CT ct);

    Task<ContractorStatusStatement?> GetLastContractorStatusStatementById(
        long id,
        CT ct);

    Task<string> CodeCreator(
        long? companyId,
        CT ct);

    Task<(List<GetFilteredContractorStatusStatementModel> Data, int RowCount)> GetFilteredContractorStatusStatement(
        List<long>? ids,
        long? contractorId,
        List<long>? contractorIds,
        long? costCenterId,
        long? projectId,
        long? projectManagerId,
        long? contractorContractId,
        long? creatorId,
        string? contractNumber,
        string? code,
        string? managerAmount,
        string? managerDescription,
        List<CSSStatus>? statuses,
        bool isPayment,
        bool? multiPayment,
        DateTime? startDate,
        DateTime? endDate,
        bool? isPrimaryManagerConfirmed,
        bool? isFinalManagerConfirmed,
        bool? isManager,
        string? filterData,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<long> Data, int RowCount)> GetsContractorStatusStatementContractorIds(
        long? costCenterId,
        long? projectId,
        long? projectManagerId,
        long? contractorContractId,
        string? contractNumber,
        string? code,
        List<CSSStatus>? statuses,
        DateTime? startDate,
        DateTime? endDate,
        string? filterData,
        CT ct);

    Task<(List<GetsDraftableContractorStatusStatementModel> Data, int RowCount)> GetsDraftableContractorStatusStatement(
        long contractorId,
        long projectId,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<PaidContractorStatusStatementModel> Data, int RowCount)> GetsPaidContractorStatusStatement(
        long contractorId,
        long projectId,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<decimal?> PaymentConfirmationCSSAmounts(
        long contractorId,
        long projectId,
        CT ct);

    Task<List<ContractorStatusStatement>> GetContractorStatusStatementByHeaderId(
        long contractorContractId,
        CT ct);

    Task<(List<GetsIntegratedCSSModel> Data, int RowCount)> GetsIntegratedCSS(
        long? contractorId,
        List<long>? contractorIds,
        long? costCenterId,
        long? projectId,
        long? contractorContractId,
        List<CSSStatus>? statuses,
        bool isPrimaryManager,
        bool isFinalManager,
        string? filterData,
        CT ct);

    Task<(List<GetIntegratedCSSByProjectIdModel> Data, int RowCount)> GetsIntegratedCSSByProjectId(
        long? contractorId,
        List<long>? contractorIds,
        long? costCenterId,
        long? projectId,
        long? contractorContractId,
        List<CSSStatus>? statuses,
        bool isPrimaryManager,
        bool isFinalManager,
        string? filterData,
        CT ct);

    Task<List<long>?> GetCSSCreators(
        CT ct);
}
