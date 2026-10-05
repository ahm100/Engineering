using Engineering.Application.Abstractions.Data.FiduciaryProducts;
using Engineering.Domain.Entities.FiduciaryProducts;
using Engineering.Domain.Entities.FiduciaryProducts.Enums;

namespace Engineering.Application.Services.FiduciaryProductDetails.Commands.UpdateFiduciaryProductDetail;

public class UpdateFiduciaryProductDetailCommandHandler : ICommandHandler<UpdateFiduciaryProductDetailCommand, FiduciaryProductDetail>
{
    private readonly IFiduciaryProductDetailRepository _repository;
    private readonly ILogger<UpdateFiduciaryProductDetailCommandHandler> _logger;

    public UpdateFiduciaryProductDetailCommandHandler(IFiduciaryProductDetailRepository repository, ILogger<UpdateFiduciaryProductDetailCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<FiduciaryProductDetail?>> Handle(UpdateFiduciaryProductDetailCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.FiduciaryProductDetail.Id, ct);
            if (entity is null)
                return Result.Failure<FiduciaryProductDetail>(FiduciaryProductDetailErrors.FiduciaryProductDetailWithIdNotFound);

            entity.SetProductId(request.ProductId);
            entity.SetMeasureUnitId(request.MeasureUnitId);
            entity.SetCurrencyId(request.CurrencyId);
            entity.SetDailyLateFine(request.DailyLateFine);
            entity.SetLoanDays(request.LoanDays);
            entity.SetDescription(request.Description);
            entity.SetLoanCount(request.LoanCount);

            if (entity.Status == FiduciaryProductDetailStatus.Rejected)
                entity.ChangeStatus(FiduciaryProductDetailStatus.DetailResened);
            else
                entity.ChangeStatus(FiduciaryProductDetailStatus.New);

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
