using Engineering.Domain.Entities.ContractorServices;

namespace Engineering.Application.Services.Contractors.Commands.ContractorServices.DeleteContractorService;

public record DeleteContractorServiceCommand(long Id) : ICommand<ContractorService>;

