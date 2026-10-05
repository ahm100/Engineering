using Engineering.Application.Abstractions.Data.OperationInfos;
using Engineering.Domain.Entities.OperationInfos;

namespace Engineering.Application.Services.OperationInfoServices.Commands.DeleteOperationInfoService;

public class DeleteOperationInfoServiceCommandHandler : ICommandHandler<DeleteOperationInfoServiceCommand, OperationInfoService>
{
    private readonly ILogger<DeleteOperationInfoServiceCommand> _logger;
    private readonly IOperationInfoServiceRepository _repository;

    public DeleteOperationInfoServiceCommandHandler(ILogger<DeleteOperationInfoServiceCommand> logger, IOperationInfoServiceRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<OperationInfoService?>> Handle(DeleteOperationInfoServiceCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.GetOperationInfoServiceByIdForDelete(request.OperationInfoId, request.ServiceInfoId, ct);
            if (entity is null)
                return Result.Failure<OperationInfoService>(OperationInfoServiceErrors.OperationInfoServiceWithIdNotFound);
            if (entity.IsDeleted == true)
                return Result.Failure<OperationInfoService>(OperationInfoServiceErrors.IsDeleted);
            var entityValidate = await _repository.ValidateForContractorServices(request.OperationInfoId, request.ServiceInfoId, ct);
            if (entityValidate)
                return Result.Failure<OperationInfoService>(ServiceInfoErrors.CanNotDeleteForContractorServices);

            entity.SetIsDeleted();

            await _repository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<OperationInfoService>(SharedErrors.UnknownError);
        }
    }
}