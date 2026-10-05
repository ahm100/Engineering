using Engineering.Domain.Entities.ContractorMachineries.Enums;

namespace Engineering.Application.Services.ContractorMachineries.Models.CreateContractorMachinery;

public record CreateContractorMachineryRequest(
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
