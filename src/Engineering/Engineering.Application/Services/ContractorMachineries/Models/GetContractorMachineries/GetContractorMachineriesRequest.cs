using Engineering.Domain.Entities.ContractorMachineries.Enums;

namespace Engineering.Application.Services.ContractorMachineries.Models.GetContractorMachineries;

public record GetContractorMachineriesRequest(
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
    int PageSize
     ) : IHttpRequest;
