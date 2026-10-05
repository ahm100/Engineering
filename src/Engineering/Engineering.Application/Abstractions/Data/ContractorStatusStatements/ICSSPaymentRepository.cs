using Engineering.Application.Services.ContractorStatusStatements.Contracts.GetCSSPayments;
using Engineering.Application.Services.ContractorStatusStatements.Contracts.GetMonthlyPayment;
using Engineering.Domain.Entities.ContractorStatusStatements;

namespace Engineering.Application.Abstractions.Data.ContractorStatusStatements;

public interface IContractorStatusStatementPaymentRepository : IBaseRepository<ContractorStatusStatementPayment>
{
    Task<(List<GetCSSPaymentsModel> Data, int RowCount)> GetCSSPayments(
        long id,
        int pageIndex,
        int pageSize, CT ct);

    Task<decimal> GetTodaysPayment(
    CT ct);

    Task<List<GetMonthlyPaymentModel>> GetMonthlyPayment(
    int monthCount,
    CT ct);
}