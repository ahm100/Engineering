using Engineering.Application.Abstractions.Data.OperationInfos;
using Engineering.Domain.Entities.OperationInfos;

namespace Engineering.Application.Services.OperationInfoGroups.Queries.GetByName;

public class GetOperationInfoGroupByNameQueryHandler : IQueryHandler<GetOperationInfoGroupByNameQuery, OperationInfoGroup?>
{
    private readonly ILogger<GetOperationInfoGroupByNameQueryHandler> _logger;
    private readonly IOperationInfoGroupRepository _repository;

    public GetOperationInfoGroupByNameQueryHandler(ILogger<GetOperationInfoGroupByNameQueryHandler> logger, IOperationInfoGroupRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<OperationInfoGroup?>> Handle(GetOperationInfoGroupByNameQuery request, CT ct)
    {
        try
        {
            var entity = await _repository.FindByName(request.OperationInfoGroupName, request.CompanyId, ct);
            return entity ?? Result.Failure<OperationInfoGroup>(OperationInfoGroupErrors.OperationInfoGroupWithNameNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<OperationInfoGroup>(SharedErrors.UnknownError);
        }
    }
}
