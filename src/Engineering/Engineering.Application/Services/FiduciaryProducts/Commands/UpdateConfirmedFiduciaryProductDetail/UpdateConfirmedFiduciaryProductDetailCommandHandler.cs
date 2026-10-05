using Engineering.Application.Abstractions.Data.FiduciaryProducts;
using Engineering.Domain.Entities.FiduciaryProducts;

namespace Engineering.Application.Services.FiduciaryProductDetails.Commands.UpdateConfirmedFiduciaryProductDetail;

public class UpdateConfirmedFiduciaryProductDetailCommandHandler : ICommandHandler<UpdateConfirmedFiduciaryProductDetailCommand, FiduciaryProductDetail>
{
    private readonly IFiduciaryProductDetailRepository _repository;
    private readonly ILogger<UpdateConfirmedFiduciaryProductDetailCommandHandler> _logger;

    public UpdateConfirmedFiduciaryProductDetailCommandHandler(IFiduciaryProductDetailRepository repository, ILogger<UpdateConfirmedFiduciaryProductDetailCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<FiduciaryProductDetail?>> Handle(UpdateConfirmedFiduciaryProductDetailCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.FiduciaryProductDetailId, ct);
            if (entity is null)
                return Result.Failure<FiduciaryProductDetail>(FiduciaryProductDetailErrors.FiduciaryProductDetailWithIdNotFound);

            entity.SetConfirmedDailyLateFine(request.ConfirmedDailyLateFine);
            entity.SetConfirmedLoanDays(request.ConfirmLoanDays);
            entity.SetDeliverDate(DateTime.Now);
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
