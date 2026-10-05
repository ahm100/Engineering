using Engineering.Application.Abstractions.Data.OperationInfos;
using OperationInfo = Engineering.Domain.Entities.OperationInfos.OperationInfo;

namespace Engineering.Application.Services.OperationInfos.Commands.ActiveOperationInfo;

public class ActiveOperationInfoCommandHandler : ICommandHandler<ActiveOperationInfoCommand, OperationInfo>
{
    private readonly ILogger<ActiveOperationInfoCommand> _logger;
    private readonly IOperationInfoRepository _repository;

    public ActiveOperationInfoCommandHandler(ILogger<ActiveOperationInfoCommand> logger, IOperationInfoRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<OperationInfo?>> Handle(ActiveOperationInfoCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.Id, ct);
            if (entity is null)
                return Result.Failure<OperationInfo>(OperationInfoErrors.OperationInfoWithIdNotFound);
            if (entity.IsActive == true)
                return Result.Failure<OperationInfo>(OperationInfoErrors.IsActive);
            if (entity.IsDeleted == true)
                return Result.Failure<OperationInfo>(OperationInfoErrors.IsDeleted);

            entity.SetActive();
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