using Engineering.Application.Abstractions.Data.OperationInfos;
using Engineering.Domain.Entities.OperationInfos;
namespace Engineering.Application.Services.OperationInfoGroupRelations.Commands.CreateOperationInfoGroupRelation;

public class CreateOperationInfoGroupRelationCommandHandler : ICommandHandler<CreateOperationInfoGroupRelationCommand, List<OperationInfoGroupRelation>?>
{
    private readonly ILogger<CreateOperationInfoGroupRelationCommand> _logger;
    private readonly IOperationInfoGroupRelationRepository _repository;

    public CreateOperationInfoGroupRelationCommandHandler(ILogger<CreateOperationInfoGroupRelationCommand> logger, IOperationInfoGroupRelationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<List<OperationInfoGroupRelation>?>> Handle(CreateOperationInfoGroupRelationCommand request, CT ct)
    {
        try
        {
            var result = new List<OperationInfoGroupRelation>();
            foreach (var item in request.OperationInfoGroups)
                result.Add(await _repository.Create(new OperationInfoGroupRelation(request.OperationInfo, item), ct));

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<OperationInfoGroupRelation>>(SharedErrors.UnknownError)!;
        }
    }
}