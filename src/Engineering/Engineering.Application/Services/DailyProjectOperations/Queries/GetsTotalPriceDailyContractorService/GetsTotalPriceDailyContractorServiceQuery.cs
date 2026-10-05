
namespace Engineering.Application.Services.DailyProjectOperations.Queries.GetsTotalPriceDailyContractorService;

public record GetsTotalPriceDailyContractorServiceQuery(
     long ProjectId,
     long ContractorId,
     DateTime? StartDate,
     DateTime? EndDate
    ) : IQuery<decimal>;
