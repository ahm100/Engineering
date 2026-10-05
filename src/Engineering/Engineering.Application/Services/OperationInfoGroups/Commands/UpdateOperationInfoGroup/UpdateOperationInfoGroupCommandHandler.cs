using Engineering.Application.Abstractions.Data.OperationInfos;
using OperationInfoGroup = Engineering.Domain.Entities.OperationInfos.OperationInfoGroup;

namespace Engineering.Application.Services.OperationInfoGroups.Commands.UpdateOperationInfoGroup;

public class UpdateOperationInfoGroupCommandHandler : ICommandHandler<UpdateOperationInfoGroupCommand, OperationInfoGroup>
{
    private readonly ILogger<UpdateOperationInfoGroupCommand> _logger;
    private readonly IOperationInfoGroupRepository _repository;

    public UpdateOperationInfoGroupCommandHandler(ILogger<UpdateOperationInfoGroupCommand> logger, IOperationInfoGroupRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<OperationInfoGroup?>> Handle(UpdateOperationInfoGroupCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.Id, ct);
            if (entity is null)
                return Result.Failure<OperationInfoGroup>(ProjectErrors.ProjectWithIdNotFound);

            entity.SetName(request.OperationInfoGroupName);
            entity.SetCode(request.OperationInfoGroupCode);
            entity.SetCompanyId(request.CompanyId);

            if (request.IsActive == false)
                entity.SetInActive();
            else
                entity.SetActive();

            await _repository.Update(entity);

            return entity;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<OperationInfoGroup>(SharedErrors.UnknownError);
        }
    }
}