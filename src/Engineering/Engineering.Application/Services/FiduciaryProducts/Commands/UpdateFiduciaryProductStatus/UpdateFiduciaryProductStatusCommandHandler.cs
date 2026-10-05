using Engineering.Application.Abstractions.Data.FiduciaryProducts;
using Engineering.Domain.Entities.FiduciaryProducts;

namespace Engineering.Application.Services.FiduciaryProducts.Commands.UpdateFiduciaryProductStatus;

public class UpdateFiduciaryProductStatusCommandHandler : ICommandHandler<UpdateFiduciaryProductStatusCommand, FiduciaryProduct>
{
    private readonly ILogger<UpdateFiduciaryProductStatusCommandHandler> _logger;
    private readonly IFiduciaryProductRepository _repository;

    public UpdateFiduciaryProductStatusCommandHandler(ILogger<UpdateFiduciaryProductStatusCommandHandler> logger, IFiduciaryProductRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<FiduciaryProduct?>> Handle(UpdateFiduciaryProductStatusCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.FiduciaryProductId, ct);
            if (entity is null)
                return Result.Failure<FiduciaryProduct>(FiduciaryProductErrors.FiduciaryProductWithIdNotFound);

            entity.ChangeStatus(request.Status);
            entity.SetStatusDescription(request.StatusDescription);
            entity.SetLastDescription(request.LastDescripiton);
            entity.AddHistory();

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<FiduciaryProduct>(SharedErrors.UnknownError);
        }
    }
}
