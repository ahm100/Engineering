using Engineering.Application.Abstractions.Data.FiduciaryProducts;
using Engineering.Domain.Entities.FiduciaryProducts;

namespace Engineering.Application.Services.FiduciaryProductManages.Commands.UpdateFiduciaryProductDetailReturn;

public class UpdateFiduciaryProductDetailReturnCommandHandler : ICommandHandler<UpdateFiduciaryProductDetailReturnCommand, FiduciaryProductDetailReturn>
{
    private readonly ILogger<UpdateFiduciaryProductDetailReturnCommandHandler> _logger;
    private readonly IFiduciaryProductDetailReturnRepository _repository;

    public UpdateFiduciaryProductDetailReturnCommandHandler(ILogger<UpdateFiduciaryProductDetailReturnCommandHandler> logger, IFiduciaryProductDetailReturnRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<FiduciaryProductDetailReturn?>> Handle(UpdateFiduciaryProductDetailReturnCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.FiduciaryProductDetailReturnId, ct);

            if (entity is null)
                return Result.Failure<FiduciaryProductDetailReturn>(FiduciaryProductDetailReturnErrors.FiduciaryProductDetailReturnWithIdNotFound);

            entity.SetDescription(request.Description);
            entity.SetLateDay(request.LateDay);
            entity.SetLateFine(request.LateFine);
            entity.SetCurrencyId(request.CurrencyId);
            entity.SetReturnCount(request.ReturnCount);
            entity.SetReturnDate(request.ReturnDate);
            entity.SetType(request.Type);

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<FiduciaryProductDetailReturn>(SharedErrors.UnknownError);
        }
    }
}