using Engineering.Application.Abstractions.Data.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineries.Commands.UpdateRequestMachineryMachinery;

public class UpdateRequestMachineryMachineryCommandHandler : ICommandHandler<UpdateRequestMachineryMachineryCommand, RequestMachinery>
{
    private readonly ILogger<UpdateRequestMachineryMachineryCommandHandler> _logger;
    private readonly IRequestMachineryRepository _repository;

    public UpdateRequestMachineryMachineryCommandHandler(ILogger<UpdateRequestMachineryMachineryCommandHandler> logger, IRequestMachineryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestMachinery?>> Handle(UpdateRequestMachineryMachineryCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.GetByIdAsync(request.RequestMachineryId, ct);
            if (entity is null)
                return Result.Failure<RequestMachinery>(RequestMachineryErrors.RequestMachineryNotFound);

            entity.SetMachinery(request.Machinery);

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
