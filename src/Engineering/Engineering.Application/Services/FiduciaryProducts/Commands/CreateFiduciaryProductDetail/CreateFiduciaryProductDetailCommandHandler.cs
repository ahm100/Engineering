using Engineering.Application.Abstractions.Data.FiduciaryProducts;
using Engineering.Domain.Entities.FiduciaryProducts;

namespace Engineering.Application.Services.FiduciaryProductDetails.Commands.CreateFiduciaryProductDetail;

public class CreateFiduciaryProductDetailCommandHandler : ICommandHandler<CreateFiduciaryProductDetailCommand, FiduciaryProductDetail>
{
    private readonly IFiduciaryProductDetailRepository _repository;
    private readonly ILogger<CreateFiduciaryProductDetailCommandHandler> _logger;

    public CreateFiduciaryProductDetailCommandHandler(IFiduciaryProductDetailRepository repository, ILogger<CreateFiduciaryProductDetailCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<FiduciaryProductDetail?>> Handle(CreateFiduciaryProductDetailCommand request, CT ct)
    {
        try
        {
            var entity = new FiduciaryProductDetail(request.FiduciaryProduct, request.ProductId, request.LoanCount, request.LoanDays, request.MeasureUnitId, request.CurrencyId, request.DailyLateFine);

            await _repository.Create(entity, ct);

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<FiduciaryProductDetail>(SharedErrors.UnknownError);
        }
    }
}
