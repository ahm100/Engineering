using Engineering.Domain.Entities.ContractorServices;

namespace Engineering.Application.Services.Contractors.Commands.ContractorServices.CreateContractorService;

public record CreateContractorServiceCommand(long ServiceInfoId,
                                             long ContractorId,
                                             long? CompanyId) : ICommand<ContractorService>;