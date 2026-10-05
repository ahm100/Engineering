using Engineering.Application.Services.ContractorStatusStatements.Contracts.GetCSSDailyServiceUrls;
using Engineering.Domain.Entities.ContractorStatusStatements;

namespace Engineering.Application.Abstractions.Data.ContractorStatusStatements;

public interface IContractorStatusStatementServiceDailyRepository : IBaseRepository<ContractorStatusStatementServiceDaily>
{
    Task<List<GetCSSDailyServiceUrlsModel>> GetCSSDailyServiceUrls(
        long cSSId,
        CT ct);

    Task<decimal?> GetTotalPriceByCSSId(
        long cSSId,
        CT ct);
}
