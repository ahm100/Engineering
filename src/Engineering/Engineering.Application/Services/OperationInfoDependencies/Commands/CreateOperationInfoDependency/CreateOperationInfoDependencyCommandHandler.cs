using Engineering.Application.Abstractions.Data.OperationInfos;
using OperationInfoDependency = Engineering.Domain.Entities.OperationInfos.OperationInfoDependency;

namespace Engineering.Application.Services.OperationInfoDependencies.Commands.CreateOperationInfoDependency;

public class CreateOperationInfoDependencyCommandHandler : ICommandHandler<CreateOperationInfoDependencyCommand, OperationInfoDependency>
{
    private readonly ILogger<CreateOperationInfoDependencyCommand> _logger;
    private readonly IOperationInfoDependencyRepository _repository;

    public CreateOperationInfoDependencyCommandHandler(ILogger<CreateOperationInfoDependencyCommand> logger, IOperationInfoDependencyRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<OperationInfoDependency?>> Handle(CreateOperationInfoDependencyCommand request, CT ct)
    {
        try
        {
            var entity = new OperationInfoDependency(request.OperationInfo, request.RelationId, request.WorkingDays, request.DependencyType);
            var result = await _repository.Create(entity, ct);

            return entity;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<OperationInfoDependency>(SharedErrors.UnknownError);
        }
    }
}
