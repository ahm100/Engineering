using Engineering.Application.Abstractions.Data.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries.Enums;

namespace Engineering.Application.Services.RequestMachineryManagements.Commands.UpdateRequestMachineryOnPaymentState;

public class UpdateRequestMachineryOnPaymentStateCommandHandler : ICommandHandler<UpdateRequestMachineryOnPaymentStateCommand, bool>
{
    private readonly ILogger<UpdateRequestMachineryOnPaymentStateCommandHandler> _logger;
    private readonly IRequestMachineryRepository _repository;

    public UpdateRequestMachineryOnPaymentStateCommandHandler(ILogger<UpdateRequestMachineryOnPaymentStateCommandHandler> logger, IRequestMachineryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<bool>> Handle(UpdateRequestMachineryOnPaymentStateCommand request, CT ct)
    {
        try
        {
            var entities = await _repository.GetByIdsAsync(request.RequestMachineriesId, ct);
            if (entities is null || entities.Distinct().Count() != request.RequestMachineriesId.Distinct().Count())
                return Result.Failure<bool>(RequestMachineryErrors.RequestMachineryNotFound);

            if (request.Status == RequestMachineryStatus.SendToManager)
                if (entities.Any(x => x.Status != RequestMachineryStatus.OnProject))
                    return Result.Failure<bool>(RequestMachineryErrors.InValidStatus);

            if (request.Status == RequestMachineryStatus.ManagerConfirm)
                if (entities.Any(x => x.Status != RequestMachineryStatus.SendToManager))
                    return Result.Failure<bool>(RequestMachineryErrors.InValidStatus);

            if (request.Status == RequestMachineryStatus.OnProject)
                if (entities.Any(x => x.Status != RequestMachineryStatus.ManagerConfirm))
                    return Result.Failure<bool>(RequestMachineryErrors.InValidStatus);

            foreach (var item in entities)
            {
                item.ChangeStatus(request.Status);

                if (request.Status == RequestMachineryStatus.ManagerConfirm)
                {
                    item.SetConfirmedDescription(request.Description);
                }
                if (request.Status == RequestMachineryStatus.SendToManager)
                {
                    item.SetManagerDescription(request.Description);
                }

                item.AddHistory(request.Description);
                await _repository.Update(item);
            }

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<bool>(SharedErrors.UnknownError);
        }
    }
}
