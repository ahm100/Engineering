using Engineering.Application.Abstractions.Data;
using Engineering.Domain.Entities.ContractorServices;

namespace Engineering.Application.Services.Contractors.Commands.ContractorServices.CreateContractorService;

public class CreateContractorServiceCommandHandler : ICommandHandler<CreateContractorServiceCommand, ContractorService>
{
    private readonly ILogger<CreateContractorServiceCommandHandler> _logger;
    private readonly IContractorServicesRepository _ContractorServiceRepository;

    public CreateContractorServiceCommandHandler(ILogger<CreateContractorServiceCommandHandler> logger, IContractorServicesRepository ContractorServiceRepository)
    {
        _ContractorServiceRepository = ContractorServiceRepository;
        _logger = logger;
    }

    public async Task<Result<ContractorService?>> Handle(CreateContractorServiceCommand request, CT ct)
    {
        try
        {
            var exists = await _ContractorServiceRepository.ExistAsync(request.ServiceInfoId, request.ContractorId, ct);
            if (exists)
                return Result.Failure<ContractorService>(ContractorServicesErrors.ContractorServiceIsDuplicate);

            var entity = ContractorService.Create(request.ServiceInfoId, request.ContractorId, request.CompanyId);
            var result = await _ContractorServiceRepository.Create(entity, ct);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ContractorService>(SharedErrors.UnknownError);
        }
    }
}