using Engineering.Application.Abstractions.Data.FiduciaryProducts;
using Engineering.Domain.Entities.FiduciaryProducts;
using Engineering.Domain.Entities.FiduciaryProducts.Enums;

namespace Engineering.Application.Services.FiduciaryProducts.Commands.UpdateFiduciaryProduct;

public class UpdateFiduciaryProductCommandHandler : ICommandHandler<UpdateFiduciaryProductCommand, FiduciaryProduct>
{
    private readonly IFiduciaryProductRepository _repository;
    private readonly ILogger<UpdateFiduciaryProductCommandHandler> _logger;

    public UpdateFiduciaryProductCommandHandler(IFiduciaryProductRepository repository, ILogger<UpdateFiduciaryProductCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<FiduciaryProduct?>> Handle(UpdateFiduciaryProductCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.FiduciaryProduct.Id, ct);
            if (entity is null)
                return Result.Failure<FiduciaryProduct>(FiduciaryProductErrors.FiduciaryProductWithIdNotFound);

            entity.SetProject(request.Project);
            entity.SetProjectOperation(request.ProjectOperation);
            entity.SetThirdPartyId(request.ThirdPartyId);
            entity.SetDescription(request.Description);
            entity.SetCompanyId(request.CompanyId);

            if (entity.Status == FiduciaryProductStatus.Rejected)
                entity.ChangeStatus(FiduciaryProductStatus.Resended);
            else
                entity.ChangeStatus(FiduciaryProductStatus.New);

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
