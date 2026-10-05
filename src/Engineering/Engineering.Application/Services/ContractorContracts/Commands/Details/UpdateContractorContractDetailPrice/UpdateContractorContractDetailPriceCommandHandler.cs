using Engineering.Application.Abstractions.Data.ContractorContracts;
using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Application.Services.ContractorContracts.Commands.Details.UpdateContractorContractDetailPrice;

public class UpdateContractorContractDetailPriceCommandHandler : ICommandHandler<UpdateContractorContractDetailPriceCommand, ContractorContractDetailPrice>
{
    private readonly ILogger<UpdateContractorContractDetailPriceCommandHandler> _logger;
    private readonly IContractorContractDetailPriceRepository _repository;

    public UpdateContractorContractDetailPriceCommandHandler(
        ILogger<UpdateContractorContractDetailPriceCommandHandler> logger,
        IContractorContractDetailPriceRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ContractorContractDetailPrice?>> Handle(UpdateContractorContractDetailPriceCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.Id, ct);
            if (entity is null)
                return Result.Failure<ContractorContractDetailPrice>(ContractorContractDetailPriceErrors.InvalidContractorContractDetailPriceId);
            if (entity.ContractorContractDetailPriceHistories?.Any() != true)
                entity.AddHistory();
            entity.SetPrice(request.Price);
            entity.SetCurrencyId(request.CurrencyId);
            entity.SetStartDate(request.StartDate);
            entity.SetEndDate(request.EndDate);

            if (request.IsActive != entity.IsActive)
            {
                if (request.IsActive == true)
                    entity.SetActive();
                else
                    entity.SetDeactivate();
            }

            entity.AddHistory();
            await _repository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ContractorContractDetailPrice>(SharedErrors.UnknownError);
        }
    }
}
