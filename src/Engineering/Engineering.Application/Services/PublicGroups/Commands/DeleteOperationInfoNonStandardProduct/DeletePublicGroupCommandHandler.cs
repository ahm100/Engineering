using Engineering.Application.Abstractions.Data.OperationInfos;
using Engineering.Domain.Entities.OperationInfos;

namespace Engineering.Application.Services.PublicGroups.Commands.DeletePublicGroup;

public class DeletePublicGroupCommandHandler : ICommandHandler<DeletePublicGroupCommand, PublicGroup>
{
    private readonly ILogger<DeletePublicGroupCommand> _logger;
    private readonly IPublicGroupRepository _repository;

    public DeletePublicGroupCommandHandler(ILogger<DeletePublicGroupCommand> logger, IPublicGroupRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<PublicGroup?>> Handle(DeletePublicGroupCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.GetById(request.Id, ct);
            if (entity is null)
                return Result.Failure<PublicGroup>(OperationInfoErrors.OperationInfoNonStandardWithIdNotFound);
            if (entity.IsDeleted == true)
                return Result.Failure<PublicGroup>(OperationInfoErrors.NonstandardIsDeleted);

            entity.SetIsDeleted();

            await _repository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<PublicGroup>(SharedErrors.UnknownError);
        }
    }
}