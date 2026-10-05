using Engineering.Application.Abstractions.Data.OperationInfos;
using Engineering.Domain.Entities.OperationInfos;

namespace Engineering.Application.Services.OperationInfoGroups.Queries.GetByCode;

public class GetOperationInfoGroupByCodeQueryHandler : IQueryHandler<GetOperationInfoGroupByCodeQuery, OperationInfoGroup?>
{
    private readonly ILogger<GetOperationInfoGroupByCodeQueryHandler> _logger;
    private readonly IOperationInfoGroupRepository _repository;

    public GetOperationInfoGroupByCodeQueryHandler(ILogger<GetOperationInfoGroupByCodeQueryHandler> logger, IOperationInfoGroupRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<OperationInfoGroup?>> Handle(GetOperationInfoGroupByCodeQuery request, CT ct)
    {
        try
        {
            var entity = await _repository.FindByCode(request.OperationInfoGroupCode, request.CompanyId, ct);
            return entity ?? Result.Failure<OperationInfoGroup>(OperationInfoGroupErrors.OperationInfoGroupWithCodeNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<OperationInfoGroup>(SharedErrors.UnknownError);
        }
    }
}