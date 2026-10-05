using Engineering.Application.Abstractions.Data.ContractorContracts;
using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Application.Services.ContractorContracts.Commands.ContractorContracts.SetCCTotalAmount;

public class SetCCTotalAmountCommandHandler : ICommandHandler<SetCCTotalAmountCommand, ContractorContract>
{
    private readonly ILogger<SetCCTotalAmountCommandHandler> _logger;
    private readonly IContractorContractRepository _repository;

    public SetCCTotalAmountCommandHandler(
        ILogger<SetCCTotalAmountCommandHandler> logger,
        IContractorContractRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ContractorContract?>> Handle(SetCCTotalAmountCommand request, CT ct)
    {
        try
        {
            var entity = request.Entity;

            entity.SetTotalAmount(null);

            await _repository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ContractorContract>(SharedErrors.UnknownError);
        }
    }
}
