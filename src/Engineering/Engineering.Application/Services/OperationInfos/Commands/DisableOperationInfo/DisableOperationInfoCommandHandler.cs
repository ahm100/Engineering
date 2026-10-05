using Engineering.Application.Abstractions.Data.OperationInfos;
using OperationInfo = Engineering.Domain.Entities.OperationInfos.OperationInfo;

namespace Engineering.Application.Services.OperationInfos.Commands.DisableOperationInfo;

public class DisableOperationInfoCommandHandler : ICommandHandler<DisableOperationInfoCommand, OperationInfo>
{
    private readonly ILogger<DisableOperationInfoCommand> _logger;
    private readonly IOperationInfoRepository _repository;

    public DisableOperationInfoCommandHandler(ILogger<DisableOperationInfoCommand> logger, IOperationInfoRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<OperationInfo?>> Handle(DisableOperationInfoCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindForDelete(request.Id, ct);
            if (entity is null)
                return Result.Failure<OperationInfo>(OperationInfoErrors.OperationInfoWithIdNotFound);
            if (entity.IsDeleted == true)
                return Result.Failure<OperationInfo>(OperationInfoErrors.IsDeleted);
            if (entity.ProjectOperations.Any())
                return Result.Failure<OperationInfo>(OperationInfoErrors.CanNottDeleteForProjectOperations);
            if (entity.ProjectOperations.Any(x => x.ProjectOperationDetails.Count > 0))
                return Result.Failure<OperationInfo>(OperationInfoErrors.CanNottDeleteForProjectOperationDetails);
            if (entity.OperationInfoServices.Any())
                return Result.Failure<OperationInfo>(OperationInfoErrors.CanNottDeleteForServiceInfo);
            if (entity.OperationInfoDependencies.Any())
                return Result.Failure<OperationInfo>(OperationInfoErrors.CanNottDeleteForDependencies);
            //if (entity.ConsiderationDependencies.Any())
            //    return Result.Failure<OperationInfo>(OperationInfoErrors.CanNottDeleteForConsideration);

            entity.SetIsDeleted();
            entity.AddHistory();

            await _repository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<OperationInfo>(SharedErrors.UnknownError);
        }
    }
}