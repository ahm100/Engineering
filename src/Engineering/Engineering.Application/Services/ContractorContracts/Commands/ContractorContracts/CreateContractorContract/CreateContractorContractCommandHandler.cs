using Engineering.Application.Abstractions.Data.ContractorContracts;
using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Application.Services.ContractorContracts.Commands.ContractorContracts.CreateContractorContract;

public class CreateContractorContractCommandHandler : ICommandHandler<CreateContractorContractCommand, ContractorContract>
{
    private readonly ILogger<CreateContractorContractCommandHandler> _logger;
    private readonly IContractorContractRepository _repository;

    public CreateContractorContractCommandHandler(
        ILogger<CreateContractorContractCommandHandler> logger,
        IContractorContractRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ContractorContract?>> Handle(CreateContractorContractCommand request, CT ct)
    {
        try
        {
            var result = new ContractorContract(
                request.CompanyId,
                request.ContractorContractHeader,
                request.ContractorContractType,
                request.Project,
                request.StartDate,
                request.EndDate,
                request.TotalAmount,
                request.PercentageDoingJobWell,
                request.DoingJobWellAmount,
                request.PercentageAdvancePayment,
                request.AdvancePaymentAmount,
                request.DailyLatenessPenalty,
                request.WorkDonePercent,
                request.WorkDeliveryPercent,
                request.WorkCompletionPercent,
                request.DailyBaseHours,
                request.MonthlyBaseHours,
                request.Description
                );

            await _repository.Create(result, ct);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ContractorContract>(SharedErrors.UnknownError);
        }
    }
}
