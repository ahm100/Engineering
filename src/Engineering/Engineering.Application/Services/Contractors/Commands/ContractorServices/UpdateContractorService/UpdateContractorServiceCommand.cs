using Engineering.Domain.Entities.ContractorServices;

namespace Engineering.Application.Services.Contractors.Commands.ContractorServices.UpdateContractorService;

public record UpdateContractorServiceCommand(long Id,
                                             long ServiceInfoId,
                                             long ContractorId,
                                             bool IsActive,
                                             long? CompanyId) : ICommand<ContractorService>;