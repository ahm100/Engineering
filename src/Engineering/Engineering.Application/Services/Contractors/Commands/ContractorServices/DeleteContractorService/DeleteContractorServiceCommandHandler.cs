using Engineering.Application.Abstractions.Data;
using Engineering.Domain.Entities.ContractorServices;

namespace Engineering.Application.Services.Contractors.Commands.ContractorServices.DeleteContractorService;

public class DeleteContractorServiceCommandHandler : ICommandHandler<DeleteContractorServiceCommand, ContractorService>
{
    private readonly IContractorServicesRepository _ContractorServiceRepository;
    private readonly ILogger<DeleteContractorServiceCommandHandler> _logger;

    public DeleteContractorServiceCommandHandler(ILogger<DeleteContractorServiceCommandHandler> logger,
                                                  IContractorServicesRepository ContractorServiceRepository)
    {
        _ContractorServiceRepository = ContractorServiceRepository;
        _logger = logger;
    }

    public async Task<Result<ContractorService?>> Handle(DeleteContractorServiceCommand request, CT ct)
    {
        try
        {
            var entity = await _ContractorServiceRepository.FindById(request.Id, ct);
            if (entity is null)
                return Result.Failure<ContractorService>(ContractorServicesErrors.ContractorServiceIdNotFound);
            if (entity.IsDeleted)
                return Result.Failure<ContractorService>(ContractorServicesErrors.IsDeleted);

            entity.SetIsDeleted();
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ContractorService>(SharedErrors.UnknownError);
        }
    }
}
