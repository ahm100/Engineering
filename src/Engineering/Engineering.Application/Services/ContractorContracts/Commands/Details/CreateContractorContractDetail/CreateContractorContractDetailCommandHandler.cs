using Engineering.Application.Abstractions.Data.ContractorContracts;
using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Application.Services.ContractorContracts.Commands.Details.CreateContractorContractDetail;

public class CreateContractorContractDetailCommandHandler :
    ICommandHandler<CreateContractorContractDetailCommand, ContractorContractDetail>
{
    private readonly ILogger<CreateContractorContractDetailCommandHandler> _logger;
    private readonly IContractorContractDetailRepository _repository;

    public CreateContractorContractDetailCommandHandler(
        ILogger<CreateContractorContractDetailCommandHandler> logger,
        IContractorContractDetailRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ContractorContractDetail?>> Handle(
        CreateContractorContractDetailCommand request, CT ct)
    {
        try
        {
            var result = new ContractorContractDetail(
                request.ContractorContract,
                request.ProjectOperation,
                request.StartDate,
                request.EndDate,
                request.WorkLoad,
                request.UnitAmount,
                request.ContractCoefficient
                );

            await _repository.Create(result, ct);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ContractorContractDetail>(SharedErrors.UnknownError);
        }
    }
}
