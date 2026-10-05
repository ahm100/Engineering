using Engineering.Application.Abstractions.Data.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineryManagements.Commands.SetUnConfirmRequestMachinery;

public class SetUnConfirmRequestMachineryCommandHandler : ICommandHandler<SetUnConfirmRequestMachineryCommand, RequestMachinery>
{
    private readonly ILogger<SetUnConfirmRequestMachineryCommandHandler> _logger;
    private readonly IRequestMachineryRepository _repository;

    public SetUnConfirmRequestMachineryCommandHandler(ILogger<SetUnConfirmRequestMachineryCommandHandler> logger, IRequestMachineryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestMachinery?>> Handle(SetUnConfirmRequestMachineryCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.RequestMachinery.Id, ct);
            if (entity is null)
                return Result.Failure<RequestMachinery>(RequestMachineryErrors.RequestMachineryNotFound);

            entity.SetContractorId(null);
            entity.SetConfirmFromDate(null);
            entity.SetConfirmToDate(null);
            entity.SetConfirmedTimeRequired(null);
            entity.SetConfirmedDescription(null);
            entity.SetDriverId(null);
            entity.SetDriverName(null);

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
