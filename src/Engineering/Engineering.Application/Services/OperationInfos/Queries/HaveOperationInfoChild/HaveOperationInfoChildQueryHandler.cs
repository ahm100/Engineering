using Engineering.Application.Abstractions.Data.OperationInfos;
using OperationInfo = Engineering.Domain.Entities.OperationInfos.OperationInfo;

namespace Engineering.Application.Services.OperationInfos.Queries.HaveOperationInfoChild;

public class HaveOperationInfoChildQueryHandler : IQueryHandler<HaveOperationInfoChildQuery, OperationInfo>
{
    private readonly ILogger<HaveOperationInfoChildQueryHandler> _logger;
    private readonly IOperationInfoRepository _repository;

    public HaveOperationInfoChildQueryHandler(ILogger<HaveOperationInfoChildQueryHandler> logger, IOperationInfoRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<OperationInfo?>> Handle(HaveOperationInfoChildQuery request, CT ct)
    {
        try
        {
            var result = await _repository.HaveOperationInfoChild(request.Id, ct);

            return result ?? Result.Failure<OperationInfo>(SharedErrors.ItemNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<OperationInfo>(SharedErrors.UnknownError);
        }
    }
}