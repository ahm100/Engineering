using Engineering.Application.Abstractions.Data.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineries.Commands.DeleteRequestMachineryProjectOperationDetail;

public class DeleteRequestMachineryProjectOperationDetailCommandHandler : ICommandHandler<DeleteRequestMachineryProjectOperationDetailCommand, RequestMachineryProjectOperationDetail>
{
    private readonly ILogger<DeleteRequestMachineryProjectOperationDetailCommandHandler> _logger;
    private readonly IRequestMachineryProjectOperationDetailRepository _repository;

    public DeleteRequestMachineryProjectOperationDetailCommandHandler(ILogger<DeleteRequestMachineryProjectOperationDetailCommandHandler> logger, IRequestMachineryProjectOperationDetailRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestMachineryProjectOperationDetail?>> Handle(DeleteRequestMachineryProjectOperationDetailCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.RequestMachineryProjectOperationDetailId, ct);
            if (entity is null)
                return Result.Failure<RequestMachineryProjectOperationDetail>(RequestMachineryProjectOperationDetailErrors.RequestMachineryProjectOperationDetailNotFound);
            if (entity.IsDeleted)
                return Result.Failure<RequestMachineryProjectOperationDetail>(RequestMachineryProjectOperationDetailErrors.IsDeleted);

            entity.SetIsDeleted();

            await _repository.Update(entity);

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<RequestMachineryProjectOperationDetail>(SharedErrors.UnknownError);
        }
    }
}
