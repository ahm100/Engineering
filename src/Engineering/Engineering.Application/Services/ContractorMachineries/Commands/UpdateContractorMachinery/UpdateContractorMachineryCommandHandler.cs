using Engineering.Application.Abstractions.Data.ContractorMachineries;
using ContractorMachinery = Engineering.Domain.Entities.ContractorMachineries.ContractorMachinery;

namespace Engineering.Application.Services.ContractorMachineries.Commands.UpdateContractorMachinery;

public class UpdateContractorMachineryCommandHandler : ICommandHandler<UpdateContractorMachineryCommand, ContractorMachinery>
{
    private readonly ILogger<UpdateContractorMachineryCommand> _logger;
    private readonly IContractorMachineryRepository _repository;

    public UpdateContractorMachineryCommandHandler(ILogger<UpdateContractorMachineryCommand> logger, IContractorMachineryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ContractorMachinery?>> Handle(UpdateContractorMachineryCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.GetById(request.Id, ct);
            if (entity is null)
                return Result.Failure<ContractorMachinery>(ContractorMachineryErrors.ContractorMachineryNotFoundWithId);

            entity.SetMachineryIdentifier(request.MachineryIdentifier);
            entity.SetNumberPlates(request.NumberPlates);
            entity.SetContractorId(request.ContractorId);
            entity.SetMachinery(request.Machinery);
            entity.SetDescription(request.Description);
            entity.SetCompanyId(request.CompanyId);
            entity.SetMachineryPrice(request.MachineryPrice);
            entity.SetCurrencyId(request.CurrencyId);
            entity.SetContractorMachineryUnit(request.Unit);
            if (request.IsActive != entity.IsActive)
            {
                if (request.IsActive == true)
                    entity.SetActive();
                else
                    entity.SetInActive();
            }

            await _repository.Update(entity);

            return entity;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<ContractorMachinery>(SharedErrors.UnknownError);
        }
    }
}