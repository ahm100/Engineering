using Engineering.Domain.Entities.ContractorMachineries.Enums;

namespace Engineering.Application.Services.ContractorMachineries.Models.UpdateContractorMachinery;

public record UpdateContractorMachineryRequest(
    long Id,
    long MachineryId,
    long ContractorId,
    decimal MachineryPrice,
    long CurrencyId,
    ContractorMachineryUnit Unit,
    string? NumberPlates,
    string? MachineryIdentifier,
    bool IsActive,
    string? Description
     ) : IHttpRequest;
