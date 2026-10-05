using Engineering.Application.Abstractions.Data.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineries.Commands.SetOnProjectRequestMachineryConfirmDate;

public class SetOnProjectRequestMachineryConfirmDateCommandHandler : ICommandHandler<SetOnProjectRequestMachineryConfirmDateCommand, RequestMachinery>
{
    private readonly ILogger<SetOnProjectRequestMachineryConfirmDateCommandHandler> _logger;
    private readonly IRequestMachineryRepository _repository;

    public SetOnProjectRequestMachineryConfirmDateCommandHandler(ILogger<SetOnProjectRequestMachineryConfirmDateCommandHandler> logger, IRequestMachineryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestMachinery?>> Handle(SetOnProjectRequestMachineryConfirmDateCommand request, CT ct)
    {
        try
        {
            var entity = request.RequestMachinery;

            if (entity.FromDate > entity.ToDate)
                return Result.Failure<RequestMachinery>(RequestMachineryErrors.InValidConfirmDates);

            entity.SetConfirmFromDate(entity.FromDate);
            entity.SetConfirmToDate(entity.ToDate);

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
