using Engineering.Application.Abstractions.Data.OperationInfos;
using Engineering.Domain.Entities.OperationInfos;

namespace Engineering.Application.Services.OperationInfoGroupRelations.Commands.DeleteOperationInfoGroupRelation;

public class DeleteOperationInfoGroupRelationCommandHandler : ICommandHandler<DeleteOperationInfoGroupRelationCommand, OperationInfoGroupRelation>
{
    private readonly ILogger<DeleteOperationInfoGroupRelationCommand> _logger;
    private readonly IOperationInfoGroupRelationRepository _repository;

    public DeleteOperationInfoGroupRelationCommandHandler(ILogger<DeleteOperationInfoGroupRelationCommand> logger, IOperationInfoGroupRelationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<OperationInfoGroupRelation?>> Handle(DeleteOperationInfoGroupRelationCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.GetById(request.OperationInfoGroupId, request.OprationInfoId, ct);
            if (entity is null)
                return Result.Failure<OperationInfoGroupRelation>(OperationInfoGroupRelationErrors.OperationInfoGroupRelationWithIdNotFound);
            if (entity.IsDeleted == true)
                return Result.Failure<OperationInfoGroupRelation>(OperationInfoGroupRelationErrors.IsDeleted);

            entity.SetIsDeleted();

            await _repository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<OperationInfoGroupRelation>(SharedErrors.UnknownError);
        }
    }
}