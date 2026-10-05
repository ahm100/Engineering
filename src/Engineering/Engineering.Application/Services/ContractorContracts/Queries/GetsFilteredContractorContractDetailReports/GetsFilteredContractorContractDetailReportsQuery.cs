using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Application.Services.ContractorContracts.Queries.GetsFilteredContractorContractDetailReports;

public record GetsFilteredContractorContractDetailReportsQuery(
    List<long>? Ids,
    long? ContractorContractId,
    long? ContractorId,
    DateTime? FromDate,
    DateTime? ToDate,
    long? CompanyId,
    string? FilterData,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<ContractorContractDetail>>>;
