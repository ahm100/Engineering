using Engineering.Application.Abstractions.Data;
using Engineering.Domain.Entities.ContractorServices;

namespace Engineering.Application.Services.Contractors.Commands.ContractorServices.UpdateContractorService;

public class UpdateContractorServiceCommandHandler : ICommandHandler<UpdateContractorServiceCommand, ContractorService>
{
    private readonly ILogger<UpdateContractorServiceCommandHandler> _logger;
    private readonly IContractorServicesRepository _ContractorServiceRepository;

    public UpdateContractorServiceCommandHandler(ILogger<UpdateContractorServiceCommandHandler> logger,
                                                  IContractorServicesRepository ContractorServiceRepository)
    {
        _ContractorServiceRepository = ContractorServiceRepository;
        _logger = logger;
    }

    public async Task<Result<ContractorService?>> Handle(UpdateContractorServiceCommand request, CT ct)
    {
        try
        {
            var entity = await _ContractorServiceRepository.FindById(request.Id, ct);
            if (entity is null)
                return Result.Failure<ContractorService>(ContractorServiceErrors.NotFound);

            entity!.SetContractorId(request.ContractorId);
            entity!.SetServiceInfoId(request.ServiceInfoId);
            entity!.SetActive(request.IsActive);
            entity!.SetCompanyId(request.CompanyId);

            await _ContractorServiceRepository.Update(entity!);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ContractorService>(SharedErrors.UnknownError);
        }
    }
}