using Engineering.Application.Abstractions.Data.ContractorContracts;
using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Application.Services.ContractorContracts.Commands.ContractorContracts.UpdateContractorContract;

public class UpdateContractorContractCommandHandler : ICommandHandler<UpdateContractorContractCommand, ContractorContract>
{
    private readonly ILogger<UpdateContractorContractCommandHandler> _logger;
    private readonly IContractorContractRepository _repository;

    public UpdateContractorContractCommandHandler(
        ILogger<UpdateContractorContractCommandHandler> logger,
        IContractorContractRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ContractorContract?>> Handle(UpdateContractorContractCommand request, CT ct)
    {
        try
        {
            var entity = request.Entity;

            entity.SetStartDate(request.StartDate);
            entity.SetEndDate(request.EndDate);
            entity.SetTotalAmount(request.TotalAmount);
            entity.SetPercentageDoingJobWell(request.PercentageDoingJobWell);
            entity.SetDoingJobWellAmount(request.DoingJobWellAmount);
            entity.SetPercentageAdvancePayment(request.PercentageAdvancePayment);
            entity.SetAdvancePaymentAmount(request.AdvancePaymentAmount);
            entity.SetDailyLatenessPenalty(request.DailyLatenessPenalty);
            entity.SetWorkDonePercent(request.WorkDonePercent);
            entity.SetWorkDeliveryPercent(request.WorkDeliveryPercent);
            entity.SetWorkCompletionPercent(request.WorkCompletionPercent);
            entity.SetDailyBaseHours(request.DailyBaseHours);
            entity.SetMonthlyBaseHours(request.MonthlyBaseHours);
            entity.SetDescription(request.Description);
            entity.AddHistory();

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
