using Engineering.Domain.Entities.ContractorMachineries.Enums;
using ContractorMachinery = Engineering.Domain.Entities.ContractorMachineries.ContractorMachinery;

namespace Engineering.Application.Services.ContractorMachineries.Queries.GetContractorMachineries;

public record GetContractorMachineriesQuery(
    List<long>? Ids,
    List<long>? MachineryIds,
    List<long>? ContractorIds,
    ContractorMachineryUnit? Unit,
    DateTime? FromDate,
    DateTime? ToDate,
    string? FilterData,
    bool? IsActive,
    long? CompanyId,
    string[]? OrderBy,
    int PageIndex,
    int PageSize) : IQuery<DataResult<List<ContractorMachinery>>>;