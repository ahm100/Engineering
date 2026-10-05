using Engineering.Application.Abstractions.Data.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineries.Commands.DeleteRequestMachineryProjectOperation;

public class DeleteRequestMachineryProjectOperationCommandHandler : ICommandHandler<DeleteRequestMachineryProjectOperationCommand, RequestMachineryProjectOperation>
{
    private readonly ILogger<DeleteRequestMachineryProjectOperationCommandHandler> _logger;
    private readonly IRequestMachineryProjectOperationRepository _repository;

    public DeleteRequestMachineryProjectOperationCommandHandler(ILogger<DeleteRequestMachineryProjectOperationCommandHandler> logger, IRequestMachineryProjectOperationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestMachineryProjectOperation?>> Handle(DeleteRequestMachineryProjectOperationCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.RequestMachineryProjectOperationId, ct);
            if (entity is null)
                return Result.Failure<RequestMachineryProjectOperation>(RequestMachineryProjectOperationErrors.RequestMachineryProjectOperationNotFound);
            if (entity.IsDeleted)
                return Result.Failure<RequestMachineryProjectOperation>(RequestMachineryProjectOperationErrors.IsDeleted);

            entity.SetIsDeleted();

            await _repository.Update(entity);

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<RequestMachineryProjectOperation>(SharedErrors.UnknownError);
        }
    }
}
