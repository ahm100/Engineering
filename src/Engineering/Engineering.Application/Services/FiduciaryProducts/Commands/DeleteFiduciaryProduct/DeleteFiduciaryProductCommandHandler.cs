using Engineering.Application.Abstractions.Data.FiduciaryProducts;
using Engineering.Domain.Entities.FiduciaryProducts;
using Engineering.Domain.Entities.FiduciaryProducts.Enums;

namespace Engineering.Application.Services.FiduciaryProducts.Commands.DeleteFiduciaryProduct;

public class DeleteFiduciaryProductCommandHandler : ICommandHandler<DeleteFiduciaryProductCommand, FiduciaryProduct>
{
    private readonly IFiduciaryProductRepository _repository;
    private readonly ILogger<DeleteFiduciaryProductCommandHandler> _logger;

    public DeleteFiduciaryProductCommandHandler(IFiduciaryProductRepository repository, ILogger<DeleteFiduciaryProductCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<FiduciaryProduct?>> Handle(DeleteFiduciaryProductCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.GetByIdAsync(request.FiduciaryProductId, ct);
            if (entity is null)
                return Result.Failure<FiduciaryProduct>(FiduciaryProductErrors.FiduciaryProductWithIdNotFound);
            if (entity.IsDeleted)
                return Result.Failure<FiduciaryProduct>(FiduciaryProductErrors.IsDeleted);
            if (!(ValidateFiduciaryProductStatus.AllowForDelete.Any(x => x == entity.Status)))
                return Result.Failure<FiduciaryProduct>(FiduciaryProductErrors.InValidStatus);

            entity.SetIsDeleted();
            entity.AddHistory();

            foreach (var item in entity.Details)
            {
                item.SetIsDeleted();
                item.AddHistory();
            }

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<FiduciaryProduct>(SharedErrors.UnknownError);
        }
    }
}
