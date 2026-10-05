using Engineering.Application.Abstractions.Data.FiduciaryProducts;
using Engineering.Domain.Entities.FiduciaryProducts;

namespace Engineering.Application.Services.FiduciaryProducts.Commands.DeleteFiduciaryProductDetail;

public class DeleteFiduciaryProductDetailCommandHandler : ICommandHandler<DeleteFiduciaryProductDetailCommand, FiduciaryProductDetail>
{
    private readonly IFiduciaryProductDetailRepository _repository;
    private readonly ILogger<DeleteFiduciaryProductDetailCommandHandler> _logger;

    public DeleteFiduciaryProductDetailCommandHandler(IFiduciaryProductDetailRepository repository, ILogger<DeleteFiduciaryProductDetailCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<FiduciaryProductDetail?>> Handle(DeleteFiduciaryProductDetailCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.FiduciaryProductDetailId, ct);
            if (entity is null)
                return Result.Failure<FiduciaryProductDetail>(FiduciaryProductDetailErrors.FiduciaryProductDetailWithIdNotFound);
            if (entity.IsDeleted)
                return Result.Failure<FiduciaryProductDetail>(FiduciaryProductDetailErrors.IsDeleted);

            entity.SetIsDeleted();
            entity.AddHistory();

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<FiduciaryProductDetail>(SharedErrors.UnknownError);
        }
    }
}
