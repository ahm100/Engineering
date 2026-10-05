using Engineering.Application.Abstractions.Data.ContractorContracts;
using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Application.Services.ContractorContracts.Commands.Details.CreateContractorContractDetailCostOver;

public class CreateContractorContractDetailCostOverCommandHandler : ICommandHandler<CreateContractorContractDetailCostOverCommand, ContractorContractDetailCostOver>
{
    private readonly ILogger<CreateContractorContractDetailCostOverCommandHandler> _logger;
    private readonly IContractorContractDetailCostOverRepository _repository;

    public CreateContractorContractDetailCostOverCommandHandler(
        ILogger<CreateContractorContractDetailCostOverCommandHandler> logger,
        IContractorContractDetailCostOverRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ContractorContractDetailCostOver?>> Handle(CreateContractorContractDetailCostOverCommand request, CT ct)
    {
        try
        {
            var result = ContractorContractDetailCostOver.Create(
                request.ContractorContract,
                request.ContractorContractDetail,
                request.CostOver,
                request.ContractorId,
                request.Percentage,
                request.Description
                );

            await _repository.Create(result, ct);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ContractorContractDetailCostOver>(SharedErrors.UnknownError);
        }
    }
}
