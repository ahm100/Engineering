using Engineering.Application.Abstractions.Data.OperationInfos;
using OperationInfo = Engineering.Domain.Entities.OperationInfos.OperationInfo;

namespace Engineering.Application.Services.OperationInfos.Queries.GetOperationInfoByIdByProducts;

public class GetOperationInfoByIdByProductsQueryHandler : IQueryHandler<GetOperationInfoByIdByProductsQuery, OperationInfo>
{
    private readonly ILogger<GetOperationInfoByIdByProductsQueryHandler> _logger;
    private readonly IOperationInfoRepository _repository;

    public GetOperationInfoByIdByProductsQueryHandler(ILogger<GetOperationInfoByIdByProductsQueryHandler> logger, IOperationInfoRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<OperationInfo?>> Handle(GetOperationInfoByIdByProductsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetOperationInfoByIdByProducts(request.Id, ct);
            return result ?? Result.Failure<OperationInfo>(OperationInfoErrors.OperationInfoWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<OperationInfo>(SharedErrors.UnknownError);
        }
    }
}