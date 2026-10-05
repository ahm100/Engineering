using Engineering.Application.Abstractions.Data.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineries.Commands.UpdateRequestMachineryConfirmDate;

public class UpdateRequestMachineryConfirmDateCommandHandler : ICommandHandler<UpdateRequestMachineryConfirmDateCommand, RequestMachinery>
{
    private readonly ILogger<UpdateRequestMachineryConfirmDateCommandHandler> _logger;
    private readonly IRequestMachineryRepository _repository;

    public UpdateRequestMachineryConfirmDateCommandHandler(ILogger<UpdateRequestMachineryConfirmDateCommandHandler> logger, IRequestMachineryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestMachinery?>> Handle(UpdateRequestMachineryConfirmDateCommand request, CT ct)
    {
        try
        {
            var entity = request.RequestMachinery;

            DateTime? fromDate = null;
            if (request.ConfirmFromTime != null)
            {
                fromDate = request.ConfirmFromDate.Date.Add(request.ConfirmFromTime.Value);
            }
            else
                fromDate = request.ConfirmFromDate;
            DateTime? toDate = null;
            if (request.ConfirmToTime != null)
            {
                toDate = request.ConfirmToDate.Date.Add(request.ConfirmToTime.Value);
            }
            else
                toDate = request.ConfirmToDate;

            if (fromDate > toDate)
                return Result.Failure<RequestMachinery>(RequestMachineryErrors.InValidConfirmDates);

            entity.SetConfirmFromDate(fromDate);
            entity.SetConfirmToDate(toDate);

            entity.SetConfirmedTimeRequired(request.ConfirmTimeRequired);
            entity.SetConfirmedDescription(request.ConfirmedDescription);

            entity.ChangeStatus(Domain.Entities.RequestMachineries.Enums.RequestMachineryStatus.OnProject);
            entity.SetContractorId(request.ContractorId);

            if (request.ContractorMachinery is not null)
                entity.SetContractorMachinery(request.ContractorMachinery);

            entity.AddHistory("تخصیص ماشین آلات و استفاده در پروژه");

            await _repository.Update(entity);

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<RequestMachinery>(SharedErrors.UnknownError);
        }
    }
}
