using Engineering.Application.Abstractions.Data.FiduciaryProducts;
using Engineering.Domain.Entities.FiduciaryProducts;

namespace Engineering.Application.Services.FiduciaryProducts.Commands.UpdateFiduciaryProductDetailStatus;

public class UpdateFiduciaryProductDetailStatusCommandHandler : ICommandHandler<UpdateFiduciaryProductDetailStatusCommand, FiduciaryProductDetail>
{
    private readonly ILogger<UpdateFiduciaryProductDetailStatusCommandHandler> _logger;
    private readonly IFiduciaryProductDetailRepository _repository;

    public UpdateFiduciaryProductDetailStatusCommandHandler(ILogger<UpdateFiduciaryProductDetailStatusCommandHandler> logger, IFiduciaryProductDetailRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<FiduciaryProductDetail?>> Handle(UpdateFiduciaryProductDetailStatusCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.FiduciaryProductDetailId, ct);
            if (entity is null)
                return Result.Failure<FiduciaryProductDetail>(FiduciaryProductDetailErrors.FiduciaryProductDetailWithIdNotFound);

            entity.ChangeStatus(request.Status);
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
