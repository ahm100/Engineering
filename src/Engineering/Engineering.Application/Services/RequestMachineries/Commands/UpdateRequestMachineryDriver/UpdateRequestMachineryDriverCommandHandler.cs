using Engineering.Application.Abstractions.Data.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineries.Commands.UpdateRequestMachineryDriver;

public class UpdateRequestMachineryDriverCommandHandler : ICommandHandler<UpdateRequestMachineryDriverCommand, RequestMachinery>
{
    private readonly ILogger<UpdateRequestMachineryDriverCommandHandler> _logger;
    private readonly IRequestMachineryRepository _repository;

    public UpdateRequestMachineryDriverCommandHandler(ILogger<UpdateRequestMachineryDriverCommandHandler> logger, IRequestMachineryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestMachinery?>> Handle(UpdateRequestMachineryDriverCommand request, CT ct)
    {
        try
        {
            var entity = request.RequestMachinery;

            entity.SetDriverId(request.DriverId);
            entity.SetDriverName(request.DriverName);

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
