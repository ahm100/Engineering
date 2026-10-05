using Engineering.Application.Abstractions.Data.OperationInfos;
using OperationInfoGroup = Engineering.Domain.Entities.OperationInfos.OperationInfoGroup;

namespace Engineering.Application.Services.OperationInfoGroups.Commands.CreateOperationInfoGroup;

public class CreateOperationInfoGroupCommandHandler : ICommandHandler<CreateOperationInfoGroupCommand, OperationInfoGroup>
{
    private readonly ILogger<CreateOperationInfoGroupCommand> _logger;
    private readonly IOperationInfoGroupRepository _repository;

    public CreateOperationInfoGroupCommandHandler(ILogger<CreateOperationInfoGroupCommand> logger, IOperationInfoGroupRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<OperationInfoGroup?>> Handle(CreateOperationInfoGroupCommand request, CT ct)
    {
        try
        {
            var newOperationInfoGroup = new OperationInfoGroup(request.OperationInfoGroupName, request.OperationInfoGroupCode, request.IsActive, request.CompanyId);
            var result = await _repository.Create(newOperationInfoGroup, ct);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<OperationInfoGroup>(SharedErrors.UnknownError);
        }
    }
}