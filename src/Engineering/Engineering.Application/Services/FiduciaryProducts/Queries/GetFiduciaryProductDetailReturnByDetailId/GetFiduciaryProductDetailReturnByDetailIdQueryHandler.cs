using Engineering.Application.Abstractions.Data.FiduciaryProducts;
using Engineering.Application.Services.FiduciaryProductManages.Models.GetFiduciaryProductDetailReturnByDetailId;

namespace Engineering.Application.Services.FiduciaryProducts.Queries.GetFiduciaryProductDetailReturnByDetailId;

public class GetFiduciaryProductDetailReturnByDetailIdQueryHandler : IQueryHandler<GetFiduciaryProductDetailReturnByDetailIdQuery, GetFiduciaryProductDetailReturnByDetailIdResponse>
{
    private readonly ILogger<GetFiduciaryProductDetailReturnByDetailIdQueryHandler> _logger;
    private readonly IFiduciaryProductDetailRepository _repository;

    public GetFiduciaryProductDetailReturnByDetailIdQueryHandler(ILogger<GetFiduciaryProductDetailReturnByDetailIdQueryHandler> logger, IFiduciaryProductDetailRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<GetFiduciaryProductDetailReturnByDetailIdResponse?>> Handle(GetFiduciaryProductDetailReturnByDetailIdQuery request, CT ct)
    {
        try
        {
            var entity = await _repository.GetDetailReturnById(request.FiduciaryProductDetailId, ct);
            if (entity is null)
                return Result.Failure<GetFiduciaryProductDetailReturnByDetailIdResponse>(FiduciaryProductDetailErrors.FiduciaryProductDetailWithIdNotFound);

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetFiduciaryProductDetailReturnByDetailIdResponse>(SharedErrors.UnknownError);
        }
    }
}
