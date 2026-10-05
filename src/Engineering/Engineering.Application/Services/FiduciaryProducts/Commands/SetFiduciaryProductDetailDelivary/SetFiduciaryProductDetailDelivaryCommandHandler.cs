using Engineering.Application.Abstractions.Data.FiduciaryProducts;
using Engineering.Domain.Entities.FiduciaryProducts;
using Engineering.Domain.Entities.FiduciaryProducts.Enums;

namespace Engineering.Application.Services.FiduciaryProducts.Commands.SetFiduciaryProductDetailDelivary;

public class SetFiduciaryProductDetailDelivaryCommandHandler : ICommandHandler<SetFiduciaryProductDetailDelivaryCommand, FiduciaryProductDetail>
{
    private readonly IFiduciaryProductDetailRepository _repository;
    private readonly ILogger<SetFiduciaryProductDetailDelivaryCommandHandler> _logger;

    public SetFiduciaryProductDetailDelivaryCommandHandler(IFiduciaryProductDetailRepository repository, ILogger<SetFiduciaryProductDetailDelivaryCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<FiduciaryProductDetail?>> Handle(SetFiduciaryProductDetailDelivaryCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.FiduciaryProductDetailId, ct);
            if (entity is null)
                return Result.Failure<FiduciaryProductDetail>(FiduciaryProductDetailErrors.FiduciaryProductDetailWithIdNotFound);

            entity.SetDeliverDate(DateTime.Now);
            entity.ChangeStatus(FiduciaryProductDetailStatus.Delivary);
            entity.SetLastDescription(request.LastDescription);
            entity.SetStatusDescription(request.StatusDescription);
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
