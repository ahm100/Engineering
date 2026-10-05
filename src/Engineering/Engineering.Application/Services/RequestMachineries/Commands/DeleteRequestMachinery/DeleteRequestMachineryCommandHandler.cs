using Engineering.Application.Abstractions.Data.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineries.Commands.DeleteRequestMachinery;

public class DeleteRequestMachineryCommandHandler : ICommandHandler<DeleteRequestMachineryCommand, RequestMachinery>
{
    private readonly ILogger<DeleteRequestMachineryCommandHandler> _logger;
    private readonly IRequestMachineryRepository _repository;

    public DeleteRequestMachineryCommandHandler(ILogger<DeleteRequestMachineryCommandHandler> logger, IRequestMachineryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestMachinery?>> Handle(DeleteRequestMachineryCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.RequestMachineryId, ct);
            if (entity is null)
                return Result.Failure<RequestMachinery>(RequestMachineryErrors.RequestMachineryNotFound);
            if (entity.IsDeleted)
                return Result.Failure<RequestMachinery>(RequestMachineryErrors.IsDeleted);

            entity.SetIsDeleted();

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
