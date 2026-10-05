using Engineering.Application.Abstractions.Data.OperationInfos;
using Engineering.Domain.Entities.OperationInfos;
namespace Engineering.Application.Services.PublicGroups.Commands.CreatePublicGroup;

public class CreatePublicGroupCommandHandler : ICommandHandler<CreatePublicGroupCommand, List<PublicGroup>?>
{
    private readonly ILogger<CreatePublicGroupCommand> _logger;
    private readonly IPublicGroupRepository _repository;

    public CreatePublicGroupCommandHandler(ILogger<CreatePublicGroupCommand> logger, IPublicGroupRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<List<PublicGroup>?>> Handle(CreatePublicGroupCommand request, CT ct)
    {
        try
        {
            var result = new List<PublicGroup>();
            foreach (var item in request.ProductGroupIds)
                result.Add(await _repository.Create(new PublicGroup(item), ct));

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<PublicGroup>>(SharedErrors.UnknownError)!;
        }
    }
}