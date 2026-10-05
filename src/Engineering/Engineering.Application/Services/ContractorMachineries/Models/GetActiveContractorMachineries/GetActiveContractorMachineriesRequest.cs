using Engineering.Domain.Entities.ContractorMachineries.Enums;

namespace Engineering.Application.Services.ContractorMachineries.Models.GetActiveContractorMachineries;

public record GetActiveContractorMachineriesRequest(
    List<long>? MachineryIds,
    List<long>? ContractorIds,
    ContractorMachineryUnit? Unit,
    DateTime? FromDate,
    DateTime? ToDate,
    string? FilterData,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
