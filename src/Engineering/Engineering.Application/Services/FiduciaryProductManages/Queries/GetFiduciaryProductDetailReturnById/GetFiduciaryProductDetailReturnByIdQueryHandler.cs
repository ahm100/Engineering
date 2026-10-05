using Engineering.Application.Abstractions.Data.FiduciaryProducts;
using Engineering.Domain.Entities.FiduciaryProducts;

namespace Engineering.Application.Services.FiduciaryProductManages.Queries.GetFiduciaryProductDetailReturnById;

public class GetFiduciaryProductDetailReturnByIdQueryHandler : IQueryHandler<GetFiduciaryProductDetailReturnByIdQuery, FiduciaryProductDetailReturn>
{
    private readonly ILogger<GetFiduciaryProductDetailReturnByIdQueryHandler> _logger;
    private readonly IFiduciaryProductDetailReturnRepository _repository;

    public GetFiduciaryProductDetailReturnByIdQueryHandler(ILogger<GetFiduciaryProductDetailReturnByIdQueryHandler> logger, IFiduciaryProductDetailReturnRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<FiduciaryProductDetailReturn?>> Handle(GetFiduciaryProductDetailReturnByIdQuery request, CT ct)
    {
        try
        {
            var entity = await _repository.GetByIdAsync(request.FiduciaryProductDetailReturnId, ct);
            if (entity is null)
                return Result.Failure<FiduciaryProductDetailReturn>(FiduciaryProductDetailReturnErrors.FiduciaryProductDetailReturnWithIdNotFound);

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<FiduciaryProductDetailReturn>(SharedErrors.UnknownError);
        }
    }
}
