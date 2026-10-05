using Engineering.Application.Abstractions.Data.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineries.Commands.UpdateRequestMachineryConfirmDescription;

public class UpdateRequestMachineryConfirmDescriptionCommandHandler : ICommandHandler<UpdateRequestMachineryConfirmDescriptionCommand, RequestMachinery>
{
    private readonly ILogger<UpdateRequestMachineryConfirmDescriptionCommandHandler> _logger;
    private readonly IRequestMachineryRepository _repository;

    public UpdateRequestMachineryConfirmDescriptionCommandHandler(ILogger<UpdateRequestMachineryConfirmDescriptionCommandHandler> logger, IRequestMachineryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestMachinery?>> Handle(UpdateRequestMachineryConfirmDescriptionCommand request, CT ct)
    {
        try
        {
            var entity = request.RequestMachinery;

            entity.SetConfirmedTimeRequired(request.ConfirmTimeRequired);
            entity.SetConfirmedDescription(request.ConfirmedDescription);

            entity.ChangeStatus(Domain.Entities.RequestMachineries.Enums.RequestMachineryStatus.Confirmed);

            entity.AddHistory(request.ConfirmedDescription);

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
