using Engineering.Application.Abstractions.Data.OperationInfos;
using OperationInfo = Engineering.Domain.Entities.OperationInfos.OperationInfo;

namespace Engineering.Application.Services.OperationInfos.Commands.InactiveOperationInfo;

public class InactiveOperationInfoCommandHandler : ICommandHandler<InactiveOperationInfoCommand, OperationInfo>
{
    private readonly ILogger<InactiveOperationInfoCommand> _logger;
    private readonly IOperationInfoRepository _repository;

    public InactiveOperationInfoCommandHandler(ILogger<InactiveOperationInfoCommand> logger, IOperationInfoRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<OperationInfo?>> Handle(InactiveOperationInfoCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.Id, ct);
            if (entity is null)
                return Result.Failure<OperationInfo>(OperationInfoErrors.OperationInfoWithIdNotFound);
            if (entity.IsActive == false)
                return Result.Failure<OperationInfo>(OperationInfoErrors.IsInactive);
            if (entity.IsDeleted == true)
                return Result.Failure<OperationInfo>(OperationInfoErrors.IsDeleted);

            entity.SetInActive();
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