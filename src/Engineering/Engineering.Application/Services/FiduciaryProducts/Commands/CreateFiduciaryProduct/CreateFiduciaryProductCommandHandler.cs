using Engineering.Application.Abstractions.Data.FiduciaryProducts;
using Engineering.Domain.Entities.FiduciaryProducts;

namespace Engineering.Application.Services.FiduciaryProducts.Commands.CreateFiduciaryProduct;

public class CreateFiduciaryProductCommandHandler : ICommandHandler<CreateFiduciaryProductCommand, FiduciaryProduct>
{
    private readonly IFiduciaryProductRepository _repository;
    private readonly ILogger<CreateFiduciaryProductCommandHandler> _logger;

    public CreateFiduciaryProductCommandHandler(IFiduciaryProductRepository repository, ILogger<CreateFiduciaryProductCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<FiduciaryProduct?>> Handle(CreateFiduciaryProductCommand request, CT ct)
    {
        try
        {
            var entity = new FiduciaryProduct(request.Project, request.ProjectOperation, request.ThirdPartyId, request.Description, request.CompanyId);

            await _repository.Create(entity, ct);

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<FiduciaryProduct>(SharedErrors.UnknownError);
        }
    }
}
