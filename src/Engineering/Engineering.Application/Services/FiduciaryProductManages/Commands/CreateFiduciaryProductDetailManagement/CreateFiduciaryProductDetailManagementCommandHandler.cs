using Engineering.Application.Abstractions.Data.FiduciaryProducts;
using Engineering.Domain.Entities.FiduciaryProducts;

namespace Engineering.Application.Services.FiduciaryProductManages.Commands.CreateFiduciaryProductDetailManagement;

public class CreateFiduciaryProductDetailManagementCommandHandler : ICommandHandler<CreateFiduciaryProductDetailManagementCommand, FiduciaryProductDetailManagement>
{
    private readonly ILogger<CreateFiduciaryProductDetailManagementCommandHandler> _logger;
    private readonly IFiduciaryProductDetailManagementRepository _repository;

    public CreateFiduciaryProductDetailManagementCommandHandler(ILogger<CreateFiduciaryProductDetailManagementCommandHandler> logger,
                                                           IFiduciaryProductDetailManagementRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<FiduciaryProductDetailManagement?>> Handle(CreateFiduciaryProductDetailManagementCommand request, CT ct)
    {
        try
        {
            var entity = new FiduciaryProductDetailManagement(request.InvoiceId, request.WarehouseId, request.DestWarehouseId, request.ConfirmedLoanCount, request.Description, request.FiduciaryProductDetail);

            await _repository.Create(entity, ct);

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<FiduciaryProductDetailManagement>(SharedErrors.UnknownError);
        }
    }
}
