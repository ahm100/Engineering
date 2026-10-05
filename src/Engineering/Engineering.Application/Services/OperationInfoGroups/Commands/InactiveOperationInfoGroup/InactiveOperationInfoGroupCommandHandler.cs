using Engineering.Application.Abstractions.Data.OperationInfos;
using OperationInfoGroup = Engineering.Domain.Entities.OperationInfos.OperationInfoGroup;

namespace Engineering.Application.Services.OperationInfoGroups.Commands.InactiveOperationInfoGroup;

public class InactiveOperationInfoGroupCommandHandler : ICommandHandler<InactiveOperationInfoGroupCommand, OperationInfoGroup>
{
    private readonly ILogger<InactiveOperationInfoGroupCommand> _logger;
    private readonly IOperationInfoGroupRepository _repository;

    public InactiveOperationInfoGroupCommandHandler(ILogger<InactiveOperationInfoGroupCommand> logger, IOperationInfoGroupRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<OperationInfoGroup?>> Handle(InactiveOperationInfoGroupCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.Id, ct);
            if (entity is null)
                return Result.Failure<OperationInfoGroup>(OperationInfoGroupErrors.OperationInfoGroupWithIdNotFound);
            if (entity.IsActive == false)
                return Result.Failure<OperationInfoGroup>(OperationInfoGroupErrors.IsInactive);
            if (entity.IsDeleted == true)
                return Result.Failure<OperationInfoGroup>(OperationInfoGroupErrors.IsDeleted);

            entity.SetInActive();

            await _repository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<OperationInfoGroup>(SharedErrors.UnknownError);
        }
    }
}