using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Application.Services.ContractorContracts.Queries.GetsFilteredContractorContractReports;

public record GetsFilteredContractorContractReportsQuery(
    List<long>? Ids,
    long? ContractorId,
    long? CostCenterId,
    List<long>? ProjectIds,
    List<long>? ContractorContractIds,
    DateTime? FromDate,
    DateTime? ToDate,
    long CompanyId,
    string? FilterData,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<ContractorContract>>>;
