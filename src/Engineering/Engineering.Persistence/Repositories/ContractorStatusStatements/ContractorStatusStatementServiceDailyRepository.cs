using Engineering.Application.Abstractions.Data.ContractorStatusStatements;
using Engineering.Application.Services.ContractorStatusStatements.Contracts.GetCSSDailyServiceUrls;
using Engineering.Domain.Entities.ContractorContracts.Enums;
using Engineering.Domain.Entities.ContractorStatusStatements;

namespace Engineering.Persistence.Repositories.ContractorStatusStatements;

public class ContractorStatusStatementServiceDailyRepository : BaseRepository<EngineeringDBContext, ContractorStatusStatementServiceDaily>, IContractorStatusStatementServiceDailyRepository
{
    public ContractorStatusStatementServiceDailyRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<List<GetCSSDailyServiceUrlsModel>> GetCSSDailyServiceUrls(
        long cssId,
        CT ct)
    {
        var query = DbSet
            .Where(oo => oo.ContractorStatusStatementService.ContractorStatusStatementDetail.ContractorStatusStatement.Id == cssId)

            .Select(item => new GetCSSDailyServiceUrlsModel()
            {
                Id = item.Id,
                ServiceInfoName = item.DailyProjectOperationService.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.ServiceInfoName,
                CreatedDate = item.DailyProjectOperationService.Created,
                Volume = item.DailyProjectOperationService.Volume,
                Urls = item.DailyProjectOperationService.DailyProjectOperation.DailyProjectOperationDocuments.Select(x => x.Url).ToList(),
            });

        return await query.ToListAsync(ct);
    }

    public async Task<decimal?> GetTotalPriceByCSSId(
        long cssId,
        CT ct)
    {
        return await DbSet.Where(x =>
            x.ContractorStatusStatementService.ContractorStatusStatementDetail
                .ContractorStatusStatement.Id == cssId &&

            !x.DailyProjectOperationService.IsDeleted &&

            x.ContractorStatusStatementService.ContractorStatusStatementDetail
                .ContractorContract.ContractorContractType == ContractorContractType.Service)

            .SumAsync(item => item.TotalPrice, ct);
    }
}
