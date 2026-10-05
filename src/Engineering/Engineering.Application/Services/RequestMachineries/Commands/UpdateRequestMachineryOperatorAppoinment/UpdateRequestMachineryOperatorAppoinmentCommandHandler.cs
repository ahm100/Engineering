using Engineering.Application.Abstractions.Data.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineries.Commands.UpdateRequestMachineryOperatorAppoinment;

public class UpdateRequestMachineryOperatorAppoinmentCommandHandler : ICommandHandler<UpdateRequestMachineryOperatorAppoinmentCommand, RequestMachinery>
{
    private readonly ILogger<UpdateRequestMachineryOperatorAppoinmentCommandHandler> _logger;
    private readonly IRequestMachineryRepository _repository;

    public UpdateRequestMachineryOperatorAppoinmentCommandHandler(ILogger<UpdateRequestMachineryOperatorAppoinmentCommandHandler> logger, IRequestMachineryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestMachinery?>> Handle(UpdateRequestMachineryOperatorAppoinmentCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.RequestMachineryId, ct);
            if (entity is null)
                return Result.Failure<RequestMachinery>(RequestMachineryErrors.RequestMachineryNotFound);

            entity.SetOperatorAppoinmentId(request.OperatorAppoinmentId);
            entity.SetOperatorAppoinmentUserId(request.OperatorAppoinmentUserId);
            entity.AddHistory(null);

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
