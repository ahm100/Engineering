using Engineering.Application.Abstractions.Data.OperationInfos;
using OperationInfoGroup = Engineering.Domain.Entities.OperationInfos.OperationInfoGroup;

namespace Engineering.Application.Services.OperationInfoGroups.Queries.GetOperationInfoGroupById;

public class GetOperationInfoGroupByIdQueryHandler : IQueryHandler<GetOperationInfoGroupByIdQuery, OperationInfoGroup>
{
    private readonly ILogger<GetOperationInfoGroupByIdQueryHandler> _logger;
    private readonly IOperationInfoGroupRepository _repository;

    public GetOperationInfoGroupByIdQueryHandler(ILogger<GetOperationInfoGroupByIdQueryHandler> logger, IOperationInfoGroupRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<OperationInfoGroup?>> Handle(GetOperationInfoGroupByIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetById(request.Id, ct);

            return result ?? Result.Failure<OperationInfoGroup>(ProjectErrors.ProjectWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<OperationInfoGroup>(SharedErrors.UnknownError);
        }
    }
}